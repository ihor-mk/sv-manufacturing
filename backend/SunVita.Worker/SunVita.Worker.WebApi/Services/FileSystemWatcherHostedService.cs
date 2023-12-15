using Newtonsoft.Json;
using SunVita.Core.Common.DTO.DoneTask;
using SunVita.Core.Common.DTO.Live;
using SunVita.Worker.WebApi.Interfaces;
using System.Text;

namespace SunVita.Worker.WebApi.Services
{
    public class FileSystemWatcherHostedService : BackgroundService
    {
        private readonly ILiveViewCountsUpdateService _countsUpdateService;
        public FileSystemWatcherHostedService(ILiveViewCountsUpdateService countsUpdateService) 
        {
            _countsUpdateService = countsUpdateService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var doneTaskWatcher = new FileSystemWatcher(@"d:\dev\donejobs");
            this.SetWatcher(doneTaskWatcher);
            doneTaskWatcher.Created += DoneTaskOnChanged;


            using var liveTaskWatcher = new FileSystemWatcher(@"d:\dev\inwork");
            this.SetWatcher(liveTaskWatcher);
            liveTaskWatcher.Created += LiveTaskOnChanged;

            while (!stoppingToken.IsCancellationRequested) { }

            return Task.CompletedTask;
        }
        private async void DoneTaskOnChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);
            try
            {
                using var streamReader = new StreamReader(e.FullPath);

                var fileText = streamReader.ReadToEnd();

                var doneTaskDto = JsonConvert.DeserializeObject<DoneTaskFileDto[]>(fileText);

                var json = JsonConvert.SerializeObject(doneTaskDto![0]);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:5050/donetask";

                using var httpClient = new HttpClient();

                var response = await httpClient.PostAsync(url, data);

                Console.WriteLine($"Changed: {e.FullPath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Changed: {ex.Message}");
            }
            finally
            {
                File.Delete(e.FullPath);
            }

        }
        private void LiveTaskOnChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);

            try
            {
                using var streamReader = new StreamReader(e.FullPath);

                var fileText = streamReader.ReadToEnd();

                var liveTaskDto = JsonConvert.DeserializeObject<LiveTaskDto[]>(fileText)![0];

                _countsUpdateService.SetNewTaskCounts(liveTaskDto);

                Console.WriteLine($"Changed: {e.FullPath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Changed: {ex.Message}");
            }
            finally
            {
                File.Delete(e.FullPath);
            }
        }

        private void SetWatcher(FileSystemWatcher watcher)
        {
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite;

            watcher.Filter = "*.json";

            watcher.IncludeSubdirectories = true;

            watcher.EnableRaisingEvents = true;
        }
    }
}
