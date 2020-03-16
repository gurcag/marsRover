using MarsRover.Core.Models;

namespace MarsRover.Core.Interfaces
{
    public interface IPlateau
    {
        Coordinate UpperCoordinate { get; }
        Coordinate LowerCoordinate { get; }
    }
}
