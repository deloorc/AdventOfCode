using AdventOfCode.Core.Day10;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest.Day10
{
    [TestClass]
    public class Day10Tests
    {
        [DataTestMethod]
        [DataRow(new[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, 35)]
        [DataRow(new[] { "28", "33", "18", "42", "31", "14", "46", "20", "48", "47", "24", "23", "49", "45", "19", "38",
                         "39", "11", "1", "32", "25", "35", "8", "17", "7", "9", "4", "2", "34", "10", "3"}, 220)]
        public void ExamplePartOne(string[] input, int expected)
        {
            // Arrange
            var adapter = new AdapterArray(input);

            // Act
            var result = adapter.CalculateJoltsDifference();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, 8)]
        [DataRow(new[] { "28", "33", "18", "42", "31", "14", "46", "20", "48", "47", "24", "23", "49", "45", "19", "38",
                         "39", "11", "1", "32", "25", "35", "8", "17", "7", "9", "4", "2", "34", "10", "3"}, 19208)]
        public void ExamplePartTwo(string[] input, int expected)
        {
            // Arrange
            var adapter = new AdapterArray(input);

            // Act
            var result = adapter.CalculateArrangements();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" }, 8)]
        [DataRow(new[] { "28", "33", "18", "42", "31", "14", "46", "20", "48", "47", "24", "23", "49", "45", "19", "38",
                         "39", "11", "1", "32", "25", "35", "8", "17", "7", "9", "4", "2", "34", "10", "3"}, 19208)]
        public void ExamplePartTwoRecurssion(string[] input, int expected)
        {
            // Arrange
            var adapter = new AdapterArray(input);

            // Act
            var result = adapter.CalculateArrangements();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
