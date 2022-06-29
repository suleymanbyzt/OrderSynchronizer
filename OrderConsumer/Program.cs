using OrderConsumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<OrderConsumer.OrderConsumer>(); })
    .Build();

await host.RunAsync();