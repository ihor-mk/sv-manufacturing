using Newtonsoft.Json;
using SunVita.Core.Common.DTO.Live;
using SunVita.Worker.WebApi.Interfaces;
using System.Net.NetworkInformation;
using System.Text;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveViewCountsUpdateService : ILiveViewCountsUpdateService
    {
        public object Locker { get; set; }
        public List<LiveViewCountsDto> CurrentLineStatus { get; set; }
        public List<LiveViewCountsDto> NewLineStatus { get; set; }

        private readonly string _coreApi = "http://localhost:5050/liveviewcounts";
        public LiveViewCountsUpdateService()
        {
            Locker = new object();

            CurrentLineStatus = new List<LiveViewCountsDto>()
            {
                new LiveViewCountsDto()
                {
                    LineId = 0,
                    LineCode = "000000010",
                    LineTitle = "Цех №2 (Лінія 2)",
                    IpAddress = "10.61.2.21",
                    QuantityFact = -1,
                    QuantityPlan = 2000,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 1,
                    LineCode = "000000009",
                    LineTitle = "Цех №2 (Лінія 1)",
                    IpAddress = "10.61.2.22",
                    QuantityFact = -1,
                    QuantityPlan = 2000,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 2,
                    LineCode = "000000026",
                    LineTitle = "Цех №5 (Лінія1)",
                    IpAddress = "10.61.2.23",
                    QuantityFact = -1,
                    QuantityPlan = 2000,
                    StartedAt = DateTime.Now
                }
            };

            NewLineStatus = new List<LiveViewCountsDto>();
        }
        public async Task<LivePrinterCounts> GetUpdateFromPrinter(string IpAddress)
        {
            var newPrinterCounts = new LivePrinterCounts();

            var pinger = new Ping();
            var reply = pinger.Send(IpAddress, 500);

            if (reply.Status != IPStatus.Success)
                return null!;

            using var client = new HttpClient();

            try
            {
                var resultStat = await client.GetStringAsync($"http://{IpAddress}/updatestatistics.masp");

                var resultNomenc = await client.GetStringAsync($"http://{IpAddress}/selectjob.masp");


                if (resultStat is not null && resultNomenc is not null)
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

                            newPrinterCounts.BoxesQuantity = int.Parse(countString);
                        }

                        data = resultNomenc.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                        var str = data
                            .Where(x => x.Contains("hostJobNameInput"))
                            .ToList();

                        var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        var hostJobNameProperty = hostJobNameLine
                            .Where(x => x.Contains("value"))
                            .FirstOrDefault();

                        if (hostJobNameProperty is not null)
                        {
                            newPrinterCounts.NomenclatureOnPrinter = str[1].Substring(81, str[1].Length - (81 + 53));
                        }

                        await Console.Out.WriteLineAsync($"{newPrinterCounts.BoxesQuantity} - {newPrinterCounts.NomenclatureOnPrinter}");
                    }
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newPrinterCounts;
        }

        public async Task SendNewCountsToCore(ICollection<LiveViewCountsDto> updatesCounts)
        {
            var json = JsonConvert.SerializeObject(updatesCounts);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_coreApi, data);
        }
        public LiveViewCountsDto SetCountsForNewNomenclature(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts)
        {
            lock (Locker)
            {
                newCounts.StartedAt = DateTime.Now;
                newCounts.FinishedAt = newCounts.StartedAt.AddHours(6);
                newCounts.WorkTime = 0;
                newCounts.ProductivityCurrent = 0;
                newCounts.ProductivityTop = 0;
                newCounts.ProductivityAvg = 0;
                newCounts.IsNewPrinterNomenclature = true;

                if (!currentCounts.IsNewNomenclature)
                {
                    newCounts.NomenclatureTitle = newCounts.NomenclatureOnPrinter;

                    newCounts.QuantityPlan = 1;
                }
                else
                {
                    newCounts.NomenclatureTitle = currentCounts.NomenclatureTitle;
                    newCounts.IsNewNomenclature = false;
                    newCounts.IsNewPrinterNomenclature = false;
                }
            }

            return newCounts;
        }

        public LiveViewCountsDto CalculateCounts(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts)
        {
            lock (Locker)
            {
                newCounts.WorkTime = (DateTime.Now - currentCounts.StartedAt).TotalSeconds;

                if (newCounts.QuantityFact != 0 && currentCounts.QuantityPlan != 0)
                {
                    var quantutyDiff = newCounts.QuantityFact - currentCounts.QuantityFact;
                    var timeDiff = newCounts.WorkTime - currentCounts.WorkTime;

                    if (timeDiff > 0 
                       // && quantutyDiff > 0 
                        && quantutyDiff < newCounts.NomenclatureInBox * 3)
                    {

                        newCounts.ProductivityCurrent = quantutyDiff / (timeDiff / 60);

                        if (newCounts.ProductivityCurrent > currentCounts.ProductivityTop)
                        {
                            newCounts.ProductivityTop = newCounts.ProductivityCurrent;
                        }
                    }

                    newCounts.ProductivityAvg = newCounts.QuantityFact / (newCounts.WorkTime / 60);
                    var timeToFinish = (newCounts.WorkTime / newCounts.QuantityFact) * (currentCounts.QuantityPlan - newCounts.QuantityFact);

                    if (newCounts.QuantityFact >= newCounts.QuantityPlan)
                        newCounts.FinishedAt = DateTime.Now;
                    else
                        newCounts.FinishedAt = DateTime.Now.AddSeconds(timeToFinish);
                }
            }

            return newCounts;
        }

        public void SetNewTaskCounts(LiveTaskDto liveTaskDto)
        {
            lock (Locker)
            {
                var newLine = NewLineStatus
                    .FirstOrDefault(x => x.LineCode == liveTaskDto.ProductionLineCode);

                if (newLine is not null)
                {
                    newLine.QuantityPlan = (int)liveTaskDto.Quantity;
                    newLine.NomenclatureTitle = liveTaskDto.NomenclatureTitle;
                    newLine.NomenclatureInBox = liveTaskDto.NomenclatureInBox;
                    newLine.IsNewNomenclature = true;

                    if (newLine.IsNewPrinterNomenclature)
                    {
                        newLine.IsNewPrinterNomenclature = false;
                        newLine.IsNewNomenclature = false;
                    }
                }
                var currentLine = CurrentLineStatus
                    .FirstOrDefault(x => x.LineCode == liveTaskDto.ProductionLineCode);

                if (currentLine is not null)
                {
                    currentLine.QuantityPlan = (int)liveTaskDto.Quantity;
                    currentLine.NomenclatureTitle = liveTaskDto.NomenclatureTitle;
                    currentLine.NomenclatureInBox = liveTaskDto.NomenclatureInBox;
                    currentLine.IsNewNomenclature = true;

                    if (currentLine.IsNewPrinterNomenclature)
                    {
                        currentLine.IsNewPrinterNomenclature = false;
                        currentLine.IsNewNomenclature = false;
                    }
                }
            }

        }
    }
}
