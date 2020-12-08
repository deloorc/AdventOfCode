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

        public int Repair() => RepairWithInlinePatch(Compile());

        internal static int RepairWithInlinePatch((OptCode optcode, int value)[] program)
        {
            for (var pointer = 0; pointer < program.Length; pointer++)
            {
                var instruction = program[pointer];
                if (instruction.optcode is OptCode.Acc)
                    continue;

                // Patch the current instruction.
                program[pointer] = Swap(instruction);

                var (accumulator, terminated) = Run(program);
                if (terminated)
                    return accumulator;

                // Since our 'patch' is a simple swap we can repeat the process to restore the original instruction.
                program[pointer] = Swap(program[pointer]);
            }

            throw new InvalidProgramException();

            static (OptCode optcode, int value) Swap((OptCode optcode, int value) instruction) => instruction.optcode switch
            {
                OptCode.Jmp => (OptCode.Nop, instruction.value),
                OptCode.Nop => (OptCode.Jmp, instruction.value),
                _ => throw new InvalidProgramException()
            };
        }

        internal static int RepairWithBruteForce((OptCode optcode, int value)[] program)
        {
            foreach (var patched in Patch(program))
            {
                var (accumulator, terminated) = Run(patched);
                if (terminated)
                    return accumulator;
            }

            throw new InvalidProgramException();

            static IEnumerable<(OptCode optcode, int value)[]> Patch((OptCode optcode, int value)[] program)
            {
                for (var pointer = 0; pointer < program.Length; pointer++)
                {
                    if (program[pointer].optcode is OptCode.Acc)
                        continue;

                    // NOTE: Generate the program so that it terminates normally by changing exactly one jmp (to nop) or nop (to jmp).
                    //       Only change the current code line to generate every possible 'patched' program to find the correct solution.
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
        }

        private static (int accumulator, bool terminated) Run((OptCode optcode, int value)[] program)
        {
            var (pointer, accumulator, cache) = (0, 0, new HashSet<int>());
            while (pointer < program.Length || pointer is < 0)
            {
                if (cache.Add(pointer) is false)
                    break;

                var (optcode, value) = program[pointer];
                switch (optcode)
                {
                    case OptCode.Jmp: pointer += value; continue;
                    case OptCode.Acc: accumulator += value; break;
                    case OptCode.Nop:
                    default: break;
                }

                pointer++;
            }

            return (accumulator, terminated: pointer == program.Length);
        }

        internal (OptCode optcode, int value)[] Compile()
        {
            var instructions = _codeFile switch
            {
                string[] a => new List<(OptCode, int)>(a.Length),
                ICollection<string> c => new List<(OptCode, int)>(c.Count),
                _ => new List<(OptCode, int)>()
            };

            foreach (var line in _codeFile)
            {
                var instruction = line.Split(' ', StringSplitOptions.TrimEntries);
                instructions.Add((
                    Enum.Parse<OptCode>(instruction[0], ignoreCase: true),
                    int.Parse(instruction[1])));
            }

            return instructions.ToArray();
        }

        internal enum OptCode
        {
            Jmp,
            Acc,
            Nop
        }
    }
}
