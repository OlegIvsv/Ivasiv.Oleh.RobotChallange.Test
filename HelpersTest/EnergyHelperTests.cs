using Ivasiv.Oleh.RobotClallange;
using Ivasiv.Oleh.RobotClallange.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace Ivasiv.Oleh.RobotChallange.Test.HelpersTest
{
    [TestClass]
    public class EnergyHelperTests
    {
        [TestMethod] //TODO:check if it's relevant
        public void GnergyToGetTo()
        {
            Position p1 = new Position(3, 4), p2 = new Position(98, 99);
            int expected = 50;

            int result = EnergyHelper.EnergyToGetTo(p1, p2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CanGetToWith()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { OwnerName= "owner1", Position = new Position(10, 10)},
                new Robot.Common.Robot() { OwnerName= "owner2", Position = new Position(15, 15)}
            };
            var myRobot = robots[0];
            Position newPosition = new Position(15, 15);
            int expected = 50 + Details.AttackEnergyLoss;

            int result = EnergyHelper.CanGetToWith(newPosition, robots, myRobot);

            Assert.AreEqual(expected, result);
        } //TODO:check if it's relevant

        //TODO: cover the rest of methods with tests
    }
}
