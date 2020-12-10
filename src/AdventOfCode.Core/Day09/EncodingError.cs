using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day09
{
    public class EncodingError
    {
        private readonly long[] _sequence;

        public EncodingError(IEnumerable<string> input)
            => _sequence = input.Select(long.Parse).ToArray();

        public long Preamble(int length) => FindError(length);

        public long Contiguous(int length) => FindContiguous(FindError(length));

        internal long FindError(int length)
        {
            var terms = new Queue<long>(length);
            foreach (var term in _sequence)
            {
                // NOTE: Fill the intial queue with the first preamble length before we begin to calculate the sumations.
                if (length is not 0 and not < 0)
                {
                    terms.Enqueue(term);
                    length--;
                    continue;
                }

                var sumFound = false;
                foreach (var n in terms)
                {
                    // NOTE: Skip the n terms that are higher of the given sum result.
                    if (term < n) continue;
                    if (terms.Contains(term - n))
                    {
                        terms.Dequeue();
                        terms.Enqueue(term);
                        sumFound = true;

                        break;
                    }
                }

                if (sumFound is false)
                    return term;
            }

            throw new InvalidProgramException();
        }

        internal long FindErrorWithCache(int length)
        {
            var (terms, cache) = (new Queue<long>(length), new HashSet<long>(length));
            foreach (var term in _sequence)
            {
                // NOTE: Fill the intial queue with the first preamble length before we begin to calculate the sumations.
                if (length is not 0 and not < 0)
                {
                    terms.Enqueue(term);
                    cache.Add(term);
                    length--;
                    continue;
                }

                var sumFound = false;
                foreach (var n in terms)
                {
                    // NOTE: Skip the n terms that are higher of the given sum result.
                    if (term < n) continue;
                    if (cache.Contains(term - n))
                    {
                        cache.Remove(terms.Dequeue());
                        terms.Enqueue(term);
                        cache.Add(term);
                        sumFound = true;

                        break;
                    }
                }

                if (sumFound is false)
                    return term;
            }

            throw new InvalidProgramException();
        }

        internal long FindContiguous(long sum)
        {
            for (int index = 0; index < _sequence.Length; index++)
            {
                var current = _sequence[index];
                for (int increment = index + 1; increment < _sequence.Length - index + 1; increment++)
                {
                    current += _sequence[increment];
                    if (current > sum)
                        break;

                    if (current == sum)
                    {
                        var (min, max) = _sequence[index..(increment + 1)]
                            .Aggregate(
                                seed: (min: long.MaxValue, max: long.MinValue),
                                func: (r, n) => (min: Math.Min(r.min, n), max: Math.Max(r.max, n)));

                        return min + max;
                    }
                }
            }

            throw new InvalidProgramException();
        }
    }
}