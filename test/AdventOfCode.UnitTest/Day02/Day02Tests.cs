using AdventOfCode.Core.Day02;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest
{
    [TestClass]
    public class Day02Tests
    {
        [DataTestMethod]
        [DataRow(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" }, 2)]
        public void ExamplePartOne(string[] input, int expectedValid)
        {
            // Arrange
            var passwordPhilosphy = new PasswordPhilosophy(input);

            // Act
            var output = passwordPhilosphy.PartOne();

            // Assert
            Assert.AreEqual(expectedValid, output);
        }

        [DataTestMethod]
        [DataRow(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" }, 1)]
        public void ExamplePartTwo(string[] input, int expectedValid)
        {
            // Arrange
            var passwordPhilosphy = new PasswordPhilosophy(input);

            // Act
            var output = passwordPhilosphy.PartTwo();

            // Assert
            Assert.AreEqual(expectedValid, output);
        }
    }
}
