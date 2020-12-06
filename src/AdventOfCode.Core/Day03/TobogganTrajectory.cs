using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day03
{
    public class TobogganTrajectory
    {
        private const char TreeSymbol = '#';

        private readonly IEnumerable<string> _map;

        public TobogganTrajectory(IEnumerable<string> map) => _map = map ?? throw new ArgumentNullException(nameof(map));

        public int AnalyseMap(int right, int down)
            => AnalyseMapArray(right, down);

        public long AnalyseMap(IReadOnlyList<(int right, int down)> slopes)
            => slopes
                .Select(slope => AnalyseMapArray(slope.right, slope.down))
                .Aggregate(1L, (result, count) => result * count);

        internal int AnalyseMapArray(int right, int down)
        {
            if (down is 0)
                throw new ArgumentOutOfRangeException(nameof(down));

            var map = _map switch
            {
                string[] => (string[])_map,
                _ => _map.ToArray()
            };

            // NOTE: index should be one-based in order to correctly calculate the 'symbol' index in the map line.
            // NOTE: assumption that each line has the same length!
            var (row, index, length, treeCount) = (down, 1, map[0].Length, 0);
            for (; row < map.Length; row += down, index++)
                if (map[row][(index * right) % length] is TreeSymbol)
                    treeCount++;

            return treeCount;
        }

        internal int AnalyseMapSequence(int right, int down)
        {
            if (down is 0)
                throw new ArgumentOutOfRangeException(nameof(down));

            using var enumerator = _map.GetEnumerator();
            if (enumerator.MoveNext() is false)
                return default;

            // NOTE: assumption that each line has the same length!
            var (treeCount, index, length) = (0, 0, enumerator.Current.Length);
            while (Move(enumerator, down))
                if (enumerator.Current[(right * ++index) % length] is TreeSymbol)
                    treeCount++;

            return treeCount;

            static bool Move(in IEnumerator<string> enumerator, int skip)
            {
                // NOTE: 'shorter' # code lines.
                // do if (enumerator.MoveNext() is false) return false; while (--skip > 0);

                for (; skip > 0; --skip)
                    if (enumerator.MoveNext() is false)
                        return false;

                return true;
            }
        }
    }
}
