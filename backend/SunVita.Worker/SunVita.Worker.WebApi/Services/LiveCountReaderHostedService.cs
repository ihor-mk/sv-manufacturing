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
                new LiveViewCountsDto() {LineId = 0, LineTitle="Цех №2 (Лінія 2)", QuantityFact = -1, QuantityPlan = 2000, StartedAt = DateTime.Now},
                new LiveViewCountsDto() {LineId = 1, LineTitle="Цех №2 (Лінія1)", QuantityFact = -1, QuantityPlan = 2000, StartedAt = DateTime.Now},
                new LiveViewCountsDto() {LineId = 2, LineTitle="Цех №5 (Лінія1)", QuantityFact = -1, QuantityPlan = 2000, StartedAt = DateTime.Now}
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
                newLineStatus = new List<LiveViewCountsDto>() { new LiveViewCountsDto(), new LiveViewCountsDto(), new LiveViewCountsDto() };

                await Parallel.ForEachAsync(_currentLineStatus, async (currentLine, cancellationToken) =>
                {
                    var temp = await _updateService.GetUpdateFromPrinter(currentLine.LineId);

                    if (temp is not null)
                    {
                        newLineStatus[currentLine.LineId] = temp;
    
                    if (newLineStatus[currentLine.LineId].NomenclatureTitle != currentLine.NomenclatureTitle ||
                        newLineStatus[currentLine.LineId].QuantityFact < currentLine.QuantityFact)
                            {
                                newLineStatus[currentLine.LineId] = _updateService.SetCountsForNewNomenclature(currentLine, newLineStatus[currentLine.LineId]);
                                await Console.Out.WriteLineAsync("New nomenclature");
                            }

                        if (newLineStatus[currentLine.LineId].QuantityFact > currentLine.QuantityFact)
                        {
                            newLineStatus[currentLine.LineId] = _updateService.CalculateCounts(currentLine, newLineStatus[currentLine.LineId]);
                            await Console.Out.WriteLineAsync("Update Counts");
                        }
                        else
                        {
                            newLineStatus[currentLine.LineId] = currentLine;
                        } 
                    }
                    else { newLineStatus[currentLine.LineId] = currentLine; }
                }
                );

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

