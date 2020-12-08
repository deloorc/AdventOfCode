using AdventOfCode.Core.Day09;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest.Day09
{
    [TestClass]
    public class Day09Tests
    {
        [DataTestMethod]
        [DataRow(new[] {
           ""
        }, 5)]
        public void ExamplePartOne(string[] input, int expected)
        {
            // Arrange
            var x = new Day09Puzzle(input);
            // Act

            // Assert
            //Assert.AreEqual(expected, x);
        }
    }
}
