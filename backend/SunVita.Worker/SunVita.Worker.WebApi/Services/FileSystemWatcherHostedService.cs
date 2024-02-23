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
            using var doneTaskWatcher = new FileSystemWatcher(@"c:\exchange\donejobs");
            this.SetWatcher(doneTaskWatcher);
            doneTaskWatcher.Created += DoneTaskOnChanged;


            using var startTaskWatcher = new FileSystemWatcher(@"c:\exchange\inwork");
            this.SetWatcher(startTaskWatcher);
            startTaskWatcher.Created += StartTaskOnChanged;

            using var finishTaskWatcher = new FileSystemWatcher(@"c:\exchange\outwork");
            this.SetWatcher(finishTaskWatcher);
            finishTaskWatcher.Created += StartTaskOnChanged;

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
                //File.Delete(e.FullPath);
            }

        }
        private void StartTaskOnChanged(object sender, FileSystemEventArgs e)
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
                //File.Delete(e.FullPath);
            }
        }
        private void FinishTaskOnChanged(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);

            try
            {
                using var streamReader = new StreamReader(e.FullPath);

                var fileText = streamReader.ReadToEnd();

                var liveTaskDto = JsonConvert.DeserializeObject<LiveTaskDto[]>(fileText)![0];

                _countsUpdateService.FinishTask(liveTaskDto);

                Console.WriteLine($"Changed: {e.FullPath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Changed: {ex.Message}");
            }
            finally
            {
                //File.Delete(e.FullPath);
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
