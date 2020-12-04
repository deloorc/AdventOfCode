using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core.Day01;
using AdventOfCode.Core.Day02;
using AdventOfCode.Core.Day03;
using AdventOfCode.Core.Day04;
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
            var input = File.ReadAllLines(path).Select(s => int.Parse(s)).ToArray();
            var repair = new ReportRepair(2020);

            // Part #1
            Console.WriteLine(repair.Fix(input, 2).ToString());

            // Part #2
            Console.WriteLine(repair.Fix(input, 3).ToString());
            break;
        }
    case 2:
        {
            var input = File.ReadAllLines(path);

            // Part #1
            Console.WriteLine(new PasswordPhilosophy(input).PartOne().ToString());

            // Part #2
            Console.WriteLine(new PasswordPhilosophy(input).PartTwo().ToString());
            break;
        }
    case 3:
        {
            var input = File.ReadLines(path);

            // Part #1
            Console.WriteLine(new TobogganTrajectory(input).AnalyseTrees(right: 3, down: 1).ToString());

            // Part #2
            Console.WriteLine(new TobogganTrajectory(input).AnalyseTrees(new (int right, int down)[]
            {
                (right: 1, down: 1),
                (right: 3, down: 1),
                (right: 5, down: 1),
                (right: 7, down: 1),
                (right: 1, down: 2)
            }).ToString());

            break;
        }
    case 4:
        {
            var input = File.ReadAllLines(path);

            // Part #1
            Console.WriteLine(new PassportProcessing(input).AnalysePassports().ToString());

            // Part #2
            Console.WriteLine(new PassportProcessing(input).AnalysePassportsWithRules().ToString());

            break;
        }
    default:
        break;
}

Console.WriteLine("Press any key...");
Console.ReadKey();
return 0;