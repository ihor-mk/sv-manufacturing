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
                    LineCode = "000000040",
                    LineTitle = "Цех №1 (Лінія 1)",
                    IpAddress = "",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 1,
                    LineCode = "000000009",
                    LineTitle = "Цех №2 (Лінія 1)",
                    IpAddress = "10.61.2.22",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 2,
                    LineCode = "000000010",
                    LineTitle = "Цех №2 (Лінія 2)",
                    IpAddress = "10.61.2.21",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 3,
                    LineCode = "000000008",
                    LineTitle = "Цех №4 (Лінія 1, кросфолд 1)",
                    IpAddress = "",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 4,
                    LineCode = "000000026",
                    LineTitle = "Цех №5 (Лінія 1)",
                    IpAddress = "10.61.2.23",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 5,
                    LineCode = "000000047",
                    LineTitle = "Цех №5 (Лінія 2, кросфолд 2)",
                    IpAddress = "",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                },
                new LiveViewCountsDto()
                {
                    LineId = 6,
                    LineCode = "000000048",
                    LineTitle = "Цех №5 (Лінія 3)",
                    IpAddress = "10.61.2.24",
                    QuantityFact = 0,
                    QuantityPlan = 0,
                    StartedAt = DateTime.Now
                }
            };

            NewLineStatus = new List<LiveViewCountsDto>();
        }
        public async Task<LivePrinterCounts> GetUpdateFromPrinter(string IpAddress)
        {
            var newPrinterCounts = new LivePrinterCounts();

            var pinger = new Ping();
            var reply = pinger.Send(IpAddress, 1000);

            if (reply.Status != IPStatus.Success)
                return null!;

            using var client = new HttpClient();

            try
            {
                var resultStat = await client.GetStringAsync($"http://{IpAddress}/updatestatistics.masp");

                Thread.Sleep(150);

                var resultNomenc = await client.GetStringAsync($"http://{IpAddress}/selectjob.masp");


                if (resultStat is not null && resultNomenc is not null)
                {
                    var data = resultStat.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    if (data is not null)
                    {
                        var preCountString = data.Where(x => x.Contains("Качественная партия")).FirstOrDefault();

                        if (preCountString is not null && preCountString != "")
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

                        if (hostJobNameProperty is not null && hostJobNameProperty != "")
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

                    newCounts.QuantityPlan = 0;
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
                var dateTimeNow = DateTime.Now;

                var timeDiff = (dateTimeNow - currentCounts.StartedAt).TotalSeconds - currentCounts.WorkTime;

                if (newCounts.QuantityPlan == 0)
                    newCounts.QuantityFact = newCounts.QuantityFact / newCounts.NomenclatureInBox;

                if (newCounts.QuantityFact != currentCounts.QuantityFact)
                    newCounts.WorkTime = (dateTimeNow - currentCounts.StartedAt).TotalSeconds;

                if (newCounts.QuantityFact != 0)
                {
                    var quantutyDiff = newCounts.QuantityFact - currentCounts.QuantityFact;


                    if (timeDiff > 0 && quantutyDiff < newCounts.NomenclatureInBox * 4 && quantutyDiff >= 0)
                    {
                            newCounts.ProductivityCurrent = (quantutyDiff / (timeDiff / 60) + currentCounts.ProductivityCurrent * 15) / 16;

                            if (newCounts.ProductivityCurrent > currentCounts.ProductivityTop)
                            {
                                newCounts.ProductivityTop = newCounts.ProductivityCurrent;
                            }
                    }

                    newCounts.ProductivityAvg = newCounts.QuantityFact / ((currentCounts.WorkTime + timeDiff) / 60);
                    var timeToFinish = ((currentCounts.WorkTime + timeDiff) / newCounts.QuantityFact)
                        * (currentCounts.QuantityPlan - newCounts.QuantityFact);

                    if (newCounts.QuantityFact >= newCounts.QuantityPlan)
                        newCounts.FinishedAt = dateTimeNow;
                    else
                        newCounts.FinishedAt = dateTimeNow.AddSeconds(timeToFinish);
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
                    if (newLine.IpAddress == "")
                        newLine.StartedAt = DateTime.Now;
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
                    if (currentLine.IpAddress == "")
                        currentLine.StartedAt = DateTime.Now;
                }
            }

        }
    }
}
