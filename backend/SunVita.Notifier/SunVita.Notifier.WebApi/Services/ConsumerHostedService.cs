using SunVita.Core.Common.DTO.Live;
using SunVita.Notifier.WebApi.Hubs;
using SunVita.RabbitMQ.Interfaces;

namespace SunVita.Notifier.WebApi.Services
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly IMessageConsumer _consumer;
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly LiveViewHub _hub;
        public ConsumerHostedService(IMessageConsumer consumer, ILogger<ConsumerHostedService> logger, LiveViewHub hub)
        {
            _consumer = consumer;
            _logger = logger;
            _hub = hub;
            _consumer.Init("notifications");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                _consumer.Receive<ICollection<LiveViewCountsDto>>(async data =>
                {
                    if (data is not null)
                    {
                        await _hub.SendLineUpdateMessage(data);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }

            return Task.CompletedTask;
        }
    }
}
