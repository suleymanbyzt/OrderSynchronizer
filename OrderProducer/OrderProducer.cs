using Confluent.Kafka;
using Newtonsoft.Json;
using OrderProducer.Models;

namespace OrderProducer;

public class OrderProducer : BackgroundService
{
    private readonly ILogger<OrderProducer> _logger;

    public OrderProducer(ILogger<OrderProducer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                int i = 0;
                List<Orders>? orders = Orders.FakeData.Generate(5);

                foreach (Orders order in orders)
                {
                    Task<DeliveryResult<string, string>>? result = producer.ProduceAsync("order-created",
                        new Message<string, string>
                        {
                            Key = $"{order.PairSymbol.ToLower()}-order-created",
                            Value = JsonConvert.SerializeObject(new Orders()
                            {
                                Id = order.Id,
                                UserId = order.UserId,
                                Price = order.Price,
                                Amount = order.Amount,
                                PairSymbol = order.PairSymbol
                            })
                        });
                    Task.Delay(1000);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            Console.WriteLine(e);
        }
    }
}