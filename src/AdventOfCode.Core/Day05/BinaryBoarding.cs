using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day05
{
    public class BinaryBoarding
    {
        private readonly IEnumerable<string> _tickets;

        public BinaryBoarding(IEnumerable<string> tickets) => _tickets = tickets ?? throw new ArgumentNullException(nameof(tickets));

        public int SearchMax() => ScanLinq().max;

        public int SearchSeat()
        {
            var (min, max, ids) = ScanLinq();
            while (--max > min)
                if (ids.Remove(max) is false)
                    return max;

            throw new InvalidOperationException("Seat ID not found!");
        }

        /// <remarks>
        /// AOC exercise: "Every seat also has a unique seat ID, to find it multiply the row by 8, then add the column."
        /// 
        /// We could solve this via binary operations. Because a left arithmetic shift by n is equivalent to multiplying by 2n.
        /// In this case multiplying by 8 is basicly shifting 3 bits (e.g. 2³ = 8).
        /// 
        /// Example: 44 * 8 = 352.
        /// Binary: 0b_101100 << 3 = 0b_101100000 = 352
        /// 
        /// The final part is adding the binary value of the column.
        /// 
        /// Example: 352 + 5 = 357
        /// Binary: 0b_101100000 + 0b_0101 = 0b_101100101 = 357.
        /// 
        /// If we reflect on exercise we can map the characters to a binary value:
        /// Example: input = FBFBBFFRLR
        /// 
        /// FBFBBFF = 44 / RLR = 5
        /// 0101100 = 44 / 101 = 5
        /// </remarks>
        internal (int min, int max, HashSet<int> ids) ScanLinq()
            => _tickets
                .Select(ticket => string.Create(ticket.Length, ticket, (chars, state) =>
                {
                    for (int index = 0; index < state.Length; index++)
                    {
                        chars[index] = state[index] switch
                        {
                            'F' => '0',
                            'L' => '0',
                            'B' => '1',
                            'R' => '1',
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }
                }))
                .Select(binary => Convert.ToInt32(binary, 2))
                .Aggregate((min: int.MaxValue, max: int.MinValue, seats: new HashSet<int>()), (result, seatID) =>
                {
                    result.seats.Add(seatID);
                    return (
                        min: Math.Min(result.min, seatID),
                        max: Math.Max(result.max, seatID),
                        seats: result.seats
                    );
                });

        internal (int min, int max, HashSet<int> ids) ScanForeach()
        {
            // REMARK: 
            // Define min with max value since we want to search the minimum value, 
            // if we set it on 0 this will always be the minimum.
            var (min, max, seats) = (int.MaxValue, int.MinValue, new HashSet<int>());
            foreach (var ticket in _tickets)
            {
                var binaryTicket = string.Create(ticket.Length, ticket, (chars, state) =>
                {
                    for (int index = 0; index < state.Length; index++)
                    {
                        chars[index] = state[index] switch
                        {
                            'F' => '0',
                            'L' => '0',
                            'B' => '1',
                            'R' => '1',
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }
                });
                var id = Convert.ToInt32(binaryTicket, fromBase: 2);

                seats.Add(id);
                min = Math.Min(min, id);
                max = Math.Max(max, id);
            }

            return (min, max, seats);
        }
    }
}
