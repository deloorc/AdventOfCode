using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core.Day07
{
    public class HandyHaversacks
    {
        private const string REGEX_KEY_GROUP = "key";
        private const string REGEX_TOTAL_GROUP = "total";
        private const string REGEX_BAG_GROUP = "bag";

        /// <remarks>
        /// Assumption is that the color is always 2 words. \w+ \w+ will search for "word <space> word"
        /// </remarks>
        private static readonly Regex _bagPattern =
            new Regex(
                pattern: @$"^(?'{REGEX_KEY_GROUP}'\w+ \w+)|(?'{REGEX_TOTAL_GROUP}'\d+) (?'{REGEX_BAG_GROUP}'\w+ \w+)",
                options: RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly IEnumerable<string> _rules;

        public HandyHaversacks(IEnumerable<string> rules) => _rules = rules;

        public int ContainsBag(string term)
        {
            var indice = BagRulesInverseIndice();
            var roots = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var colors = new Stack<string>();
            colors.Push(term);

            // NOTE: Transitive closure.
            while (colors.Count is not 0)
            {
                var color = colors.Pop();
                if (roots.Contains(color))
                    continue;

                roots.Add(color);

                if (indice.TryGetValue(color, out var inventory))
                    foreach (var bag in inventory)
                        colors.Push(bag);
            }

            // NOTE: roots will contain the 'term' e.g. 'shiny gold', it can be excluded from the final count.
            return roots.Count - 1;
        }

        public long CalculateBagContent(string term)
        {
            var (indice, bags, sum) = (BagRulesIndice(), new Stack<(string color, long amount)>(), 0L);

            bags.Push((color: term, amount: 1));
            while (bags.Count is not 0)
            {
                var (color, amount) = bags.Pop();
                if (indice.TryGetValue(color, out var inventory))
                {
                    foreach (var bag in inventory)
                    {
                        bags.Push((bag.color, amount * bag.amount));
                        sum += bags.Peek().amount;
                    }
                }
            }

            return sum;
        }

        private IReadOnlyDictionary<string, HashSet<string>> BagRulesInverseIndice()
        {
            var indice = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var rule in _rules)
            {
                var matches = _bagPattern.Matches(rule);
                if (matches.Count is 0)
                    continue;

                var key = matches.First().Groups[REGEX_KEY_GROUP].Value;
                for (var index = 1; index < matches.Count; index++)
                {
                    var bag = matches[index].Groups[REGEX_BAG_GROUP].Value;
                    if (indice.TryGetValue(bag, out var bags))
                    {
                        bags.Add(key);
                    }
                    else
                    {
                        indice[bag] = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                        {
                            key
                        };
                    }
                }
            }

            return indice;
        }

        private IReadOnlyDictionary<string, IReadOnlyCollection<(string color, long amount)>> BagRulesIndice()
        {
            var indice = new Dictionary<string, IReadOnlyCollection<(string color, long amount)>>(StringComparer.OrdinalIgnoreCase);
            foreach (var rule in _rules)
            {
                var matches = _bagPattern.Matches(rule);
                if (matches.Count is 0)
                    continue;

                var bags = new List<(string color, long amount)>(matches.Count - 1);
                for (var index = 1; index < matches.Count; index++)
                {
                    bags.Add((
                        color: matches[index].Groups[REGEX_BAG_GROUP].Value,
                        amount: long.Parse(matches[index].Groups[REGEX_TOTAL_GROUP].Value)));
                }

                var key = matches.First().Groups[REGEX_KEY_GROUP].Value;
                indice[key] = bags switch
                {
                    { Count: 0 } => Array.Empty<(string, long)>(),
                    _ => bags.ToArray()
                };
            }

            return indice;
        }
    }
}
