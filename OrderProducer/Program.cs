IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<OrderProducer.OrderProducer>(); })
    .Build();

await host.RunAsync();