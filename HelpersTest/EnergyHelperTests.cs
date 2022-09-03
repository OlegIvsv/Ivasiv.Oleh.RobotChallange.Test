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
        [TestMethod]
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
        }

        [DataRow(5, 5, 1)]
        [DataRow(40, 40, 1)]
        [DataRow(50, 10, 2)]
        [DataRow(99, 45, 1)]
        [TestMethod]
        public void MinEnergyFromHereToStation(int x, int y, int expectedStationIndex)
        {
            var map = new Map()
            {
                Stations = new List<EnergyStation>
                {
                    new EnergyStation(){ Position = new Position(10, 10)},
                    new EnergyStation(){ Position = new Position(10, 45)},
                    new EnergyStation(){ Position = new Position(45, 10)},
                }
            };
            var myRobot = new Robot.Common.Robot() { Position = new Position(x, y), OwnerName = "owner1" };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(45, 10), OwnerName = "owner2"},
                myRobot
            };
            int expected = EnergyHelper.CanGetToWith(map.Stations[expectedStationIndex].Position, robots, myRobot);


            int result = EnergyHelper.MinEnergyFromHereToStation(map, myRobot, robots);


            Assert.AreEqual(expected, result);
        }


        [DataRow(5, 5, 2)]
        [DataRow(40, 40, 2)]
        [DataRow(50, 10, 1)]
        [DataRow(99, 45, 2)]
        [TestMethod]
        public void ThenMinEnergyFromHereToStation(int x, int y, int expectedStationIndex)
        {
            var map = new Map()
            {
                Stations = new List<EnergyStation>
                {
                    new EnergyStation(){ Position = new Position(10, 10)},
                    new EnergyStation(){ Position = new Position(10, 45)},
                    new EnergyStation(){ Position = new Position(45, 10)},
                }
            };
            var myRobot = new Robot.Common.Robot() { Position = new Position(x, y), OwnerName = "owner1" };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(45, 10), OwnerName = "owner2"},
                myRobot
            };
            int expected = EnergyHelper.CanGetToWith(map.Stations[expectedStationIndex].Position, robots, myRobot);


            int result = EnergyHelper.ThenMinEnergyFromHereToStation(map, myRobot, robots);


            Assert.AreEqual(expected, result);
        }


        [DataRow(1, 40, 1)]
        [DataRow(10, 90, 2)]
        [DataRow(10, 9, 1)]
        [TestMethod]
        public void MostBeneficialStation(int x, int y, int expectedStationIndex)
        {
            var map = new Map()
            {
                Stations = new List<EnergyStation>
                {
                    new EnergyStation(){ Position = new Position(10, 10)},
                    new EnergyStation(){ Position = new Position(10, 40)},
                    new EnergyStation(){ Position = new Position(10, 70)},
                }
            };
            var myRobot = new Robot.Common.Robot() { Position = new Position(x, y), OwnerName = "owner1" };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(10, 70), OwnerName = "owner2"},
                myRobot
            };
            EnergyStation expected = map.Stations[expectedStationIndex];


            EnergyStation result = EnergyHelper.MostBenneficialStation(map, myRobot, robots);


            Assert.AreSame(expected, result);
        }
    }
}
