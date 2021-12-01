using System.Collections.Generic;

namespace AdventOfCode.Core.Day01
{
    public sealed class SonarSweep
    {
        public static int ReportIncrease(IReadOnlyList<int> sweeps) => Sweep(sweeps, 1);

        public static int ReportThreeMeasurementIncrease(IReadOnlyList<int> sweeps) => Sweep(sweeps, 3);

        private static int Sweep(IReadOnlyList<int> sweeps, int windowSize)
        {
            var totalIncreases = 0;
            for (var index = windowSize; index < sweeps.Count; index++)
                if (sweeps[index] > sweeps[index - windowSize])
                    ++totalIncreases;

            return totalIncreases;
        }
    }
}
