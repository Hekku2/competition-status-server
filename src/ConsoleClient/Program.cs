using ConsoleClient;

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<SimulatorWorker>();
        services.AddSingleton<ApiWrapper>();
        services.AddHostedService<SignalRWorker>();
        services.Configure<SignalRSettings>(context.Configuration.GetSection("SignalRSettings"));
        services.Configure<SimulatorSettings>(context.Configuration.GetSection("SimulatorSettings"));
    })
    .ConfigureLogging(loggingBuilder =>
    {
        loggingBuilder.AddSimpleConsole();
    })
    .Build();

await host.RunAsync();
