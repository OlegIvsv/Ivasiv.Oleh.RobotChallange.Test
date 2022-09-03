using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using Ivasiv.Oleh.RobotChallange;
using System;
using Ivasiv.Oleh.RobotClallange.Helpers;

namespace Ivasiv.Oleh.RobotChallange.Test.HelpersTest
{
    //TODO : change movement cost from calculation for a whole way to calculation for one step 
    [TestClass]
    public class DirectionHelperTest
    {
        [DataRow(47, 48, 48, 49)]
        [DataRow(48, 49, 49, 50)]
        [DataRow(49, 50, 50, 50)]

        [DataRow(48, 49, 49, 50)]
        [DataRow(49, 50, 50, 50)]
        [DataRow(49, 49, 50, 50)]
        [DataRow(47, 52, 48, 51)]
        [DataRow(48, 51, 49, 50)]
        [DataRow(49, 50, 50, 50)]

        [DataRow(51, 48, 50, 49)]
        [DataRow(50, 49, 50, 50)]

        [DataRow(52, 53, 51, 52)]
        [DataRow(51, 52, 50, 51)]
        [DataRow(50, 51, 50, 50)]
        [TestMethod]
        public void NextPosition(int x, int y, int expX, int expY)
        {
            Position end = new Position(50, 50),
                current = new Position(x, y),
                expected = new Position(expX, expY);


            Position res = DirectionHelper.NextPosition(current, end);


            Assert.AreEqual(expected, res);
        }
    }
}
