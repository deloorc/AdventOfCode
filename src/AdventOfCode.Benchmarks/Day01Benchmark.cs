using AdventOfCode.Core.Day01;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace AdventOfCode.Benchmarks
{
    [MemoryDiagnoser]
    [NativeMemoryProfiler]
    public class Day01Benchmark
    {
        private readonly ReportRepair _repair = new ReportRepair(2020);
        private readonly int[] _input = new[] { 1721, 979, 366, 299, 675, 1456 };

        [Benchmark]
        public long BenchmarkNestedLoops() => _repair.NestedLoops(_input);

        [Benchmark()]
        public long BenchmarkHashset() => _repair.HashSet(_input);
    }
}
