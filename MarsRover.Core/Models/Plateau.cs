using MarsRover.Core.Interfaces;

namespace MarsRover.Core.Models
{
    public class Plateau : IPlateau
    {
        public Coordinate UpperCoordinate { get; }
        public Coordinate LowerCoordinate { get; }

        public Plateau(Coordinate lowerCoordinate, Coordinate upperCoordinate)
        {
            this.LowerCoordinate = lowerCoordinate;
            this.UpperCoordinate = upperCoordinate;
        }
    }
}
