namespace Core.Application;

public class RabbitMqSettings
{
    public RabbitMqSettings()
    {
        RabbitMqConsumerSettings = new List<RabbitMqConsumerSettings>();
    }
    public bool IsActive { get; set; }

    public string Port { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public List<RabbitMqConsumerSettings> RabbitMqConsumerSettings { get; set; }
}

public class RabbitMqConsumerSettings
{
    public string QueueName { get; set; }

    public Type Type { get; set; }
}