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
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public int AnalysePassports() => ScanPassports().Count(passport => ValidateRequiredFields(passport));

        public int AnalysePassportsWithRules()
        {
            return ScanPassports().Count(passport => ValidateRequiredFields(passport) && ValidateFieldRules(passport));
        }

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

        private bool ValidateRequiredFields(IReadOnlyDictionary<string, string> passport)
        {
            return _requiredFields.All(field => passport.ContainsKey(field));
        }

        private bool ValidateFieldRules(IReadOnlyDictionary<string, string> passport)
        {
            foreach (var (field, value) in passport)
            {
                var flag = field switch
                {
                    "byr" => Range(value, @"[0-9]{4}", 1920, 2002),
                    "iyr" => Range(value, @"[0-9]{4}", 2010, 2020),
                    "eyr" => Range(value, @"[0-9]{4}", 2020, 2030),
                    "hgt" => Range(value, @"([0-9]{3})cm", 150, 193) || Range(value, @"([0-9]{2})in", 59, 76),
                    "hcl" => Regex.Match(value, @"^#[0-9a-f]{6}$").Success,
                    "ecl" => "amb blu brn gry grn hzl oth".Split(" ").Contains(value),
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

            static bool Range(string st, string pattern, int min, int max)
            {
                var m = Regex.Match(st, "^" + pattern + "$");
                if (m.Success is false)
                {
                    return false;
                }

                var v = int.Parse(m.Groups[^1].Value);
                return v >= min && v <= max;
            }
        }
    }
}
