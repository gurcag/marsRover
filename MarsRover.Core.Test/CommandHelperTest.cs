using System.Collections.Generic;
using System.Text;
using Xunit;
using MarsRover.Core.Models;
using MarsRover.Core.Helpers;

namespace MarsRover.Core.Test
{
    public class CommandHelperTest
    {
        #region TryParseInputCommand
        [Fact]
        public void ValidInputCommand()
        {
            string inputCommand = new StringBuilder()
                .Append("5 5")
                .Append('\n')
                .Append("1 2 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("3 3 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.True(result);
            Assert.Equal("5 5", plateauUpperCoordinateCommand);
            Assert.Equal("1 2 N", initialRoverPositionCommands[0]);
            Assert.Equal("LMLMLMLMM", movementCommands[0]);
            Assert.Equal("3 3 E", initialRoverPositionCommands[1]);
            Assert.Equal("MMRMMRMRRM", movementCommands[1]);
        }

        [Fact]
        public void InvalidInputCommandMissingNewLine()
        {
            string inputCommand = new StringBuilder()
                .Append("5 5")
                .Append("1 2 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("3 3 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.False(result);
        }

        [Fact]
        public void InvalidInputCommandMuchNewLine()
        {
            string inputCommand = new StringBuilder()
                .Append("5 5")
                .Append('\n')
                .Append("1 2 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("3 3 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .Append('\n')
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.False(result);
        }

        [Fact]
        public void InvalidInputCommandInvalidRoverInitialPosition()
        {
            string inputCommand = new StringBuilder()
                .Append("5 5")
                .Append('\n')
                .Append("5 6 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("3 3 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.False(result);
        }

        [Fact]
        public void InvalidInputCommandInvalidRoverInitialPosition2()
        {
            string inputCommand = new StringBuilder()
                .Append("5 5")
                .Append('\n')
                .Append("1 2 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("-1 1 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.False(result);
        }

        [Fact]
        public void InvalidInputCommandInvalidPlateauUpperCoordinate()
        {
            string inputCommand = new StringBuilder()
                .Append("0 0")
                .Append('\n')
                .Append("1 2 N")
                .Append('\n')
                .Append("LMLMLMLMM")
                .Append('\n')
                .Append("3 3 E")
                .Append('\n')
                .Append("MMRMMRMRRM")
                .ToString();

            bool result = CommandHelper.TryParseInputCommand(inputCommand
                , out string plateauUpperCoordinateCommand
                , out IList<string> initialRoverPositionCommands
                , out IList<string> movementCommands);

            Assert.False(result);
        }
        #endregion

        #region IsValidPlateauUpperCoordinateCommand
        [Fact]
        public void ValidPlateauUpperCoordinateCommand()
        {
            string validCommand = "7 8";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.True(result);
        }

        [Fact]
        public void InvalidPlateauUpperCoordinateCommandWithoutWhiteSpace()
        {
            string validCommand = "78";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.False(result);
        }

        [Fact]
        public void InvalidPlateauUpperCoordinateCommandTooLongCommand()
        {
            string validCommand = "7 8 ";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.False(result);
        }

        [Fact]
        public void InvalidPlateauUpperCoordinateCommandTooLongCommand2()
        {
            string validCommand = " 7 8";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.False(result);
        }

        [Fact]
        public void InvalidPlateauUpperCoordinateCommandNonNumeric()
        {
            string validCommand = "a 8";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.False(result);
        }

        [Fact]
        public void InvalidPlateauUpperCoordinateCommandNonNumeric2()
        {
            string validCommand = "1 a";
            bool result = CommandHelper.IsValidPlateauUpperCoordinateCommand(validCommand);
            Assert.False(result);
        }
        #endregion

        #region IsValidInitialRoverPositionCommand
        [Fact]
        public void ValidInitialRoverPositionCommand()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "1 3 W";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.True(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandWithoutWhiteSpace()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "22E";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandTooLongCommand()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "2  1 N";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandTooLongCommand2()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = " 2 1 N";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandTooLongCommand3()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "2 1  N";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandTooLongCommand4()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "2 1 N ";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandNonNumeric()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "x 2 E";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandNonNumeric2()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "1 y W";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }

        [Fact]
        public void InvalidInitialRoverPositionCommandInvalidCompassDirection()
        {
            Coordinate plateauLowerCoordinate = new Coordinate(1, 1);
            Coordinate plateauUpperCoordinate = new Coordinate(3, 3);
            string command = "2 2 A";
            bool result = CommandHelper.IsValidInitialRoverPositionCommand(plateauLowerCoordinate, plateauUpperCoordinate, command);
            Assert.False(result);
        }
        #endregion

        #region IsValidCompassDirectionChar

        [Fact]
        public void ValidCompassDirectionChar()
        {
            Assert.True(CommandHelper.IsValidCompassDirectionCommand("W"));
        }

        [Fact]
        public void InvalidCompassDirectionChar()
        {
            Assert.False(CommandHelper.IsValidCompassDirectionCommand("X"));
        }
        #endregion

        #region IsValidMovementCommand
        [Fact]
        public void ValidMovementCommand()
        {
            Assert.True(CommandHelper.IsValidMovementCommand("LMRLMRLMR"));
        }

        [Fact]
        public void InvalidMovementCommandInvalidChar()
        {
            Assert.False(CommandHelper.IsValidMovementCommand("LMRXLMR"));
        }

        [Fact]
        public void InvalidMovementCommandWithWhiteSpace()
        {
            Assert.False(CommandHelper.IsValidMovementCommand("LMR LMR"));
        }

        [Fact]
        public void InvalidMovementCommandWithWhiteSpace2()
        {
            Assert.False(CommandHelper.IsValidMovementCommand(" LMRLMR"));
        }

        [Fact]
        public void InvalidMovementCommandWithWhiteSpace3()
        {
            Assert.False(CommandHelper.IsValidMovementCommand("LMRLMR "));
        }
        #endregion
    }
}
