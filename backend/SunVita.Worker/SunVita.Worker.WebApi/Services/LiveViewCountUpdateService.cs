using Newtonsoft.Json;
using SunVita.Core.Common.DTO.Live;
using SunVita.Worker.WebApi.Interfaces;
using System.Net.NetworkInformation;
using System.Text;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveViewCountsUpdateService : ILiveViewCountsUpdateService
    {
        public List<LiveViewCountsDto> CurrentLineStatus { get; set; }

        public LiveViewCountsUpdateService()
        {
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
                    LineTitle = "Цех №5 (Лінія1)",
                    IpAddress = "10.61.2.23",
                    QuantityFact = -1,
                    QuantityPlan = 2000,
                    StartedAt = DateTime.Now
                }
            };
        }

        private readonly string[] _printers = { "10.61.2.21", "10.61.2.22", "10.61.2.23" };
        public async Task<LiveViewCountsDto> GetUpdateFromPrinter(int lineId)
        {
            var newLineCounts = new LiveViewCountsDto { LineId = lineId };

            var pinger = new Ping();
            var reply = pinger.Send(_printers[lineId], 500);

            if (reply.Status != IPStatus.Success)
                return null;

            using var client = new HttpClient();
            try
            {
                var resultStat = await client.GetStringAsync($"http://{_printers[lineId]}/updatestatistics.masp");

                var resultNomenc = await client.GetStringAsync($"http://{_printers[lineId]}/selectjob.masp");


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

                            newLineCounts.QuantityFact = int.Parse(countString);
                        }

                        data = resultNomenc.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                        var str = data
                            .Where(x => x.Contains("hostJobNameInput"))
                            .ToList();

                        var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        var hostJobNameProperty = hostJobNameLine
                            .Where(x => x.Contains("value"))
                            .FirstOrDefault();

                        //newLineCounts.NomenclatureTitle = str[1].Substring(81, str[1].Length - (81 + 53));
                        await Console.Out.WriteLineAsync($"{newLineCounts.QuantityFact} - {newLineCounts.NomenclatureTitle}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newLineCounts;
        }

        public async Task SendNewCountsToCore(ICollection<LiveViewCountsDto> updatesCounts)
        {
            var json = JsonConvert.SerializeObject(updatesCounts);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5050/liveviewcounts";

            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(url, data);

            httpClient.Dispose();
        }
        public LiveViewCountsDto SetCountsForNewNomenclature(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts)
        {
            newCounts.LineId = currentCounts.LineId;
            newCounts.LineTitle = currentCounts.LineTitle;
            newCounts.StartedAt = DateTime.Now;
            newCounts.FinishedAt = newCounts.StartedAt.AddHours(12);
            newCounts.ProductivityCurrent = 0;
            newCounts.ProductivityTop = 0;
            newCounts.ProductivityAvg = 0;
            newCounts.QuantityPlan = 2000;

            return newCounts;
        }

        public LiveViewCountsDto CalculateCounts(LiveViewCountsDto currentCounts, LiveViewCountsDto newCounts)
        {
            newCounts.LineId = currentCounts.LineId;
            newCounts.LineTitle = currentCounts.LineTitle;
            newCounts.QuantityPlan = currentCounts.QuantityPlan;
            newCounts.NomenclatureTitle = currentCounts.NomenclatureTitle;  ///////////
            newCounts.LineCode = currentCounts.LineCode;                    /////////
            newCounts.NomenclatureInBox = currentCounts.NomenclatureInBox;  /////////
            newCounts.StartedAt = currentCounts.StartedAt;
            newCounts.WorkTime = (int)(DateTime.Now - currentCounts.StartedAt).TotalSeconds;

            var workTime = (DateTime.Now - currentCounts.StartedAt).TotalSeconds + 60;

            if (newCounts.QuantityFact != 0 && currentCounts.QuantityPlan != 0)
            {
                float quantutyDiff = newCounts.QuantityFact - currentCounts.QuantityFact;
                float timeDiff = newCounts.WorkTime - currentCounts.WorkTime;

                if (timeDiff > 0 && quantutyDiff > 0)
                {

                    float prodCurr = quantutyDiff / (timeDiff / 60);

                    if (prodCurr < 1 && prodCurr > 0)
                        newCounts.ProductivityCurrent = 1;

                    else newCounts.ProductivityCurrent = (int)prodCurr;

                    if (newCounts.ProductivityCurrent > currentCounts.ProductivityTop)
                    {
                        newCounts.ProductivityTop = newCounts.ProductivityCurrent;
                    }
                    else
                    {
                        newCounts.ProductivityTop = currentCounts.ProductivityTop;
                    }
                }

                var prodAvg = newCounts.QuantityFact / (workTime / 60);
                newCounts.ProductivityAvg = (int)prodAvg;
                var timeToFinish = (workTime / newCounts.QuantityFact) * (currentCounts.QuantityPlan - newCounts.QuantityFact);
                newCounts.FinishedAt = DateTime.Now.AddSeconds(timeToFinish);
            }

            return newCounts;
        }

        public void SetNewTaskCounts(LiveTaskDto liveTaskDto)
        {
            var line = CurrentLineStatus
                .FirstOrDefault(x => x.LineCode == liveTaskDto.ProductionLineCode);

            if (line is not null)
            {
                line.QuantityPlan = liveTaskDto.Quantity;
                line.NomenclatureTitle = liveTaskDto.NomenclatureTitle;
                line.NomenclatureInBox = liveTaskDto.NomenclatureInBox;
            }
                
        }
    }
}
