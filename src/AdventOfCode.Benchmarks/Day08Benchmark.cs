using System;
using System.IO;
using AdventOfCode.Core.Day08;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    public class Day08Benchmark
    {
        private (GameConsole.OptCode optcode, int value)[] _program;

        [GlobalSetup]
        public void Setup()
            => _program = new GameConsole(File.ReadLines(Path.Combine(AppContext.BaseDirectory, $"App_Data/{8.ToString("D2")}.txt"))).Compile();

        [Benchmark]
        public int BenchmarkBruteForce()
            => GameConsole.RepairWithBruteForce(_program);

        [Benchmark]
        public int BenchmarkWithInlinePatch()
            => GameConsole.RepairWithInlinePatch(_program);
    }

}
