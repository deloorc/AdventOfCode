using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Core.Day03;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    [BenchmarkCategory("Day03")]
    public class Day03Benchmark
    {
        private TobogganTrajectory _tobogganTrajectory;

        [GlobalSetup(Targets = new[] { nameof(BenchmarkArrayAllLines), nameof(BenchmarkSequenceAllLines) })]
        public void ReadAllLines()
            => _tobogganTrajectory = new TobogganTrajectory(
                 map: File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt")));

        [GlobalSetup(Targets = new[] { nameof(BenchmarkArrayLines), nameof(BenchmarkSequenceLines) })]
        public void ReadLines()
          => _tobogganTrajectory = new TobogganTrajectory(
               map: File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt")));

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkArrayAllLines(int right, int down) => _tobogganTrajectory.AnalyseMapArray(right, down);

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkSequenceAllLines(int right, int down) => _tobogganTrajectory.AnalyseMapSequence(right, down);

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkArrayLines(int right, int down) => _tobogganTrajectory.AnalyseMapArray(right, down);

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkSequenceLines(int right, int down) => _tobogganTrajectory.AnalyseMapSequence(right, down);

        public IEnumerable<object[]> Slopes()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 3, 1 };
            yield return new object[] { 5, 1 };
            yield return new object[] { 7, 1 };
            yield return new object[] { 1, 2 };
        }
    }
}
