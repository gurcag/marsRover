using System;
using System.Text;

namespace MarsRover.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mars Rover Code Case Study by Gurcag Yaman");

            string inputCommand = GetInputString();

            Console.WriteLine("\ninput is :\n" + inputCommand);

            var response = MarsRover.Core.Repositories.CommandController.GetCommandController().ExecuteCommand(inputCommand);

            Console.WriteLine("\noutput is:");
            Console.WriteLine(response.IsSuccess ? response.Output : response.ErrorMessage);

            Console.ReadLine();
        }

        static string GetInputString()
        {
            return new StringBuilder()
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
        }

        // TODO : apply dependency injection
        // NOTE : MS DependencyInjection library can not resolve classic approach of singleton pattern
        // https://espressocoder.com/2019/01/03/implementing-the-singleton-pattern-in-asp-net-core/

        //static void ResolveDependencies()
        //{
        //    var serviceProvider = new ServiceCollection()
        //        .AddSingleton<ICommandController, CommandController>()
        //        .AddSingleton<IRoverController, RoverController>()
        //        .BuildServiceProvider();

        //    var commandControllerService = serviceProvider.GetService<ICommandController>();
        //    var roverControllerService = serviceProvider.GetService<IRoverController>();
        //}
    }
}
