using Api.Models;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ConsoleClient;

public class ApiWrapper : IDisposable
{
    private const string ApiName = "Competition";
    private readonly RestClient _client;

    public ApiWrapper(IOptions<SignalRSettings> config)
    {
        _client = new RestClient($"{config.Value.BaseUrl}{ApiName}");
    }

    public async Task UploadCompetition(CompetitionFileModel model, CancellationToken token)
    {
        await _client.PostJsonAsync("upload-competition", model, token);
    }

    public async Task SetCurrentCompetitor(CurrentCompetitorSetModel model, CancellationToken token)
    {
        await _client.PostJsonAsync("set-current-competitor", model, token);
    }

    public async Task SetResults(CompetitorResultModel model, CancellationToken token)
    {
        await _client.PostJsonAsync("set-result", model, token);
    }

    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
