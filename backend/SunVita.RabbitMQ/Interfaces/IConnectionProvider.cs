using RabbitMQ.Client;

namespace SunVita.RabbitMQ.Interfaces
{
    public interface IConnectionProvider
    {
        public IConnection? Connection { get; }
    }
}
