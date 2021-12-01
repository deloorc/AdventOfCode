using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core.Day01;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddCommandLine(args, new Dictionary<string, string> { { "-d", "day" } }).Build();
var day = config.GetValue<int>("day");
var path = Path.Combine(AppContext.BaseDirectory, $"App_Data/{day.ToString("D2")}.txt");
if (File.Exists(path) is false)
{
    return -1;
}

switch (day)
{
    case 1:
        {
          
        }
    default:
        break;
}

Console.WriteLine("Press any key...");
Console.ReadKey();
return 0;