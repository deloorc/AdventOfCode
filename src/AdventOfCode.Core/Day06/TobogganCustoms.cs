using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day06
{
    public class TobogganCustoms
    {
        private readonly IEnumerable<string> _forms;

        public TobogganCustoms(IEnumerable<string> forms) => _forms = forms;

        public int AnalyseAnswers()
            => ScanGroupDeclarations((result, next) =>
            {
                result.UnionWith(next);
                return result;
            }).Sum(result => result.Count);

        public int AnalyseGroupAnswers()
            => ScanGroupDeclarations((result, next) =>
             {
                 result.IntersectWith(next);
                 return result;
             })
            .Sum(result => result.Count);

        internal IEnumerable<HashSet<char>> ScanGroupDeclarations(Func<HashSet<char>, HashSet<char>, HashSet<char>> aggegrate)
        {
            var group = new LinkedList<HashSet<char>>();
            foreach (var answer in _forms)
            {
                if (string.IsNullOrEmpty(answer))
                {
                    yield return group.Aggregate(aggegrate);
                    group = new LinkedList<HashSet<char>>();
                }
                else
                {
                    group.AddLast(new HashSet<char>(answer));
                }
            }

            yield return group.Aggregate(aggegrate);
        }
    }
}
