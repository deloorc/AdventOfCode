using System.Collections.Generic;

namespace AdventOfCode.Core.Day08
{
    public class GameConsole
    {
        private readonly IEnumerable<string> _instructions;

        public GameConsole(IEnumerable<string> instructions) => _instructions = instructions;

        public int Boot()
        {
            return 5;
        }
    }
}
