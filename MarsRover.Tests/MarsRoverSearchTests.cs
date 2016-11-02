using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using MarsRover;

namespace MarsRover.Tests
{
    [TestFixture]
    public class MarsRoverSearchTests
    {
        [Test]
        public void Valid_Inputs_Return_Expected_Outputs()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");
            testList.Add("3 3 E");
            testList.Add("MMRMMRMRRM");
            MarsRover.MarsRoverService service = new MarsRoverService();

            List<string> actual = service.NavigateRovers(testList, false);

            string expected1 = "1 3 N";
            string expected2 = "5 1 E";

            Assert.IsTrue(actual[0] == expected1);
            Assert.IsTrue(actual[1] == expected2);
        }

        [Test]
        public void Error_Invalid_Input_Lines()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 2 N");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidCommandLines);
        }

        [Test]
        public void Error_Invalid_Plateau_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("55");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidPlateauCoordinates);
        }

        [Test]
        public void Error_Invalid_Plateau_X_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add(" 5");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidPlateauXCoordinate);
        }

        [Test]
        public void Error_Invalid_Plateau_Y_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 ");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidPlateauYCoordinate);
        }

        [Test]
        public void Error_Negative_Plateau_X_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("-5 5");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidPlateauXCoordinate);
        }

        [Test]
        public void Error_Negative_Plateau_Y_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 -5");
            testList.Add("1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidPlateauYCoordinate);
        }

        [Test]
        public void Error_Invalid_Rover_Starting_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartCoordinates);
        }

        [Test]
        public void Error_Invalid_Rover_Starting_X_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("X 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartXCoordinate);
        }

        [Test]
        public void Error_Invalid_Rover_Starting_Y_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 X N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartYCoordinate);
        }

        [Test]
        public void Error_Negative_Rover_Starting_X_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("-1 2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartXCoordinate);
        }

        [Test]
        public void Error_Negative_Rover_Starting_Y_Coordinates()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 -2 N");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartYCoordinate);
        }

        [Test]
        public void Error_Invalid_Rover_Starting_Direction()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 2 X");
            testList.Add("LMLMLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidRoverStartDirection);
        }

        [Test]
        public void Error_Invalid_Navigation_Command()
        {
            List<string> testList = new List<string>();
            testList.Add("5 5");
            testList.Add("1 2 N");
            testList.Add("LMLXLMLMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.InvalidNavigationCommand);
        }

        [Test]
        public void Error_Invalid_MoveX()
        {
            List<string> testList = new List<string>();
            testList.Add("2 2");
            testList.Add("1 2 N");
            testList.Add("LMMMMMMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.MoveOutOfBounds);
        }
        [Test]
        public void Error_Invalid_MoveY()
        {
            List<string> testList = new List<string>();
            testList.Add("2 2");
            testList.Add("1 2 N");
            testList.Add("MMMMMMM");

            MarsRover.MarsRoverService service = new MarsRoverService();

            var ex = Assert.Throws<RoverException>(() => service.NavigateRovers(testList, false));
            Assert.AreEqual(ex.ExceptionReason, RoverExceptionReason.MoveOutOfBounds);
        }

    }
}
