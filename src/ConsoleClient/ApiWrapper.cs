using Microsoft.Extensions.Options;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace ConsoleClient;

public class ApiWrapper
{
    private readonly CompetitionApi _client;

    public ApiWrapper(IOptions<SignalRSettings> config)
    {
        _client = new CompetitionApi(config.Value.BaseUrl);
    }

    public async Task UploadCompetition(CompetitionFileModel model, CancellationToken token)
    {
        await _client.CompetitionUploadCompetitionAsync(model, token);
    }

    public async Task SetCurrentCompetitor(CurrentCompetitorSetModel model, CancellationToken token)
    {
        await _client.CompetitionSetCurrentCompetitorAsync(model, token);
    }

    public async Task SetResults(CompetitorResultModel model, CancellationToken token)
    {
        await _client.CompetitionSetResultAsync(model, token);
    }
}
