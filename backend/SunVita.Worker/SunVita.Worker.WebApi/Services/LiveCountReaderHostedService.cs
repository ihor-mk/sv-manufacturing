using SunVita.Core.Common.DTO.Live;
using SunVita.RabbitMQ.Interfaces;
using SunVita.Worker.WebApi.Interfaces;
using System.Diagnostics.Metrics;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveCountReaderHostedService : BackgroundService
    {
        private readonly IMessageProducer _producer;
        private readonly ILogger<LiveCountReaderHostedService> _logger;
        private readonly ILiveViewCountsUpdateService _updateService;
        private ICollection<LiveViewCountsDto> _currentLineStatus;
        private bool updateFlag;
        public LiveCountReaderHostedService(IMessageProducer producer, ILogger<LiveCountReaderHostedService> logger, ILiveViewCountsUpdateService updateService)
        {
            _producer = producer;
            _logger = logger;
            _updateService = updateService;

            _producer.Init("notifications", "");

            _currentLineStatus = new List<LiveViewCountsDto>()
            {
                new LiveViewCountsDto() {LineId = 0, QuantityFact = -1},
                //new LiveViewCountsDto() {Id = 1, CurrentQuantity = -1},
                //new LiveViewCountsDto() {Id = 2, CurrentQuantity = -1}
            };
            updateFlag = false;
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
            
            while (true)
            {
                var newLineStatus = new List<LiveViewCountsDto>();

                int counter = 0;

                foreach (var line in _currentLineStatus)
                {
                    newLineStatus.Add(await _updateService.GetUpdateFromPrinter(counter++));
                }
                counter = 0;

                if (!Enumerable.SequenceEqual(newLineStatus, _currentLineStatus))
                {
                    await _updateService.SendNewCountsToCore(newLineStatus);
                    _producer.SendMessage(newLineStatus);
                    _currentLineStatus = new List<LiveViewCountsDto> (newLineStatus);
                    newLineStatus.Clear();
                }
            }
            

            //using var client = new HttpClient();

            //int currentCount = -1;

            //int newQuantity = 0;

            //while (true)
            //{
            //    try
            //    {
            //        Console.WriteLine("--------------------------------------------------");

            //        var resultNomenc = await client.GetStringAsync("http://10.61.2.21/selectjob.masp");

            //        var resultStat = await client.GetStringAsync("http://10.61.2.21/updatestatistics.masp");

            //        if (resultStat != null)
            //        {
            //            var data = resultStat.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            //            if (data != null)
            //            {
            //                var preCountString = data.Where(x => x.Contains("Качественная партия")).FirstOrDefault();



            //                if (preCountString != null)
            //                {
            //                    var stringIndex = Array.IndexOf(data, preCountString);

            //                    var countString = data[++stringIndex]
            //                        .Split("\t", StringSplitOptions.RemoveEmptyEntries)[0]
            //                        .Split("\"", StringSplitOptions.RemoveEmptyEntries)[0];

            //                    newQuantity = int.Parse(countString);

            //                    Console.WriteLine($"count - {newQuantity}");
            //                }

            //                data = resultNomenc.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            //                var str = data
            //                    .Where(x => x.Contains("hostJobNameInput"))
            //                    .ToList();

            //                var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //                var hostJobNameProperty = hostJobNameLine
            //                    .Where(x => x.Contains("value"))
            //                    .FirstOrDefault();


            //                var name = str[1].Substring(81, str[1].Length - (81 + 53));

            //                Console.WriteLine(name);
            //            }
            //        }

            //        if (currentCount != newQuantity)
            //        {
            //            _producer.SendMessage(new LineUpdateDto() { Id = 1, CurrentQuantity = newQuantity });
            //            currentCount = newQuantity;
            //        }
            //        Thread.Sleep(5000);
            //    }
            //    catch (Exception ex) 
            //    {
            //        _logger.LogError(ex, "Printer live reader fail");
            //    }
            //}
        }
    }
}

