
using var client = new HttpClient();
DateTime timeStart = DateTime.Now;

while (true)
{
    Console.WriteLine("--------------------------------------------------");
    timeStart = DateTime.Now;

    var resultNomenc = await client.GetStringAsync("http://10.61.2.21/selectjob.masp");
    Console.WriteLine($"timer - {DateTime.Now - timeStart}");

    var resultStat = await client.GetStringAsync("http://10.61.2.21/updatestatistics.masp");
    Console.WriteLine($"timer - {DateTime.Now - timeStart}");

    if (resultStat != null)
    {
        var data = resultStat.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (data != null)
        {
            var preCountString = data.Where(x => x.Contains("Качественная партия")).FirstOrDefault();

            int count;

            if (preCountString != null)
            {
                var stringIndex = Array.IndexOf(data, preCountString);

                var countString = data[++stringIndex]
                    .Split("\t", StringSplitOptions.RemoveEmptyEntries)[0]
                    .Split("\"", StringSplitOptions.RemoveEmptyEntries)[0];

                count = int.Parse(countString);

                Console.WriteLine($"count - {count}");
            }

            data = resultNomenc.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var str = data
                .Where(x => x.Contains("hostJobNameInput"))
                .ToList();

            var hostJobNameLine = str[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var hostJobNameProperty = hostJobNameLine
                .Where(x => x.Contains("value"))
                .FirstOrDefault();


            var name = str[1].Substring(81, str[1].Length - (81 + 53));

            Console.WriteLine(name);
        }
    }


    Console.WriteLine($"timer - {DateTime.Now - timeStart}");
    Thread.Sleep(5000);
}



