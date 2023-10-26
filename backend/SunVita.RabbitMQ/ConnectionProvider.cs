using RabbitMQ.Client;

namespace SunVita.RabbitMQ
{
    public class ConnectionProvider
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
