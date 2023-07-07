using RabbitMQ.Client;

namespace SV_MFG.RabbitMQ.Interfaces
{
    public interface IConnectionProvider
    {
        public IConnection? Connection { get; }
    }
}
