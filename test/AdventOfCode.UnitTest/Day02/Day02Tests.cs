using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode.Core.Day02;

namespace AdventOfCode.UnitTest
{
    [TestClass]
    public class Day02Tests
    {
        [DataTestMethod]
        [DataRow(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" }, CompanyPolicy.SledRental, 2)]
        [DataRow(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" }, CompanyPolicy.Toboggan, 1)]
        public void Sample(string[] input, CompanyPolicy policy, int expectedValid)
        {
            // Arrange
            var passwordPhilosphy = new PasswordPhilosophy(input, policy);

            // Act
            var output = passwordPhilosphy.Validate();

            // Assert
            Assert.AreEqual(expectedValid, output);
        }
    }
}
