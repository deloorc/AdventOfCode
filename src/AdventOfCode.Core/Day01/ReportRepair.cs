using System;
using System.Collections.Generic;

namespace AdventOfCode.Core.Day01
{
    public class ReportRepair
    {
        private readonly int _sum;

        public ReportRepair(int sum) => _sum = sum;

        public long Fix(IReadOnlyList<int> expenses, int k) => k switch
        {
            2 => HashSet(expenses),
            3 => NestedLoops(expenses),
            _ => throw new ArgumentOutOfRangeException(nameof(k))
        };

        public long NestedLoops(IReadOnlyList<int> expenses)
        {
            foreach (var i in expenses)
            {
                foreach (var j in expenses)
                {
                    foreach (var k in expenses)
                    {
                        if (i + j + k == _sum)
                        {
                            return i * j * k;
                        }
                    }
                }
            }

            throw new InvalidOperationException();
        }

        public long HashSet(IReadOnlyList<int> expenses)
        {
            var set = new HashSet<int>(expenses.Count);
            foreach (var expense in expenses)
            {
                if (expense > _sum)
                {
                    continue;
                }

                var remaining = _sum - expense;
                if (set.Contains(remaining))
                {
                    return remaining * expense;
                }

                set.Add(expense);
            }

            throw new InvalidOperationException();
        }
    }
}
