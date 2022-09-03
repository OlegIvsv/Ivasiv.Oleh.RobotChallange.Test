using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Robot.Tournament;
using Ivasiv.Oleh.RobotClallange;
using Robot.Common;
using Ivasiv.Oleh.RobotClallange.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Ivasiv.Oleh.RobotChallange.Test
{
    [TestClass]
    public class OlehIvasivAlgorithmTest
    {
        public class Algorithm : OlehIvasivAlgorithm
        {
            public CreateNewRobotCommand IfCreateNewRobot(Map map, Robot.Common.Robot myRobot, List<Robot.Common.Robot> robots)
                => base.IfCreateNewRobot(map, myRobot, robots);

            protected int CanCreateChildWithEnergy(Map map, Robot.Common.Robot myRobot, List<Robot.Common.Robot> robots)
                => base.CanCreateChildWithEnergy(map, myRobot, robots);

            public int EnergyToSuriveMyself(Map map, Robot.Common.Robot myRobot, List<Robot.Common.Robot> robots)
                => EnergyToSuriveMyself(map, myRobot, robots);

            public RobotCommand IfLookForEnergy(Map map, Robot.Common.Robot myRobot, List<Robot.Common.Robot> robots)
                => base.IfLookForEnergy(map, myRobot, robots);  
        }


        [TestMethod]
        [DataRow(10, 40, typeof(CollectEnergyCommand))]
        [DataRow(11, 40, typeof(MoveCommand))]
        [DataRow(11, 40, typeof(MoveCommand))]
        public void IfLookForEnergy(int x, int y, Type commandType)
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


            var result = new Algorithm().IfLookForEnergy(map, myRobot, robots);


           Assert.IsInstanceOfType(result, commandType);
        }

        //TODO : Uncompleted test IfCreteChild
        [TestMethod]
        [DataRow(10, 40, true)]
        [DataRow(11, 40, false)]
        [DataRow(11, 40, true)]
        public void IfCreteChild(int x, int y, bool resultIsComman)
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
            var myRobot = new Robot.Common.Robot() { 
                Position = new Position(x, y), 
                OwnerName = "owner1" 
            };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(10, 70), OwnerName = "owner2"},
                myRobot
            };
            myRobot.Energy = EnergyHelper.CanGetToWith(map.Stations[2].Position, robots, myRobot);


            var result = new Algorithm().IfCreateNewRobot(map, myRobot, robots) != null;


            Assert.AreEqual(resultIsComman, result);
        }

        //TODO : Uncompleted test IfCreteChild_ChildEnergy
        [TestMethod]
        [DataRow(10, 40)]
        [DataRow(11, 40)]
        [DataRow(11, 40)]
        public void IfCreteChild_ChildEnergy(int x, int y)
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
            var myRobot = new Robot.Common.Robot()
            {
                Position = new Position(x, y),
                OwnerName = "owner1"
            };
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot(){ Position = new Position(10, 10), OwnerName = "owner1"},
                new Robot.Common.Robot(){ Position = new Position(10, 70), OwnerName = "owner2"},
                myRobot
            };


            myRobot.Energy = EnergyHelper.CanGetToWith(
                map.Stations[2].Position,
                robots,
                new Robot.Common.Robot { Position = map.FindFreeCell(myRobot.Position, robots) }
                );
            int result = new Algorithm().IfCreateNewRobot(map, myRobot, robots).NewRobotEnergy;


            Assert.AreEqual(myRobot.Energy, result);
        }
    }
}
