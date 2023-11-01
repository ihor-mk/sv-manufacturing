
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

Console.WriteLine("Press enter to exit.");
Console.ReadLine();

 static void OnChanged(object sender, FileSystemEventArgs e)
{
   
    Console.WriteLine($"Changed: {e.FullPath}");
}



