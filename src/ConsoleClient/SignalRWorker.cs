using Api.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace ConsoleClient;

public class SignalRWorker : BackgroundService
{
    private readonly ILogger<SignalRWorker> _logger;
    private readonly HubConnection _connection;

    public SignalRWorker(ILogger<SignalRWorker> logger, IOptions<SignalRSettings> config)
    {
        _logger = logger;
        _connection = new HubConnectionBuilder()
            .WithUrl($"{config.Value.BaseUrl}competition-hub", HttpTransportType.LongPolling)
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _connection.StartAsync(stoppingToken);

        var channel = await _connection.StreamAsChannelAsync<CurrentCompetitorEnvelopeModel>("StreamCompetitors", stoppingToken);
        while (await channel.WaitToReadAsync(stoppingToken))
        {
            while (channel.TryRead(out var receivedEvent))
            {
                if (receivedEvent.Content == null)
                {
                    _logger.LogInformation("No competitor active.");
                }
                else
                {
                    var competitorLogFormat = string.Join(", ", receivedEvent.Content.Competitors.Select(c => $"{c.Name} - {c.Team}"));
                    _logger.LogInformation("New competitor(s): {Competitors}, division: {Division}", competitorLogFormat, receivedEvent.Content.Division);
                }
            }
        }
    }
}
