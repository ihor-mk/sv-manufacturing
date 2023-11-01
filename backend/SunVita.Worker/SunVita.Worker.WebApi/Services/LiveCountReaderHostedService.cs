using SunVita.Core.Common.DTO.Live;
using SunVita.RabbitMQ.Interfaces;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveCountReaderHostedService : BackgroundService
    {
        private readonly IMessageProducer _producer;
        private readonly ILogger<LiveCountReaderHostedService> _logger;
        public LiveCountReaderHostedService(IMessageProducer producer, ILogger<LiveCountReaderHostedService> logger)
        {
            _producer = producer;
            _logger = logger;

            _producer.Init("notifications", "");
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

            using var client = new HttpClient();

            int currentCount = -1;

            int newCount = 0;

            while (true)
            {
                try
                {
                    Console.WriteLine("--------------------------------------------------");

                    var resultNomenc = await client.GetStringAsync("http://10.61.2.21/selectjob.masp");

                    var resultStat = await client.GetStringAsync("http://10.61.2.21/updatestatistics.masp");

                    if (resultStat != null)
                    {
                        var data = resultStat.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                        if (data != null)
                        {
                            var preCountString = data.Where(x => x.Contains("Качественная партия")).FirstOrDefault();



                            if (preCountString != null)
                            {
                                var stringIndex = Array.IndexOf(data, preCountString);

                                var countString = data[++stringIndex]
                                    .Split("\t", StringSplitOptions.RemoveEmptyEntries)[0]
                                    .Split("\"", StringSplitOptions.RemoveEmptyEntries)[0];

                                newCount = int.Parse(countString);

                                Console.WriteLine($"count - {newCount}");
                            }

                            data = resultNomenc.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                            var str = data
                                .Where(x => x.Contains("hostJobNameInput"))
                                .ToList();

                            var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                            var hostJobNameProperty = hostJobNameLine
                                .Where(x => x.Contains("value"))
                                .FirstOrDefault();


                            var name = str[1].Substring(81, str[1].Length - (81 + 53));

                            Console.WriteLine(name);
                        }
                    }

                    if (currentCount != newCount)
                    {
                        _producer.SendMessage(new LineUpdateDto() { Id = 1, CurrentCount = newCount });
                        currentCount = newCount;
                    }
                    Thread.Sleep(5000);
                }
                catch (Exception ex) 
                {
                    _logger.LogError(ex, "Printer live reader fail");
                }
            }
        }
    }
}

