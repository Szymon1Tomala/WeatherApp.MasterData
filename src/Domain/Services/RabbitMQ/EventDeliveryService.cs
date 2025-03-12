using System.Text;
using Domain.Persistence;
using Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Domain.Services.RabbitMQ;

public class EventDeliveryService
{
    private readonly IChannel _channel;
    private readonly DatabaseContext _databaseContext;
    
    public EventDeliveryService(DatabaseContext databaseContext)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnectionAsync().Result;
        _channel = connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null).Wait();
        _databaseContext = databaseContext;
    }
    public async Task PublishEvents()
    {
        var eventsToPublish = await _databaseContext.Events
            .Where(e => e.Status == EventStatus.Pending)
            .OrderBy(e => e.CreatedOn)
            .ToListAsync();

        foreach (var @event in eventsToPublish)
        {
            var serializedMessage = JsonConvert.SerializeObject(@event.Content);
            
            var body = Encoding.UTF8.GetBytes(serializedMessage);
            
            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
            @event.Status = EventStatus.Published;
            @event.PublishedOn = DateTime.UtcNow;
            _databaseContext.Events.Update(@event);
            await _databaseContext.SaveChangesAsync();
        }
    }
}