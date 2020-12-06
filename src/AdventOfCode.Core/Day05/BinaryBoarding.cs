using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day05
{
    public class BinaryBoarding
    {
        private readonly IEnumerable<string> _input;

        public BinaryBoarding(IEnumerable<string> input) => _input = input ?? throw new ArgumentNullException(nameof(input));

        public int SearchMax() => Scan().Max();

        public int SearchSeat()
        {
            var ids = Scan()
                .OrderBy(id => id);

            var (min, max) = (ids.Min(), ids.Max());

            return Enumerable.Range(min, max - min + 1).Except(ids).Single();
        }

        private IEnumerable<int> Scan() => _input
           .Select(s => s.Select(c => c switch
           {
               'F' => '0',
               'L' => '0',
               'B' => '1',
               'R' => '1',
               _ => throw new ArgumentOutOfRangeException()
           }))
           .Select(chars => new string(chars.ToArray()))
           .Select(binary => Convert.ToInt32(binary, 2));
    }
}
