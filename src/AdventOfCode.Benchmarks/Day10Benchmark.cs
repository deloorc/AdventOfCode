using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core.Day10;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    public class Day10Benchmark
    {
        private List<int> _jolts;

        [GlobalSetup()]
        public void Setup()
        {
            var input = File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{10.ToString("D2")}.txt"))
                .Select(int.Parse)
                .OrderBy(j => j);

            _jolts = new List<int> { 0 };
            _jolts.AddRange(input);
            _jolts.Add(_jolts[^1] + 3);
        }

        [Benchmark]
        public long BenchmarkDynamicProgramming() => AdapterArray.CalculateArrangementsDynamicProgramming(_jolts);

        [Benchmark]
        public long BenchmarkRecurssion() => AdapterArray.CalculateArrangementsRecurssion(_jolts);
    }
}
