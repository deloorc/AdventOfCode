﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode.Core.Day03;

namespace AdventOfCode.UnitTest.Day03
{
    [TestClass]
    public class Day03Tests
    {
        [DataTestMethod]
        [DataRow(new[]
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#",
        }, 3, 1, 7)]
        public void ExamplePartOne_Array(string[] input, int right, int down, int expectedTrees)
        {
            // Arrange
            var trajectory = new TobogganTrajectory(input);

            // Act
            var output = trajectory.AnalyseMap(right, down);

            // Assert
            Assert.AreEqual(expectedTrees, output);
        }

        [DataTestMethod]
        [DataRow(new[]
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#",
        }, 336)]
        public void ExamplePartTwo_Array(string[] input, int expectedTrees)
        {
            // REMARK: It seems we cannot use named tuple array in DataRowAttribute.
            var slopes = new (int right, int down)[]
            {
                 (right: 1, down: 1),
                 (right: 3, down: 1),
                 (right: 5, down: 1),
                 (right: 7, down: 1),
                 (right: 1, down: 2)
            };

            // Arrange
            var trajectory = new TobogganTrajectory(input);

            // Act
            var output = trajectory.AnalyseMap(slopes);

            // Assert
            Assert.AreEqual(expectedTrees, output);
        }

        [DataTestMethod]
        [DataRow(new[]
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#",
        }, 3, 1, 7)]
        public void ExamplePartOne_Sequence(string[] input, int right, int down, int expectedTrees)
        {
            // Arrange
            var trajectory = new TobogganTrajectory(input);

            // Act
            var output = trajectory.AnalyseMapSequence(right, down);

            // Assert
            Assert.AreEqual(expectedTrees, output);
        }

        [DataTestMethod]
        [DataRow(new[]
        {
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#",
        }, 336)]
        public void ExamplePartTwo_Sequence(string[] input, int expectedTrees)
        {
            // REMARK: It seems we cannot use named tuple array in DataRowAttribute.
            var slopes = new (int right, int down)[]
            {
                 (right: 1, down: 1),
                 (right: 3, down: 1),
                 (right: 5, down: 1),
                 (right: 7, down: 1),
                 (right: 1, down: 2)
            };

            // Arrange
            var trajectory = new TobogganTrajectory(input);

            // Act
            var output = trajectory.AnalyseMapSequence(slopes);

            // Assert
            Assert.AreEqual(expectedTrees, output);
        }
    }
}