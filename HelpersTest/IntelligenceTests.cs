using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ivasiv.Oleh.RobotClallange.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Ivasiv.Oleh.RobotClallange.Helpers.Tests
{
    [TestClass()]
    public class IntelligenceTests
    {
        [TestMethod()]
        public void FindDistanceTest()
        {
            Position startPossition = new Position(1, 1),
                endPossition = new Position(5, 9);
            int expected = 80;

            int result = Intelligence.FindDistance(startPossition, endPossition);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FindNearestFreeStationTest()
        {
            var map = new Map();
            map.Stations = new List<EnergyStation>
            {
                new EnergyStation() { Position = new Position(80, 80) },
                new EnergyStation() { Position = new Position(40, 40) },
                new EnergyStation() { Position = new Position(20, 20) }
            };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(20, 20) },
                new Robot.Common.Robot() { Position = new Position(40, 40) }
            };
            var expected = new Position(20, 20);
            
            var result = Intelligence.FindNearestFreeStation(robots[0], map, robots);

            Assert.AreEqual(expected, result);
        }


        [TestMethod()]
        public void IsStationFreeTest_Free()
        {
            var station = new EnergyStation() { Position = new Position(10, 10) };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };

            bool result = Intelligence.IsFreeStation(station, robots[0], robots);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsStationFreeTest_Occupated()
        {
            var station = new EnergyStation() { Position = new Position(25, 25) };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };

            bool result = Intelligence.IsFreeStation(station, robots[0], robots);

            Assert.IsFalse(result);
        }


        [TestMethod()]
        public void IsCellFreeTest_Occupied()
        {
            Position cell = new Position(75, 75);
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(75, 75) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };

            bool result = Intelligence.IsFreeCell(cell, robots[0], robots);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsCellFreeTest_Free()
        {
            Position cell = new Position(10, 10);
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(75, 75) },
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };

            bool result = Intelligence.IsFreeCell(cell, robots[0], robots);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TheRobotOnAStation_True()
        {
            var station = new EnergyStation() { Position = new Position(10, 10) };
            var map = new Map() { Stations = new List<EnergyStation> { station } };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = station.Position}
            };

            var result = Intelligence.TheRobotOnAStation(map, robots, robots[0]);

            Assert.AreEqual(result, station);
        }

        [TestMethod]
        public void TheRobotOnAStation_False()
        {

            var station = new EnergyStation() { Position = new Position(10, 10) };
            var map = new Map() { Stations = new List<EnergyStation> { station } };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(5, 5)}
            };

            var result = Intelligence.TheRobotOnAStation(map, robots, robots[0]);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Enemies()
        {
            var map = new Map();
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(5, 5), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner2"},
                new Robot.Common.Robot(){ Position = new Position(15, 15), OwnerName = "owner1"}
            };
            var myRobot = new Robot.Common.Robot() { Position=new Position(20, 20), OwnerName = "owner2"};
            var expected = new List<Robot.Common.Robot> { robots[0], robots[2] };

            var result = Intelligence.Enemies(robots, myRobot);

            CollectionAssert.AreEquivalent(result, expected);
        }

        [TestMethod]
        public void Family()
        {
            var map = new Map();
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(5, 5), OwnerName = "owner2"},
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(15, 15), OwnerName = "owner2"}
            };
            var myRobot = new Robot.Common.Robot() { Position = new Position(20, 20), OwnerName = "owner2" };
            var expected = new List<Robot.Common.Robot> { robots[0], robots[2] };

            var result = Intelligence.Family(robots, myRobot);

            CollectionAssert.AreEquivalent(result, expected);
        }

        [TestMethod]
        public void StationsCanBeOccupied_True()
        {
            var map = new Map()
            {
                Stations = new List<EnergyStation>
                {
                    new EnergyStation(){ Position = new Position(2, 2)},
                    new EnergyStation(){ Position = new Position(4, 4)},
                    new EnergyStation(){ Position = new Position(6, 6)},
                }
            };
            var myRobot = new Robot.Common.Robot() { Position = new Position(1, 1), OwnerName = "owner1" };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(2, 2), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(6, 6), OwnerName = "owner2"}
            };
            var expected = new List<EnergyStation> { map.Stations[1], map.Stations[2] };


            var result = Intelligence.StationsCanBeOccupied(map, robots, myRobot);


            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}