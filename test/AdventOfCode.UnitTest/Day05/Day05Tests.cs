using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode.Core.Day05;

namespace AdventOfCode.UnitTest.Day05
{
    [TestClass]
    public class Day05Tests
    {
        [DataTestMethod]
        [DataRow("BFFFBBFRRR", 567)]
        [DataRow("FFFBBBFRRR", 119)]
        [DataRow("BBFFBBFRLL", 820)]
        public void ExamplePartOne(string input, int expected)
        {
            // Arrange
            var scanner = new BinaryBoarding(new[] { input });

            // Act
            var max = scanner.SearchMax();

            // Assert
            Assert.AreEqual(expected, max);
        }
    }
}
