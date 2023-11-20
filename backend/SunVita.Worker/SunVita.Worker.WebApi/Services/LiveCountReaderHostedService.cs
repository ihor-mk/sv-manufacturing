using SunVita.Core.Common.DTO.Live;
using SunVita.RabbitMQ.Interfaces;
using SunVita.Worker.WebApi.Interfaces;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveCountReaderHostedService : BackgroundService
    {
        private readonly IMessageProducer _producer;
        private readonly ILogger<LiveCountReaderHostedService> _logger;
        private readonly ILiveViewCountsUpdateService _updateService;
        private List<LiveViewCountsDto> _currentLineStatus;
        public LiveCountReaderHostedService(IMessageProducer producer, ILogger<LiveCountReaderHostedService> logger, ILiveViewCountsUpdateService updateService)
        {
            _producer = producer;
            _logger = logger;
            _updateService = updateService;

            _producer.Init("notifications", "");

            _currentLineStatus = new List<LiveViewCountsDto>()
            {
                new LiveViewCountsDto() {LineId = 0, LineTitle="Цех №2  (Лінія1)", QuantityFact = -1, QuantityPlan = 2000, StartedAt = DateTime.Now},
                //new LiveViewCountsDto() {LineId = 0, QuantityFact = -1, QuantityPlan = 2000},
                //new LiveViewCountsDto() {Id = 2, CurrentQuantity = -1}
            };
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var newLineStatus = new List<LiveViewCountsDto>();

            while (true)
            {
                newLineStatus = new List<LiveViewCountsDto>();

                for (int i = 0; i < _currentLineStatus.Count; i++)
                {
                    newLineStatus.Add(await _updateService.GetUpdateFromPrinter(i));

                    if (newLineStatus[i].NomenclatureTitle != _currentLineStatus[i].NomenclatureTitle)
                    {
                        newLineStatus[i] = _updateService.SetCountsForNewNomenclature(_currentLineStatus[i], newLineStatus[i]);
                        await Console.Out.WriteLineAsync("New nomenclature");
                    }
                    if (newLineStatus[i].QuantityFact != _currentLineStatus[i].QuantityFact)
                    {
                        newLineStatus[i] = _updateService.CalculateCounts(_currentLineStatus[i], newLineStatus[i]);
                        await Console.Out.WriteLineAsync("Update Counts");
                    }
                }

                if (!Enumerable.SequenceEqual(newLineStatus, _currentLineStatus))
                {
                    await _updateService.SendNewCountsToCore(newLineStatus);
                    _producer.SendMessage(newLineStatus);
                    _currentLineStatus = new List<LiveViewCountsDto> (newLineStatus);
                    newLineStatus.Clear();
                }
            }
        }
    }
}

