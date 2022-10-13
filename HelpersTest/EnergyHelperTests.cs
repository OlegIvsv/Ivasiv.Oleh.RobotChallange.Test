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
        [DataRow(3, 3, 2)]
        [DataRow(3, 4, 5)]
        [DataRow(2, 1, 1)]
        [DataRow(98, 2, 16)]
        [DataRow(98, 97, 41)]
        [TestMethod] 
        public void EnergyToGetToTest(int x, int y, int energy)
        {
            Position p1 = new Position(2, 2);
            Position p2 = new Position(x, y);
            int expected = energy;

            int result = EnergyHelper.EnergyToGetTo(p1, p2);

            Assert.AreEqual(expected, result);
        }

        [DataRow(1, 1, 18)]
        [DataRow(50, 50, 4608)]
        [DataRow(99, 99, 2)]
        [DataRow(98, 99, 1)]
        [DataRow(2, 2, 32 + Details.AttackEnergyLoss)]
        [TestMethod]
        public void CanGetToWithTest(int x, int y, int energy)
        {
            var myRobot = new Robot.Common.Robot() { OwnerName = "owner1", Position = new Position(98, 98) };
            var robots = new List<Robot.Common.Robot>
            {
                myRobot,
                new Robot.Common.Robot() { OwnerName= "owner2", Position = new Position(2, 2)}
            };
            Position newPosition = new Position(x, y);
            int expected = energy;


            int result = EnergyHelper.CanGetToWith(newPosition, robots, myRobot);


            Assert.AreEqual(expected, result);
        }

        [DataRow(1, 1, 1)]
        [DataRow(40, 40, 3)]
        [DataRow(6, 6, 0)]
        [DataRow(7, 7, 3)]
        [DataRow(0, 98, 1)]
        [TestMethod]
        public void MostBenneficialStationTest(int x, int y, int stationIndex)
        {
            Map map = new Map()
            {
                Stations = new List<EnergyStation>
                {
                    new EnergyStation() { Position = new Position(1,1)},
                    new EnergyStation() { Position = new Position(98,98)},
                    new EnergyStation() { Position = new Position(50,50)},
                    new EnergyStation() { Position = new Position(12,12)},
                }
            };
            var myRobot = new Robot.Common.Robot { Position = new Position(x, y) };
            var robots = new List<Robot.Common.Robot>
            {
                myRobot,
                new Robot.Common.Robot{Position = new Position(50, 50)}
            };


            var result = EnergyHelper.MostBenneficialStation(map, myRobot, robots);


            Assert.AreSame(map.Stations[stationIndex], result);
        }

        [DataRow(5, 5, 6 + Details.EnergyLossToCreateNewRobot, true)]
        [DataRow(5, 5, 5 + Details.EnergyLossToCreateNewRobot, false)]
        [DataRow(90, 90, 100 + Details.EnergyLossToCreateNewRobot, true)]
        [DataRow(90, 90, 56 + Details.EnergyLossToCreateNewRobot, true)]
        [DataRow(95, 1, 8 + Details.EnergyLossToCreateNewRobot, true)]
        [DataRow(95, 1, 7 + Details.EnergyLossToCreateNewRobot, false)]
        [TestMethod]
        public void ChildCanSurviveTest(int x, int y, int energy, bool canSurvive)
        {
            Map map = new Map()
            {
                MaxPozition = new Position(99,99),
                MinPozition = new Position(0, 0),
                
                Stations = new List<EnergyStation>
                {
                    new EnergyStation() { Position = new Position(1,1)},
                    new EnergyStation() { Position = new Position(98,98)},
                    new EnergyStation() { Position = new Position(50,50)},
                    new EnergyStation() { Position = new Position(12,12)},
                }
            };
            var myRobot = new Robot.Common.Robot { Position = new Position(x, y), Energy = energy };
            var robots = new List<Robot.Common.Robot>
            {
                myRobot,
                new Robot.Common.Robot{Position = new Position(50, 50)}
            };


            var result = EnergyHelper.ChildCanSurvive(map, myRobot, robots);


            Assert.AreEqual(canSurvive, result);
        }
    }
}
