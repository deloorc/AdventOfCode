using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day02
{
    public class PasswordPhilosophy
    {
        private readonly IEnumerable<string> _input;

        private record PasswordPolicy(int Min, int Max, char Letter, string Password);

        public PasswordPhilosophy(IEnumerable<string> input) => _input = input ?? throw new ArgumentNullException(nameof(input));

        public int PartOne() => Parse(_input).Count(policy =>
        {
            var count = policy.Password.Count(c => c.Equals(policy.Letter));
            return count >= policy.Min && count <= policy.Max;
        });

        public int PartTwo() => Parse(_input).Count(policy =>
        {
            if (policy.Password.Length < policy.Max)
            {
                return false;
            }

            return (policy.Password[policy.Min - 1] == policy.Letter) ^ (policy.Password[policy.Max - 1] == policy.Letter);
        });

        private static IEnumerable<PasswordPolicy> Parse(IEnumerable<string> input)
        {
            foreach (var entry in input)
            {
                var parts = entry.Split(' ');
                var range = parts[0].Split('-').Select(int.Parse).ToArray();
                var letter = parts[1].First();
                var password = parts[2];

                yield return new PasswordPolicy(range[0], range[1], letter, password);
            }
        }
    }
}
