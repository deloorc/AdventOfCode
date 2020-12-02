using AdventOfCode.Core.Day01;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest.Day01
{
    [TestClass]
    public class Day01Tests
    {
        [DataTestMethod]
        [DataRow(2020, 2, new[] { 1721, 979, 366, 299, 675, 1456 }, 514579, DisplayName = "Part One")]
        [DataRow(2020, 3, new[] { 1721, 979, 366, 299, 675, 1456 }, 241861950, DisplayName = "Part Two")]
        public void Example(int sum, int k, int[] input, long expected)
        {
            // Arrange
            var repair = new ReportRepair(sum);

            // Act
            var output = repair.Fix(input, k);

            // Assert
            Assert.AreEqual(expected, output);
        }
    }
}
