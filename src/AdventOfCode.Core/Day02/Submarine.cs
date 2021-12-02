using System;
using System.Collections.Generic;

namespace AdventOfCode.Core.Day02
{
    public class Submarine
    {
        public static long Dive(IReadOnlyList<string> commands)
        {
            var (x, y) = (0, 0);
            foreach (var command in commands)
            {
                var parsed = command.Split(' ');
                var (instruction, value) = (parsed[0], int.Parse(parsed[1]));

                if (string.Equals("forward", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    x += value;
                }

                if (string.Equals("down", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    y += value;
                }

                if (string.Equals("up", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    y -= value;
                }
            }

            return x * y;
        }

        public static long DiveWithAim(IReadOnlyList<string> commands)
        {
            var (x, y, aim) = (0, 0, 0);
            foreach (var command in commands)
            {
                var parsed = command.Split(' ');
                var (instruction, value) = (parsed[0], int.Parse(parsed[1]));

                if (string.Equals("forward", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    x += value;
                    y += (aim * value);
                }

                if (string.Equals("down", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    aim += value;
                }

                if (string.Equals("up", instruction, StringComparison.OrdinalIgnoreCase))
                {
                    aim -= value;
                }
            }

            return x * y;
        }
    }
}
