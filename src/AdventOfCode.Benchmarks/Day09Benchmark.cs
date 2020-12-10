using System;
using System.IO;
using AdventOfCode.Core.Day09;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    public class Day09Benchmark
    {
        private EncodingError _program;

        [GlobalSetup()]
        public void Setup()
            => _program = new EncodingError(File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{9.ToString("D2")}.txt")));

        [Benchmark()]
        public long BenchmarkFindErrorWithCache() => _program.FindErrorWithCache(25);

        [Benchmark(Baseline = true)]
        public long BenchmarkFindError() => _program.FindError(25);
    }
}
