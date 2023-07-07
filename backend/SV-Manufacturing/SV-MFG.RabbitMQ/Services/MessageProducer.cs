using Newtonsoft.Json;
using RabbitMQ.Client;
using SV_MFG.RabbitMQ.Interfaces;
using System.Text;

namespace SV_MFG.RabbitMQ.Services
{
    internal class MessageProducer : IMessageProducer
    {
        private readonly IModel _channel;
        private string _queue = string.Empty;
        private string _exchange = string.Empty;

        public MessageProducer(IConnectionProvider connectionProvider)
        {
            _channel = connectionProvider.Connection!.CreateModel();
        }

        public void Init(string queue, string exchange)
        {
            this._queue = queue;
            this._exchange = exchange;
            _channel.QueueDeclare(queue, true, false, false);
        }
        public void SendMessage<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: _exchange, routingKey: _queue, body: body);
        }
    }
}
