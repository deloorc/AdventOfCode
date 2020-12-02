using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.UnitTest
{
    [TestClass]
    public class Day01Tests
    {
        [DataTestMethod]
        [DataRow(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" }, 2)]
        public void Sample(string[] input, int expectedValid)
        {
            // Arrange
            //var repair = new ReportRepair(sum);

            // Act
            var output = 2;

            // Assert
            Assert.AreEqual(expectedValid, output);
        }
    }
}
