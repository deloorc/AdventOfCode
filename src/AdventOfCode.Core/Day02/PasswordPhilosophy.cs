using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day02
{
    public class PasswordPhilosophy
    {
        private readonly IEnumerable<string> _input;
        private readonly Func<PasswordPolicy, bool> _predicate;

        private record PasswordPolicy(int min, int max, char letter, string password);

        public PasswordPhilosophy(IEnumerable<string> input, CompanyPolicy policy)
        {
            _input = input;
            switch (policy)
            {
                case CompanyPolicy.Toboggan:
                    _predicate = policy =>
                    {
                        if (policy.password.Length < policy.max)
                        {
                            return false;
                        }

                        return (policy.password[policy.min - 1] == policy.letter) ^ (policy.password[policy.max - 1] == policy.letter);
                    };
                    break;
                case CompanyPolicy.SledRental:
                    _predicate = policy =>
                    {
                        var count = policy.password.Count(c => c.Equals(policy.letter));
                        return count >= policy.min && count <= policy.max;
                    };
                    break;
                default:
                    throw new ArgumentException(nameof(policy));
            }
        }

        public int Validate() => Parse(_input).Count(_predicate);

        private static IEnumerable<PasswordPolicy> Parse(IEnumerable<string> input)
        {
            foreach (var entry in input)
            {
                var parts = entry.Split(' ', StringSplitOptions.TrimEntries);
                var range = parts[0].Split('-').Select(int.Parse).ToArray();
                var letter = parts[1].First();
                var password = parts[2];

                yield return new PasswordPolicy(range[0], range[1], letter, password);
            }
        }

    }
}
