using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core.Day05;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    public class Day05Benchmark
    {
        private BinaryBoarding _scanner;

        [GlobalSetup]
        public void Setup()
            => _scanner = new BinaryBoarding(File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{5.ToString("D2")}.txt")));

        [Benchmark]
        public (int min, int max, HashSet<int> ids) BenchmarkLinq() 
            => _scanner.ScanLinq();

        [Benchmark]
        public (int min, int max, HashSet<int> ids) BenchmarkHashSet() 
            => _scanner.ScanForeach();
    }

}
