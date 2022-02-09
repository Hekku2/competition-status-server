using ConsoleClient.Util;

namespace ConsoleClient;

public class SimulatorWorker : BackgroundService
{
    private readonly ILogger<SimulatorWorker> _logger;
    private readonly ApiWrapper _apiWrapper;

    public SimulatorWorker(ILogger<SimulatorWorker> logger, ApiWrapper apiWrapper)
    {
        _logger = logger;
        _apiWrapper = apiWrapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var data = Data.CreateInitialData();
        _logger.LogInformation("Waiting to start simulator...");
        await Task.Delay(5000, stoppingToken);

        _logger.LogInformation("Uploading initial competition data");
        await _apiWrapper.UploadCompetition(data, stoppingToken);
        await Task.Delay(5000, stoppingToken);

        // Set competitor A active
        // Set competitor B active
        // Set competitor A results
        // set competitor C active
        // Set competitor B results
        // Set competitor C results
        _logger.LogInformation("Simulator has finished.");
    }

}
