using Confluent.Kafka;
using Newtonsoft.Json;
using OrderConsumer.Models;

namespace OrderConsumer;

public class OrderConsumer : BackgroundService
{
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var config = new ConsumerConfig()
            {
                GroupId = "order-created-consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe("order-created");
                CancellationTokenSource token = new();
                try
                {
                    while (true)
                    {
                        var result = consumer.Consume(token.Token);
                        if (result.Message != null)
                        {
                            List<Orders>? orders = JsonConvert.DeserializeObject<List<Orders>>(result.Message.Value);
                            consumer.Commit(result);
                        }
                        
                        Task.Delay(1000);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    Console.WriteLine(e);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}