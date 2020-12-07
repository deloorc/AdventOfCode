﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core.Day07
{
    public class HandyHaversacks
    {
        /// <remarks>
        /// Assumption is that the color is always 2 words. [a-z]+ [a-z]+ will search for "word <space> word"
        /// </remarks>
        private static readonly Regex _bagPattern =
            new Regex(@"(^[a-z]+ [a-z]+)|(\d+) ([a-z]+ [a-z]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private readonly IEnumerable<string> _rules;

        public HandyHaversacks(IEnumerable<string> rules) => _rules = rules;

        public int Contains(string bag)
        {
            return SearchBag(bag);
        }

        internal int SearchBag(string bag)
        {
            var graph = ProcessRules();
            return Closure(bag,
                    b => graph.TryGetValue(b, out var childeren)
                        ? childeren
                        : Enumerable.Empty<string>()).Count() - 1;
        }

        private IReadOnlyDictionary<string, HashSet<string>> ProcessRules()
        {
            var graph = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var rule in _rules)
            {
                var matches = _bagPattern.Matches(rule);
                if (matches.Count is not > 1)
                {
                    continue;
                }

                var key = matches[0].Value;
                foreach (var match in matches.Skip(1).Select(m => m.Groups[3].Value))
                {
                    if (graph.TryGetValue(match, out var value))
                    {
                        value.Add(key);
                    }
                    else
                    {
                        graph[match] = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                        {
                            key
                        };
                    }
                }
            }

            return graph;
        }

        public static IEnumerable<T> Closure<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            stack.Push(root);

            while (stack.Count != 0)
            {
                T item = stack.Pop();
                if (seen.Contains(item))
                    continue;

                seen.Add(item);
                yield return item;
                foreach (var child in children(item))
                {
                    stack.Push(child);
                }
            }
        }
    }
}
