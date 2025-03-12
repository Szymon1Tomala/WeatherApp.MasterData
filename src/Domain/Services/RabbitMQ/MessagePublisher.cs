using System.Text;
using Domain.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Domain.Services.RabbitMQ;

public class MessagePublisher
{
    private readonly IChannel _channel;
    
    public MessagePublisher()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnectionAsync().Result;
        _channel = connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null).Wait();
    }
    public async Task Publish(Message message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        
        var body = Encoding.UTF8.GetBytes(serializedMessage);
        
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        
        
    }
}