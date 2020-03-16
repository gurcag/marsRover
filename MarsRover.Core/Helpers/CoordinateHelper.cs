using MarsRover.Core.Models;

namespace MarsRover.Core.Helpers
{
    public static class CoordinateHelper
    {
        public static bool IsValidCoordinate(Coordinate plateauLowerCoordinate, Coordinate plateauUpperCoordinate, Coordinate coordinate)
        {
            return plateauLowerCoordinate.X <= coordinate.X
                && plateauUpperCoordinate.X >= coordinate.X
                && plateauLowerCoordinate.Y <= coordinate.Y
                && plateauUpperCoordinate.Y >= coordinate.Y;
        }
    }
}
