using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Core.Day10
{
    public class AdapterArray
    {
        private readonly IEnumerable<int> _sortedAdapters;

        public AdapterArray(IEnumerable<string> input) => _sortedAdapters = input.Select(int.Parse).OrderBy(j => j);

        public int CalculateJoltsDifference()
        {
            var (_, one, three) = _sortedAdapters
                .Aggregate((jolt: 0, one: 0, three: 1), (r, jolt) => (jolt - r.jolt) switch
                {
                    1 => (jolt, r.one + 1, r.three),
                    3 => (jolt, r.one, r.three + 1),
                    _ => (jolt, r.one, r.three)
                });

            return one * three;
        }

        public long CalculateArrangements()
        {
            var jolts = new List<int> { 0 };
            jolts.AddRange(_sortedAdapters);
            jolts.Add(jolts[^1] + 3);

            return CalculateArrangementsDynamicProgramming(jolts);
        }

        internal static long CalculateArrangementsDynamicProgramming(List<int> jolts)
        {
            // https://en.wikipedia.org/wiki/Dynamic_programming
            // https://www.geeksforgeeks.org/count-number-of-ways-to-cover-a-distance/
            var (a, b, c) = (1L, 0L, 0L);
            for (int i = jolts.Count - 2; i >= 0; i--)
            {
                var r1 = i + 1 < jolts.Count && jolts[i + 1] - jolts[i] <= 3 ? a : 0;
                var r2 = i + 2 < jolts.Count && jolts[i + 2] - jolts[i] <= 3 ? b : 0;
                var r3 = i + 3 < jolts.Count && jolts[i + 3] - jolts[i] <= 3 ? c : 0;

                (a, b, c) = (r1 + r2 + r3, a, b);
            }

            return a;
        }

        internal static long CalculateArrangementsRecurssion(List<int> jolts)
        {
            return Recurssion(jolts, new long[jolts.Count], 0);

            static long Recurssion(List<int> jolts, long[] arrangements, int index)
            {
                if (index == jolts.Count - 1) return 1L;
                if (arrangements[index] != 0) return arrangements[index];

                var (count, length) = (0L, Math.Min(index + 3, jolts.Count - 1));
                for (var i = index + 1; i <= length; i++)
                    if (jolts[index] + 3 >= jolts[i])
                        count += Recurssion(jolts, arrangements, i);

                arrangements[index] = count;
                return count;
            }
        }
    }
}
