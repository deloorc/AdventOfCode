using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode.Core.Day06;

namespace AdventOfCode.UnitTest.Day06
{
    [TestClass]
    public class Day06Tests
    {
        [DataTestMethod]
        [DataRow(new[] {
            "abc",
            "",
            "a",
            "b",
            "c",
            "",
            "ab",
            "ac",
            "",
            "a",
            "a",
            "a",
            "a",
            "",
            "b"
        }, 11)]
        public void ExamplePartOne(string[] input, int expected)
        {
            // Arrange
            var customs = new TobogganCustoms(input);

            // Act
            var sum = customs.AnalyseAnswers();

            // Assert
            Assert.AreEqual(expected, sum);
        }

        [DataTestMethod]
        [DataRow(new[] {
            "abc",
            "",
            "a",
            "b",
            "c",
            "",
            "ab",
            "ac",
            "",
            "a",
            "a",
            "a",
            "a",
            "",
            "b"
        }, 6)]
        public void ExamplePartTwo(string[] input, int expected)
        {
            // Arrange
            var customs = new TobogganCustoms(input);

            // Act
            var sum = customs.AnalyseGroupAnswers();

            // Assert
            Assert.AreEqual(expected, sum);
        }
    }
}
