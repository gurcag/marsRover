using System;
using System.Text;
using MarsRover.Core.Exceptions;
using Xunit;
using MarsRover.Core.Repositories;

namespace MarsRover.Core.Test
{
    public class CommandControllerTest
    {
        [Fact]
        public void ValidExecuteCommand()
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

            var response = CommandController.GetCommandController().ExecuteCommand(inputCommand);

            string expectedOutput = "1 3 N\r\n5 1 E";

            Assert.True(response.IsSuccess);
            Assert.True(string.IsNullOrEmpty(response.ErrorMessage));
            Assert.Equal(expectedOutput, response.Output);
        }

        [Fact]
        public void ExecuteCommandThrowsCommandException()
        {
            string inputCommand = new StringBuilder()
               .Append("5 5")
               .Append('\n')
               .Append("6 2 N")
               .Append('\n')
               .Append("LMLMLMLMM")
               .ToString();

            Action action = () => CommandController.GetCommandController().ExecuteCommand(inputCommand);

            Assert.Throws<CommandException>(action);
        }

        [Fact]
        public void ExecuteCommandOutsideBoundaryMovement()
        {
            string inputCommand = new StringBuilder()
               .Append("5 5")
               .Append('\n')
               .Append("3 3 N")
               .Append('\n')
               .Append("MMMMMMMMMMMMM")
               .ToString();

            var response = CommandController.GetCommandController().ExecuteCommand(inputCommand);

            string expectedOutput = "3 5 N";

            Assert.True(response.IsSuccess);
            Assert.True(string.IsNullOrEmpty(response.ErrorMessage));
            Assert.Equal(expectedOutput, response.Output);
        }
    }
}
