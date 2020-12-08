using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core.Day08
{
    public class GameConsole
    {
        private readonly IEnumerable<string> _codeFile;

        public GameConsole(IEnumerable<string> codeFile) => _codeFile = codeFile;

        public int Boot() => Run(Compile()).accumulator;

        public int Repair()
        {
            var program = Compile();
            foreach (var patchedProgram in Patch(program))
            {
                var (local_accumulator, terminate) = Run(patchedProgram);
                if (terminate)
                {
                    return local_accumulator;
                }
            }

            throw new InvalidProgramException();
        }

        private static (int accumulator, bool terminate) Run((OptCode optcode, int value)[] program)
        {
            var (index, accumulator, cache) = (0, 0, new HashSet<int>());
            while (index < program.Length || index is < 0)
            {
                if (cache.Add(index) is false)
                    break;

                var (optcode, value) = program[index];
                switch (optcode)
                {
                    case OptCode.Jmp: index += value; continue;
                    case OptCode.Acc: accumulator += value; break;
                    case OptCode.Nop:
                    default: break;
                }

                index++;
            }

            return (accumulator, index == program.Length);
        }

        private static IEnumerable<(OptCode optcode, int value)[]> Patch((OptCode optcode, int value)[] program)
        {
            for (var pointer = 0; pointer < program.Length; pointer++)
            {
                if (program[pointer].optcode is OptCode.Acc)
                    continue;

                // NOTE: Generate the program so that it terminates normally by changing exactly one jmp (to nop) or nop (to jmp).
                //       Only change the current programm line to generate every possible 'patched' program to find 
                //       the correct solution.
                yield return program
                    .Select((instruction, p) => p != pointer
                        ? instruction
                        : instruction.optcode switch
                        {
                            OptCode.Jmp => (OptCode.Nop, instruction.value),
                            OptCode.Nop => (OptCode.Jmp, instruction.value),
                            _ => throw new InvalidProgramException()
                        })
                    .ToArray();
            }
        }

        private (OptCode optcode, int value)[] Compile()
        {
            var operations = _codeFile switch
            {
                string[] a => new List<(OptCode, int)>(a.Length),
                ICollection<string> c => new List<(OptCode, int)>(c.Count),
                _ => new List<(OptCode, int)>()
            };

            foreach (var line in _codeFile)
            {
                var instruction = line.Split(' ', StringSplitOptions.TrimEntries);
                operations.Add((
                    Enum.Parse<OptCode>(instruction[0], true),
                    int.Parse(instruction[1])));
            }

            return operations.ToArray();
        }

        private enum OptCode
        {
            Jmp,
            Acc,
            Nop
        }
    }
}
