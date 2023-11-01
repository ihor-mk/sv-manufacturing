using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SunVita.Core.Common.DTO.DoneTask;

namespace SunVita.Worker.WebApi.Services
{
    public class DoneJobWatcherHostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var watcher = new FileSystemWatcher(@"d:\dev\donejobs");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;

            watcher.Filter = "*.json";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            while (!stoppingToken.IsCancellationRequested)
            { }
        }
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            using var streamReader = new StreamReader(e.FullPath);

            var fileText = streamReader.ReadToEnd();

            var doneTaskDto = JsonConvert.DeserializeObject<DoneTaskFileDto[]>(fileText);

            Console.WriteLine($"Changed: {e.FullPath}");
        }
    }
}
