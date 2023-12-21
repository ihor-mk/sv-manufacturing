using SunVita.Core.Common.DTO.Live;
using SunVita.RabbitMQ.Interfaces;
using SunVita.Worker.WebApi.Interfaces;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveCountReaderHostedService : BackgroundService
    {
        private readonly IMessageProducer _producer;
        private readonly ILiveViewCountsUpdateService _updateService;
        public LiveCountReaderHostedService(IMessageProducer producer, ILiveViewCountsUpdateService updateService)
        {
            _producer = producer;
            _updateService = updateService;

            _producer.Init("notifications", "");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                _updateService.NewLineStatus.Clear();

                foreach (var currentCounts in _updateService.CurrentLineStatus)
                {
                    _updateService.NewLineStatus.Add((LiveViewCountsDto)currentCounts.Clone());
                }

                await Parallel.ForEachAsync(_updateService.CurrentLineStatus, async (currentLine, cancellationToken) =>
                {
                    Thread.Sleep(5000);


                    var temp = await _updateService.GetUpdateFromPrinter(currentLine.IpAddress);

                    if (temp is not null)
                    {
                        _updateService.NewLineStatus[currentLine.LineId].QuantityFact = temp.BoxesQuantity * currentLine.NomenclatureInBox;
                        _updateService.NewLineStatus[currentLine.LineId].NomenclatureOnPrinter = temp.NomenclatureOnPrinter;

                        if (_updateService.NewLineStatus[currentLine.LineId].NomenclatureOnPrinter != currentLine.NomenclatureOnPrinter ||
                            _updateService.NewLineStatus[currentLine.LineId].QuantityFact < currentLine.QuantityFact)
                        {
                            _updateService.NewLineStatus[currentLine.LineId] =
                                _updateService.SetCountsForNewNomenclature(currentLine, _updateService.NewLineStatus[currentLine.LineId]);

                            await Console.Out.WriteLineAsync("New nomenclature");
                            return;
                        }

                        //if (_updateService.NewLineStatus[currentLine.LineId].QuantityFact > currentLine.QuantityFact)
                        //{
                            _updateService.NewLineStatus[currentLine.LineId] = 
                                _updateService.CalculateCounts(currentLine, _updateService.NewLineStatus[currentLine.LineId]);

                            await Console.Out.WriteLineAsync("Update Counts");
                        //}
                        //else
                        //{
                        //    _updateService.NewLineStatus[currentLine.LineId] = (LiveViewCountsDto)currentLine.Clone();
                        //}
                    }
                    else 
                    {
                        _updateService.NewLineStatus[currentLine.LineId] =
                               _updateService.CalculateCounts(currentLine, _updateService.NewLineStatus[currentLine.LineId]);

                        await Console.Out.WriteLineAsync("Update Counts");
                        //_updateService.NewLineStatus[currentLine.LineId] = (LiveViewCountsDto)currentLine.Clone(); 
                    }
                }
                );

                if (!Enumerable.SequenceEqual(_updateService.NewLineStatus, _updateService.CurrentLineStatus))
                {
                    await _updateService.SendNewCountsToCore(_updateService.NewLineStatus);
                    _producer.SendMessage(_updateService.NewLineStatus);

                    _updateService.CurrentLineStatus.Clear();
                    foreach (var newCounts in _updateService.NewLineStatus)
                    {
                        _updateService.CurrentLineStatus.Add((LiveViewCountsDto)newCounts.Clone());
                    }
                }
                
            }
        }
    }
}

