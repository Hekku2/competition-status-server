using System.Threading.Channels;
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

        var tasks = new[]
        {
            ListForPerformanceResultEvents(stoppingToken),
            ListForCompetitorEvents(stoppingToken)
        };
        Task.WaitAll(tasks, stoppingToken);
    }

    private async Task ListForCompetitorEvents(CancellationToken stoppingToken)
    {
        var channel = await _connection.StreamAsChannelAsync<CurrentCompetitorEnvelopeModel>("StreamCompetitors", stoppingToken);
        await channel.ReadUntilStopped((receivedEvent) =>
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
        }, stoppingToken);
    }

    private async Task ListForPerformanceResultEvents(CancellationToken stoppingToken)
    {
        var channel = await _connection.StreamAsChannelAsync<PerformanceResultsEnvelopeModel>("StreamPerformanceResults", stoppingToken);
        await channel.ReadUntilStopped((receivedEvent) =>
        {
            if (receivedEvent.Content == null)
            {
                _logger.LogError("This should never happen!");
            }
            else
            {
                var competitorLogFormat = string.Join(", ", receivedEvent.Content.Competitors.Select(c => $"{c.Name} - {c.Team}"));
                var result = receivedEvent.Content.Result;
                var resultLogFormat = $"{result.ArtisticScore:0.###} {result.ExecutionScore:0.###} {result.DifficultyScore:0.###} {result.HeadJudgePenalty:0.###} = {result.Total}";
                _logger.LogInformation("Results received: {Competitors}, division: {Division} -- {Score}",
                    competitorLogFormat,
                    receivedEvent.Content.Division,
                    resultLogFormat);
            }
        }, stoppingToken);
    }
}

public static class ChannelReaderExtensions
{
    public static async Task ReadUntilStopped<T>(this ChannelReader<T> channel, Action<T> action, CancellationToken stoppingToken)
    {
        while (await channel.WaitToReadAsync(stoppingToken))
        {
            while (channel.TryRead(out var receivedEvent))
            {
                action(receivedEvent);
            }
        }
    }
}
