using System;
using MarsRover.Core.Interfaces;
using MarsRover.Core.Helpers;
using MarsRover.Core.Exceptions;
using MarsRover.Core.Models;
using System.Collections.Generic;

namespace MarsRover.Core.Repositories
{
    //[ScopedDependency]
    public class CommandController : ICommandController
    {
        static ICommandController instance;
        static readonly object locker = new object();

        private CommandController() { }

        public static ICommandController GetCommandController()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new CommandController();
                    }
                }
            }

            return instance;
        }

        public CommandExecutionResponseModel ExecuteCommand(string command)
        {
            CommandExecutionResponseModel responseModel = new CommandExecutionResponseModel();

            if (CommandHelper.TryParseInputCommand(command, out string plateauInitializationCommand, out IList<string> roverInitializationCommands, out IList<string> movementCommands))
            {
                Coordinate plateauUpperCoordinate = CommandHelper.GetCoordinateFromPlateauUpperCoordinateCommand(plateauInitializationCommand);

                // ConcretePlateau will be initiliaze
                var plateau = ConcretePlateau.GetPlateau(plateauUpperCoordinate);

                try
                {
                    // each rover will be deployed sequentially
                    for (int i = 0; i < roverInitializationCommands.Count; i++)
                    {
                        IRover rover = RoverController.GetRoverController().Deploy(plateau, roverInitializationCommands[i]);
                    }

                    // each rover will be moved sequentially
                    for (int i = 0; i < RoverController.GetRoverController().RoverRepository.Count; i++)
                    {
                        RoverController.GetRoverController().ExecuteMovement(plateau, RoverController.GetRoverController().RoverRepository[i], movementCommands[i]);
                    }
                }
                catch (CommandException commandException)
                {
                    responseModel.IsSuccess = false;
                    responseModel.ErrorMessage = commandException.Message;
                }
                catch (Exception)
                {
                    responseModel.IsSuccess = false;
                    responseModel.ErrorMessage = "Fatal Error!";
                }

                responseModel.IsSuccess = true;
                responseModel.Output = CommandHelper.GetOutputStringFinalRoverPositions(RoverController.GetRoverController().RoverRepository);

                return responseModel;
            }
            else
            {
                throw new CommandException("Invalid Input command");
            }
        }
    }
}
