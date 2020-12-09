using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode.Core.Day09
{
    public class EncodingError
    {
        private readonly IEnumerable<string> _input;

        public EncodingError(IEnumerable<string> input) => _input = input;

        public long Preamble(int length)
        {
            var numbers = _input.Select(s => long.Parse(s)).ToArray();
            return FindError(length, numbers);
        }

        public long Contiguous(int length)
        {
            var numbers = _input.Select(s => long.Parse(s)).ToArray();
            var error = FindError(length, numbers);

            for (int index = 0; index < numbers.Length; index++)
            {
                var current = numbers[index];

                for (int next = index + 1; next < numbers.Length - index + 1; next++)
                {
                    current += numbers[next];
                    if (current > error)
                        break;

                    if (current == error)
                    {
                        var (min, max) = numbers[index..(next + 1)]
                            .Aggregate(
                                seed: (min: long.MaxValue, max: long.MinValue),
                                func: (r, n) => (min: Math.Min(r.min, n), max: Math.Max(r.max, n)));

                        return min + max;
                    }
                }
            }

            throw new InvalidProgramException();
        }

        internal static long FindError(int preambleLength, long[] sequence)
        {
            for (int i = sequence.Length - 1; i >= 0; i--)
            {
                var n = sequence[i];
                var k = new HashSet<long>();
                var o = new HashSet<long>();

                for (int j = 1; j <= preambleLength; j++)
                {
                    var m = i - j;
                    if (m is < 0)
                    {
                        break;
                    }

                    var x = sequence[m];
                    o.Add(x);
                    k.Add(n - x);
                }

                o.IntersectWith(k);
                if (o.Count is 0)
                {
                    return n;
                }
            }

            throw new InvalidProgramException();
        }
    }
}
