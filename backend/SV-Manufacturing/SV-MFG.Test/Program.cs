using var client = new HttpClient();
var result = await client.GetStringAsync("http://10.61.2.21/selectjob.masp");
//http://10.61.2.21/selectjob.masp

//Console.WriteLine(result);

var data = result.Split('\n', StringSplitOptions.RemoveEmptyEntries);

http://10.61.2.21/selectjob.masp


Console.WriteLine(data.Length);

var str = data
    .Where(x => x.Contains("hostJobNameInput"))
    .ToList();

Console.WriteLine(  str.Count);
Console.WriteLine(str[1]);