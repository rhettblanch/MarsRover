using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRover
{
    public class MarsRoverService
    {
        private List<string> _output = new List<string>();
        private int _plateauX;
        private int _plateauY;
        private int _roverX = 0;
        private int _roverY = 0;
        private Directions _facing;
        private bool _debugMode = false;

        public List<string> NavigateRovers(List<string> commandLines, bool isDebugMode)
        {
            _debugMode = false;

            if (commandLines.Count < 3)
            {
                throw new RoverException("At least 3 lines of commands must be supplied", RoverExceptionReason.InvalidCommandLines);
            }

            
            SetPlateau(commandLines[0]);

            for (int i = 1; i < commandLines.Count; i++)
            {
                if (i % 2 == 1)
                {
                    SetStartingPosition(commandLines[i]);
                }
                else
                {
                    Navigate(commandLines[i]);
                }
            }
            return _output;
        }

        private void SetPlateau(string command)
        {

            string[] field = command.Split(' ');

            if (field.Length != 2)
            {
                throw new RoverException("Plateau coordinates are invalid", RoverExceptionReason.InvalidPlateauCoordinates);
            }

            if (!NumberUtility.IsPositiveInteger(field[0]))
            {
                throw new RoverException("Plateau x coordinate is invalid", RoverExceptionReason.InvalidPlateauXCoordinate);
            }
            _plateauX = Convert.ToInt32(field[0]);

            if (!NumberUtility.IsPositiveInteger(field[1]))
            {
                throw new RoverException("Plateau y coordinate is invalid", RoverExceptionReason.InvalidPlateauYCoordinate);
            }
            _plateauY = Convert.ToInt32(field[1]);
            ConsoleDebug(string.Format("SetPlateau: x={0}, y={1}", _plateauX, _plateauY));

        }

        private void SetStartingPosition(string command)
        {
            string[] field = command.Split(' ');

            if (field.Length != 3)
            {
                throw new RoverException("Rover starting coordinate is invalid", RoverExceptionReason.InvalidRoverStartCoordinates);
            }

            if (!NumberUtility.IsPositiveInteger(field[0]))
            {
                throw new RoverException("Rover starting x coordinate is invalid", RoverExceptionReason.InvalidRoverStartXCoordinate);
            }
            _roverX = Convert.ToInt32(field[0]);

            if (!NumberUtility.IsPositiveInteger(field[1]))
            {
                throw new RoverException("Rover starting y coordinate is invalid", RoverExceptionReason.InvalidRoverStartYCoordinate);
            }
            _roverY = Convert.ToInt32(field[1]);

            if(!IsValidDirection(field[2], out _facing)){
                throw new RoverException("Rover starting direction is invalid", RoverExceptionReason.InvalidRoverStartDirection);
            }
            ConsoleDebug(string.Format("SetStartingPosition: x={0}, y={1}, facing={2}", _roverX, _roverY, _facing));
        }

        private void Navigate(string command)
        {
            for(int i = 0; i < command.Length; i++){
                string navCommand = command[i].ToString().ToUpper();

                RoverCommands roverCommand;

                if(!IsValidCommand(navCommand, out roverCommand)){
                    throw new RoverException("Invalid navigation command", RoverExceptionReason.InvalidNavigationCommand);
                }

                var dictionary = new Dictionary<RoverCommands, Action<RoverCommands>>
                {
                    {RoverCommands.L, Spin},
                    {RoverCommands.R, Spin},
                    {RoverCommands.M, Move},
                };

                var action = dictionary[roverCommand];
                action.Invoke(roverCommand);
            }

            _output.Add(string.Format("{0} {1} {2}", _roverX, _roverY, _facing));
        }

        private bool IsValidCommand(string command, out RoverCommands roverCommand)
        {
            return Enum.TryParse<RoverCommands>(command, out roverCommand);
        }

        private bool IsValidDirection(string command, out Directions roverDirection)
        {
            return Enum.TryParse<Directions>(command, out roverDirection);
        }

        private void Spin(RoverCommands command)
        {
            ConsoleDebug(string.Format("Spin: {0}", command));
            int numberOfDirections = 4;
            int currentDirection = (int)_facing;
            int turn = (command == RoverCommands.L)? -1 : 1;
            _facing = (Directions)NumberUtility.Mod(currentDirection + turn, numberOfDirections);
        }

        private void Move(RoverCommands command)
        {
            ConsoleDebug(string.Format("Move: {0}", _facing));
            int x = 0;
            int y = 0;

            switch (_facing)
            {
                case Directions.N:
                    y = 1;
                    break;
                case Directions.E:
                    x = 1;
                    break;
                case Directions.S:
                    y = -1;
                    break;
                case Directions.W:
                    x = -1;
                    break;
            }
            ValidatedMove(_roverX + x, _roverY + y);
        }

        private void ValidatedMove(int x, int y)
        {
            if (x < 0 || x > _plateauX || y < 0 || y > _plateauY)
            {
                throw new RoverException("Move is out of bounds", RoverExceptionReason.MoveOutOfBounds);
            }

            _roverX = x;
            _roverY = y;
            ConsoleDebug(string.Format("Move to: {0} {1}", _roverX, _roverY));

        }

        public void ConsoleDebug(string text){
            if(_debugMode){
                Console.WriteLine(text);
            }
        }
    }
}
