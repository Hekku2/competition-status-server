using ConsoleClient.Util;
using Org.OpenAPITools.Model;

namespace ConsoleClient;

/// <summary>
/// This performs a simulated competition (on faster schedule).
/// 
/// Important: Loops etc are avoided here to make it easier to edit the
/// competition steps and order on the fly (and to make it easier to add
/// exceptions, like forfeits etc.)
/// </summary>
public class SimulatorWorker : BackgroundService
{
    private readonly ILogger<SimulatorWorker> _logger;
    private readonly ApiWrapper _apiWrapper;
    private readonly CompetitionFileModel _data;
    private readonly Dictionary<int, PoleResultFileModel> _results;

    public SimulatorWorker(ILogger<SimulatorWorker> logger, ApiWrapper apiWrapper)
    {
        _logger = logger;
        _apiWrapper = apiWrapper;

        _data = Data.CreateInitialData();
        _results = Data.CreateResults();
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        _logger.LogInformation("Waiting to start simulator...");
        await Task.Delay(5000, token);

        _logger.LogInformation("Uploading initial competition data");
        await _apiWrapper.UploadCompetition(_data, token);
        await Task.Delay(5000, token);

        await ExecuteFirstDivision(token);
        await ExecuteSecondDivision(token);

        _logger.LogInformation("Simulator has finished.");
    }

    private async Task ExecuteFirstDivision(CancellationToken token)
    {
        var division = _data.Divisions[0];
        using (_logger.BeginScope("Executing division: {Division}", division.Name))
        {
            await SetActiveCompetitor(division.Name, division.Items[1], token);
            //No results are set here. This is on purpose, because second competitor usually starts before results are received.
            await SetActiveCompetitor(division.Name, division.Items[2], token);
            await SetResults(division.Items[1], token);
            await SetActiveCompetitor(division.Name, division.Items[3], token);
            await SetResults(division.Items[2], token);
            await SetActiveCompetitor(division.Name, division.Items[4], token);
            await SetResults(division.Items[3], token);
            await SetActiveCompetitor(division.Name, division.Items[5], token);
            await SetResults(division.Items[4], token);
            await SetActiveCompetitor(division.Name, division.Items[6], token);
            await SetResults(division.Items[5], token);
            await SetActiveCompetitor(division.Name, division.Items[7], token);
            await SetResults(division.Items[6], token);
            await SetActiveCompetitor(division.Name, division.Items[8], token);
            await SetResults(division.Items[7], token);
            await SetActiveCompetitor(division.Name, division.Items[9], token);
            await SetResults(division.Items[8], token);
            await SetActiveCompetitor(division.Name, division.Items[10], token);
            await SetResults(division.Items[9], token);
            await SetActiveCompetitor(division.Name, null, token);
            await SetResults(division.Items[10], token);
        }
    }

    private async Task ExecuteSecondDivision(CancellationToken token)
    {
        var division = _data.Divisions[1];
        using (_logger.BeginScope("Executing division: {Division}", division.Name))
        {
            await SetActiveCompetitor(division.Name, division.Items[0], token);
            //No results are set here. This is on purpose, because second competitor usually starts before results are received.
            await SetActiveCompetitor(division.Name, division.Items[1], token);
            await SetResults(division.Items[0], token);
            await SetActiveCompetitor(division.Name, division.Items[2], token);
            await SetResults(division.Items[1], token);
            await SetActiveCompetitor(division.Name, null, token);
            await SetResults(division.Items[2], token);
        }
    }

    private async Task SetActiveCompetitor(string division, CompetitorPositionFileModel? competitor, CancellationToken token)
    {
        if (competitor is null)
        {
            _logger.LogInformation("Clearing active competitor");
            await Task.Delay(5000, token);
            return;
        }

        _logger.LogInformation("Next: {Name} - {Division}.", competitor.ToLogString(), division);
        await _apiWrapper.SetCurrentCompetitor(competitor.ToCurrentCompetitorSetModel(), token);
        await Task.Delay(5000, token);
    }

    private async Task SetResults(CompetitorPositionFileModel competitor, CancellationToken token)
    {
        _logger.LogInformation("Receiving results for {Competitor}", competitor.ToLogString());
        await _apiWrapper.SetResults(competitor.ToCurrentCompetitorSetModel(_results), token);
        await Task.Delay(5000, token);
    }
}

public static class CompetitorPositionFileModelExtensions
{
    public static CurrentCompetitorSetModel ToCurrentCompetitorSetModel(this CompetitorPositionFileModel model)
    {
        return new CurrentCompetitorSetModel
        {
            Id = model.Id
        };
    }

    public static string ToLogString(this CompetitorPositionFileModel model)
    {
        var name = string.Join(",", model.Competitors.Select(c => c.Name));
        return $"#{model.Id} - {name}";
    }

    public static CompetitorResultModel ToCurrentCompetitorSetModel(this CompetitorPositionFileModel model, Dictionary<int, PoleResultFileModel> results)
    {
        return new CompetitorResultModel
        {
            Id = model.Id,
            Results = results[model.Id]
        };
    }
}
