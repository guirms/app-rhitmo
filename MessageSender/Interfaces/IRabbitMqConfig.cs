namespace Infra.MessagePublisher.Interfaces;

public interface IRabbitMqConfig
{
    void Publicar(MessageSenderRequest data);
}
