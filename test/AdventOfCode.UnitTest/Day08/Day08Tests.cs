using AdventOfCode.Core.Day08;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest.Day08
{
    [TestClass]
    public class Day08Tests
    {
        [DataTestMethod]
        [DataRow(new[] {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6"
        }, 5)]
        public void ExamplePartOne(string[] input, int expected)
        {
            // Arrange
            var gameConsole = new GameConsole(input);

            // Act
            var accumulator = gameConsole.Boot();

            // Assert
            Assert.AreEqual(expected, accumulator);
        }

        [DataTestMethod]
        [DataRow(new[] {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6"
        }, 8)]
        public void ExamplePartTwo(string[] input, int expected)
        {
            // Arrange
            var gameConsole = new GameConsole(input);

            // Act
            var accumulator = gameConsole.Repair();

            // Assert
            Assert.AreEqual(expected, accumulator);
        }
    }
}
