using ConsoleClient;

var builder = Host.CreateDefaultBuilder(args);
var host = builder
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<SignalRSettings>(context.Configuration.GetSection("SignalRSettings"));
    })
    .Build();

await host.RunAsync();
