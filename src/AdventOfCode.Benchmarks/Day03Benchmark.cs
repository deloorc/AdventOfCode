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
    public class Day03enchmarkOps
    {
        private TobogganTrajectory _tobogganTrajectory;

        [GlobalSetup(Target = nameof(BenchmarkArray))]
        public void ReadAllLines()
            => _tobogganTrajectory = new TobogganTrajectory(
                 map: File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt")));

        [GlobalSetup(Target = nameof(BenchmarkSequence))]
        public void ReadLines()
          => _tobogganTrajectory = new TobogganTrajectory(
               map: File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt")));

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkArray(int right, int down) => _tobogganTrajectory.AnalyseMap(right, down);

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkSequence(int right, int down) => _tobogganTrajectory.AnalyseMapSequence(right, down);

        public IEnumerable<object[]> Slopes()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 3, 1 };
            yield return new object[] { 5, 1 };
            yield return new object[] { 7, 1 };
            yield return new object[] { 1, 2 };
        }
    }

    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    [BenchmarkCategory("Day03")]
    public class Day03enchmarkArray
    {
        private string[] _map;

        [GlobalSetup(Target = nameof(BenchmarkArray))]
        public void ReadAllLines()
            => _map = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt"));

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkArray(int right, int down) => new TobogganTrajectory(_map).AnalyseMap(right, down);

        public IEnumerable<object[]> Slopes()
        {
            yield return new object[] { 1, 1 };
            yield return new object[] { 3, 1 };
            yield return new object[] { 5, 1 };
            yield return new object[] { 7, 1 };
            yield return new object[] { 1, 2 };
        }
    }

    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    [BenchmarkCategory("Day03")]
    public class Day03enchmarkSequence
    {
        private IEnumerable<string> _map;

        [GlobalSetup(Target = nameof(BenchmarkSequence))]
        public void ReadLines()
          => _map = File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{3.ToString("D2")}.txt"));

        [Benchmark]
        [ArgumentsSource(nameof(Slopes))]
        public long BenchmarkSequence(int right, int down) => new TobogganTrajectory(_map).AnalyseMapSequence(right, down);

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
