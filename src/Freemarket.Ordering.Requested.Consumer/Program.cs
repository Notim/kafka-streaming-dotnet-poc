using OrderRequestConsumer;
using OrderRequestConsumer.Workers;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices(services =>
               {
                   services.AddHostedService<Worker>();
               }).Build();

host.Run();