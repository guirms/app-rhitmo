using Infra.MessagePublisher.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace Infra.MessagePublisher.RabbitMQ;

public sealed class RabbitMqConfig : IRabbitMqConfig, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly string _exchangeName;
    private readonly string _rountingKey;
    private const string _type = "direct";

    public RabbitMqConfig(IConfiguration configuration)
    {
        _queueName = configuration["RabbitMQ:QueueName"] ?? throw new NullReferenceException("Fila não encontrada");
        _exchangeName = configuration["RabbitMQ:ExchangeName"] ?? throw new NullReferenceException("Exchange não encontrada");
        _rountingKey = configuration["RabbitMQ:RoutingKey"] ?? throw new NullReferenceException("Chave de rota não encontrada");

        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"],
            UserName = configuration["RabbitMQ:UserName"],
            Password = configuration["RabbitMQ:Password"],
            Port = Convert.ToInt16(configuration["RabbitMQ:Port"])
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Publicar(MessageSenderRequest data)
    {
        _channel.ExchangeDeclare(
            exchange: _exchangeName,
            type: _type,
            durable: true,
            autoDelete: false,
            arguments: null
            );

        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        _channel.QueueBind(_queueName, _exchangeName, _rountingKey);

        var body = Encoding.UTF8.GetBytes(data.QtdLinhas.ToString());

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: _rountingKey,
            basicProperties: null,
            body: body
        );
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}
