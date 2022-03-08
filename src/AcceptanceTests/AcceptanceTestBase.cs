using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Org.OpenAPITools.Api;

namespace AcceptanceTests;

public abstract class AcceptanceTestBase
{
    protected CancellationTokenSource TokenSource { get; private set; }
    protected CompetitionApi CompetitionApi { get; private set; }
    protected ScoreboardApi ScoreboardApi { get; private set; }

    protected HubConnection CompetitionHub { get; private set; }
    protected HubConnection ScoreboardHub { get; private set; }

    private const string CompetitionHubName = "competition-hub";
    private const string ScoreboardHubName = "scoreboard-hub";

    [SetUp]
    public async Task Setup()
    {
        var uri = Environment.GetEnvironmentVariable("APP_URI");
        if (string.IsNullOrWhiteSpace(uri))
        {
            Assert.Inconclusive("URI not set. Unable to execute acceptance tests.");
        }

        CompetitionApi = new CompetitionApi(uri);
        ScoreboardApi = new ScoreboardApi(uri);

        TokenSource = new CancellationTokenSource();

        CompetitionHub = new HubConnectionBuilder()
            .WithUrl($"{uri}{CompetitionHubName}")
            .AddJsonProtocol(options =>
            {
                var enumConverter = new JsonStringEnumConverter();
                options.PayloadSerializerOptions.Converters.Add(enumConverter);
            })
            .Build();
        ScoreboardHub = new HubConnectionBuilder()
            .WithUrl($"{uri}{ScoreboardHubName}")
            .AddJsonProtocol(options =>
            {
                var enumConverter = new JsonStringEnumConverter();
                options.PayloadSerializerOptions.Converters.Add(enumConverter);
            })
            .Build();

        await CompetitionHub.StartAsync(TokenSource.Token);
        await ScoreboardHub.StartAsync(TokenSource.Token);

        await OnSetup();
    }

    [TearDown]
    public void TearDown()
    {
        TokenSource?.Cancel();
    }

    protected abstract Task OnSetup();
}
