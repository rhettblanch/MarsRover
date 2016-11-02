using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MarsRover
{
    public class RoverException : Exception, ISerializable
    {
        private RoverExceptionReason _exceptionReason = RoverExceptionReason.Unspecified;

        public RoverException() : base() { }

        public RoverException(string message) : base(message) { }

        public RoverException(string message, RoverExceptionReason exceptionReason)
            : base(message)
        {
            _exceptionReason = exceptionReason;
        }
        public RoverException(string message, System.Exception inner) : base(message, inner) { }

        public RoverException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public RoverExceptionReason ExceptionReason
        {
            get { return _exceptionReason; }
            set { _exceptionReason = value; }
        }
    }

    public enum RoverExceptionReason
    {
        Unspecified,
        InvalidCommandLines,
        InvalidPlateauCoordinates,
        InvalidPlateauXCoordinate,
        InvalidPlateauYCoordinate,
        InvalidRoverStartCoordinates,
        InvalidRoverStartXCoordinate,
        InvalidRoverStartYCoordinate,
        InvalidRoverStartDirection,
        InvalidNavigationCommand,
        MoveOutOfBounds
    }


}
