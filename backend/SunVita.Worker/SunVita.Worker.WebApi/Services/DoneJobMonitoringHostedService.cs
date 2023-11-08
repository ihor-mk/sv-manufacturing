using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SunVita.Core.Common.DTO.DoneTask;
using System.Text;
using System;

namespace SunVita.Worker.WebApi.Services
{
    public class DoneJobWatcherHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var watcher = new FileSystemWatcher(@"d:\dev\donejobs");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;

            watcher.Filter = "*.json";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            while (!stoppingToken.IsCancellationRequested)
            { }

            return Task.CompletedTask;
        }
        private static async void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                using var streamReader = new StreamReader(e.FullPath);

                var fileText = streamReader.ReadToEnd();

                var doneTaskDto = JsonConvert.DeserializeObject<DoneTaskFileDto[]>(fileText);

                var json = JsonConvert.SerializeObject(doneTaskDto![0]);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:5050/donetask";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);


                using var httpClient = new HttpClient();


                Console.WriteLine($"Changed: {e.FullPath}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Changed: {ex.Message}");
            }
            
        }
    }
}
