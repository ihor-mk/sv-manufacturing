using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using SunVita.RabbitMQ.Interfaces;
using System.Text;
using Newtonsoft.Json;

namespace SunVita.RabbitMQ.Services
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IModel _channel;
        private EventingBasicConsumer _consumer = null!;

        public MessageConsumer(IConnectionProvider connectionProvider)
        {
            _channel = connectionProvider.Connection!.CreateModel();
        }

        public void Init(string queue)
        {
            _channel.QueueDeclare(queue, true, false, false);
            _consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: queue, autoAck: true, consumer: _consumer);
        }

        public void Receive<T>(Action<T?> onMessage)
        {
            _consumer.Received += (model, eventArgs) =>
            {
                var jsonSpecified = Encoding.UTF8.GetString(eventArgs.Body.Span);
                var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
                onMessage(item);
            };
        }
    }
}
