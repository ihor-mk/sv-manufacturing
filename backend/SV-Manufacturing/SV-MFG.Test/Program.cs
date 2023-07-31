using var client = new HttpClient();
var result = await client.GetStringAsync("http://10.61.2.21/selectjob.masp");

var data = result.Split('\n', StringSplitOptions.RemoveEmptyEntries);


Console.WriteLine(data.Length);

var str = data
    .Where(x => x.Contains("hostJobNameInput"))
    .ToList();

var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

var hostJobNameProperty = hostJobNameLine
    .Where(x => x.Contains("value"))
    .FirstOrDefault();


var name = str[1].Substring(81, str[1].Length - (81 + 53));

Console.WriteLine(name);
//Console.WriteLine(str[1].Length);
//Console.WriteLine(str[1]);
