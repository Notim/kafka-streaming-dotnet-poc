using System.Text;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using Freemarket.Ordering.Applications.ExternalEvents;
using Freemarket.Ordering.Domain.DomainModels;
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
    public async Task<IActionResult> Get([FromBody] string variable)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
            
        };

        var dynamicObject = new OrderRequestedByCustomer{
            OrderId = Guid.NewGuid(),
            TotalValue = decimal.MaxValue,
            ClientName = variable
        };

        using (var producer = new ProducerBuilder<string, OrderRequestedByCustomer>(config)
                              .SetKeySerializer(Serializers.Utf8)
                              .SetValueSerializer(new JsonKakfaMessage<OrderRequestedByCustomer>())
                              .Build()
        )
        {
            
            for (int i = 0; i <= 1000; i++) {
                var headers = new Headers();

                headers.Add("correlation-id", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                    
                _logger.LogInformation("WRITING MESSAGE");

                var result = await producer.ProduceAsync(
                    "freemarket-order-requested-by-customer",
                    new Message<string, OrderRequestedByCustomer>{
                        Key   = Guid.NewGuid().ToString(),
                        Value = dynamicObject,
                        Headers = headers
                    }
                );
            }
            
        }

        return NoContent();
    }

}