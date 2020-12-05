using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core.Day04
{
    public class PassportProcessing
    {
        private static readonly HashSet<string> _requiredFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "byr", // Birth Year
            "iyr", // Issue Year
            "eyr", // Expiration Year
            "hgt", // Height
            "hcl", // Hair Color
            "ecl", // Eye Color
            "pid", // Passport ID
            //"cid"  // Country ID
        };

        private readonly string[] _input;

        public PassportProcessing(string[] input)
            => _input = input ?? throw new ArgumentNullException(nameof(input));

        public int AnalysePassports()
            => ScanPassports()
                .Count(passport => ValidateRequiredFields(passport));

        public int AnalysePassportsWithRules()
            => ScanPassports()
                .Count(passport => ValidateRequiredFields(passport) && ValidateFieldRules(passport));

        private IEnumerable<IReadOnlyDictionary<string, string>> ScanPassports()
        {
            for (int index = 0; index < _input.Length; index++)
            {
                var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                while (index < _input.Length && string.IsNullOrWhiteSpace(_input[index]) is false)
                {
                    foreach (var field in _input[index].Split(' '))
                    {
                        var kvp = field.Split(':');
                        if (kvp.Length is 2)
                        {
                            fields[kvp[0]] = kvp[1];
                        }
                    }

                    index++;
                }

                yield return fields;
            }
        }

        private static bool ValidateRequiredFields(IReadOnlyDictionary<string, string> passport)
            => _requiredFields
                .All(field => passport.ContainsKey(field));

        private static bool ValidateFieldRules(IReadOnlyDictionary<string, string> passport)
        {
            foreach (var (field, value) in passport)
            {
                var flag = field switch
                {
                    "byr" => int.TryParse(value, out var byr) && byr is >= 1920 and <= 2002,
                    "iyr" => int.TryParse(value, out var iyr) && iyr is >= 2010 and <= 2020,
                    "eyr" => int.TryParse(value, out var eyr) && eyr is >= 2020 and <= 2030,
                    "hgt" => Regex.Match(value, @"((1[5-8][0-9]|19[0-3])cm)|(7[0-6]|59|6[0-9])in").Success,
                    "hcl" => Regex.Match(value, @"^#[0-9a-f]{6}$").Success,
                    "ecl" => Regex.Match(value, @"(amb|blu|brn|gry|grn|hzl|oth)").Success,
                    "pid" => Regex.Match(value, @"^[0-9]{9}$").Success,
                    "cid" => true,
                    _ => false
                };

                if (flag is false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
