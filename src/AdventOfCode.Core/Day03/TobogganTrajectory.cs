using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day03
{
    public class TobogganTrajectory
    {
        private const char TreeSymbol = '#';

        private readonly string[] _map;

        public TobogganTrajectory(IEnumerable<string> map)
            => _map = map.ToArray() ?? throw new ArgumentNullException(nameof(map));

        public int AnalyseTrees(int right, int down)
        {
            var treeCount = 0;
            var col = 0;

            for (int row = down; row < _map.Length; row += down)
            {
                var area = _map[row];

                col = (col + right) % area.Length;
                if (area[col] is TreeSymbol)
                {
                    treeCount++;
                }
            }

            return treeCount;
        }

        public long AnalyseTrees(IReadOnlyList<(int right, int down)> slopes)
            => slopes
            .Select(slope => AnalyseTrees(slope.right, slope.down))
            .Aggregate(1L, (result, count) => result * count);
    }
}
