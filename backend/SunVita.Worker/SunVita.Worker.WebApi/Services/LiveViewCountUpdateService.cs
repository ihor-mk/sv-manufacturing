using Newtonsoft.Json;
using SunVita.Core.Common.DTO.Live;
using SunVita.Worker.WebApi.Interfaces;
using System.Text;

namespace SunVita.Worker.WebApi.Services
{
    public class LiveViewCountsUpdateService : ILiveViewCountsUpdateService
    {
        private readonly string[] _printers = { "10.61.2.21", "10.61.2.22", "10.61.2.23" };
        public async Task<LiveViewCountsDto> GetUpdateFromPrinter(int lineId)
        {
            var newLineCounts = new LiveViewCountsDto { LineId = lineId };

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

                        newLineCounts.NomenclatureTitle = str[1].Substring(81, str[1].Length - (81 + 53));

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
    }
}
