using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using Ivasiv.Oleh.RobotChallange;
using System;
using Ivasiv.Oleh.RobotClallange.Helpers;
using System.Runtime.InteropServices;
using Ivasiv.Oleh.RobotClallange;

namespace Ivasiv.Oleh.RobotChallange.Test.HelpersTest
{
    [TestClass]
    public class DirectionHelperTest
    {

        [DataRow(101, 1, 1, 1)]
        [DataRow(1, 105, 1, 5)]
        [DataRow(-1, -5, 99, 95)]
        [TestMethod()]
        public void GetPositionCorrectedTest(int x, int y, int newX, int newY)
        {
            Position endPossition = new Position(x, y);
            Position expected = new Position(newX, newY);

            Position result = DirectionHelper.GetPositionCorrected(endPossition);

            Assert.AreEqual(expected, result);
        }

        [DataRow(99, 1, 2)]
        [DataRow(0, 49, 49)]
        [DataRow(0, 51, 49)]
        [TestMethod()]
        public void GetXDistanceTest(int x1, int x2, int distance)
        {
            Position startPosition = new Position(x1, 1);
            Position endPosition = new Position(x2, 1);
            int expected = distance;

            int result = DirectionHelper.GetXDistance(startPosition, endPosition);

            Assert.AreEqual(expected, result);
        }

        [DataRow(5, 5, 31, 2)]
        [DataRow(10, 10, 54, 3)]
        [DataRow(99, 99, 8, 1)]
        [DataRow(99, 99, 5, 2)]
        [DataRow(45, 45, 89, 44)]
        [DataRow(66, 1, 65, 35)]
        [DataRow(45, 1, 44, 44)]
        [TestMethod()]
        public void GetStepNumberTest(int x, int y, int energy, int stepNumberExpected)
        {
            Position startPosition = new Position(1, 1);
            Position endPosition = new Position(x, y);
            int expected = stepNumberExpected;

            int result = DirectionHelper.FindStepNumber(startPosition, endPosition, energy);

            Assert.AreEqual(expected, result);
        }

        [DataRow(6, 6, 31, 4, 4)]
        [DataRow(99, 99, 16, 0, 0)]
        [DataRow(3, 3, 200, 3, 3)]
        [DataRow(4, 4, 8, 4, 4)]
        [DataRow(45, 45, 86, 3, 3)]
        [DataRow(45, 2, 88, 4, 2)]
        [TestMethod()]
        public void NextPosition(int x, int y, int energy, int newX, int newY)
        {
            var startPositionRobot = new Robot.Common.Robot()
            {
                Position = new Position(2, 2),
                Energy = energy
            };
            Position endPosition = new Position(x, y);
            Position expected = new Position(newX, newY);


            Position result = DirectionHelper.NextPosition(startPositionRobot, endPosition);


            Assert.AreEqual(expected, result);
        }
    }
}
