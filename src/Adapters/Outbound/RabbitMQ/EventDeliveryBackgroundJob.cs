using Domain.Services.RabbitMQ;

namespace Adapters.Outbound.RabbitMQ;

public class EventDeliveryBackgroundJob(EventDeliveryService service)
{
    public async Task PublishEvents()
    {
        await service.PublishEvents();
    }
}