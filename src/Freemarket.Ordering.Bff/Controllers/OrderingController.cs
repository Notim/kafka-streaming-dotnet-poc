using System.Text;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using Freemarket.Ordering.Applications.ExternalEvents;
using Freemarket.Ordering.Infrastructure.Kafka;

namespace Freemarket.Ordering.Bff.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderingController : ControllerBase
{

    private readonly ILogger<OrderingController> _logger;

    public OrderingController(ILogger<OrderingController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromForm] int quantity)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };

        using (var producer = new ProducerBuilder<string, OrderRequestedByCustomer>(config)
                              .SetKeySerializer(Serializers.Utf8)
                              .SetValueSerializer(new XmlKakfaMessage<OrderRequestedByCustomer>())
                              .Build()
        )
        {
            
            for (var i = 0; i <= quantity; i++) {
                
                var dynamicObject = new OrderRequestedByCustomer{
                    OrderId = Guid.NewGuid(),
                    TotalValue = new Faker().Finance.Amount(),
                    ClientName = new Faker().Person.FullName
                };
                
                var headers = new Headers();

                headers.Add("correlation-id", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                
                _logger.LogInformation("WRITING MESSAGE {miss}/{all} {Message}", i, quantity, dynamicObject.ToString());

                var result = await producer.ProduceAsync(
                    Topics.OrderRequestedTopic,
                    new Message<string, OrderRequestedByCustomer>{
                        Key = dynamicObject.OrderId.ToString(),
                        Value = dynamicObject,
                        Headers = headers
                    }
                );
            }
            
        }

        return NoContent();
    }

}