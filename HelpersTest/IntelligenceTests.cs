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
        [DataRow(10, 10, true)]
        [DataRow(25, 25, false)]
        [TestMethod()]
        public void IsStationFreeTest(int x, int y, bool expectedRes)
        {
            var station = new EnergyStation() { Position = new Position(x, y) };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };
            
            bool expected = expectedRes;
            bool result = Intelligence.IsFreeStation(station, robots[0], robots);

            Assert.AreEqual(expected, result);
        }

        [DataRow(10, 10, true)]
        [DataRow(25, 25, false)]
        [TestMethod()]
        public void IsCellFreeTest(int x, int y, bool expectedRes)
        {
            Position cell = new Position(x, y);
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot() { Position = new Position(50, 50) },
                new Robot.Common.Robot() { Position = new Position(75, 75) },
                new Robot.Common.Robot() { Position = new Position(25, 25) }
            };
            
            bool expected = expectedRes;
            bool result = Intelligence.IsFreeCell(cell, robots[0], robots);

            Assert.AreEqual(expected, result);
        }

        [DataRow(10, 10, 0)]
        [DataRow(5, 5, null)]
        [TestMethod]
        public void TheRobotOnAStationTest(int robotX, int robotY, int? stationIndex)
        {
            var station = new EnergyStation() { Position = new Position(10, 10) };
            var map = new Map() { Stations = new List<EnergyStation> { station } };
            
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(robotX, robotY)}
            };
            
            var expected = stationIndex == null ? null : map.Stations.ElementAt(stationIndex.Value);
            var result = Intelligence.TheRobotOnAStation(map, robots, robots[0]);

            Environment.FailFast("Oleh give me a task");

            Assert.AreEqual(result, expected);
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
