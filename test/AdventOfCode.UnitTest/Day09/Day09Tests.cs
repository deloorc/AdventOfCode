using AdventOfCode.Core.Day09;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest.Day09
{
    [TestClass]
    public class Day09Tests
    {
        [DataTestMethod]
        [DataRow(new[] {
           "35",
           "20",
           "15",
           "25",
           "47",
           "40",
           "62",
           "55",
           "65",
           "95",
           "102",
           "117",
           "150",
           "182",
           "127",
           "219",
           "299",
           "277",
           "309",
           "576"
        }, 5, 127)]
        public void ExamplePartOne(string[] input, int length, int expected)
        {
            // Arrange
            var error = new EncodingError(input);

            // Act
            var result = error.Preamble(length);

            // Assert
            Assert.AreEqual(expected, result);
        }
        [DataTestMethod]
        [DataRow(new[] {
           "35",
           "20",
           "15",
           "25",
           "47",
           "40",
           "62",
           "55",
           "65",
           "95",
           "102",
           "117",
           "150",
           "182",
           "127",
           "219",
           "299",
           "277",
           "309",
           "576"
        }, 5, 62)]
        public void ExamplePartTwo(string[] input, int length, int expected)
        {
            // Arrange
            var error = new EncodingError(input);

            // Act
            var result = error.Contiguous(length);

            // Assert
            Assert.AreEqual(expected, result);
        }

    }
}
