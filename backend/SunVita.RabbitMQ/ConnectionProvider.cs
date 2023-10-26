using RabbitMQ.Client;
using SunVita.RabbitMQ.Interfaces;

namespace SunVita.RabbitMQ
{
    public class ConnectionProvider : IConnectionProvider
    {
        public IConnection Connection { get; }

        public ConnectionProvider(string hostname)
        {
            var factory = new ConnectionFactory { Uri = new Uri(hostname) };
            factory.AutomaticRecoveryEnabled = true;
            Connection = factory.CreateConnection();
        }
    }
}
