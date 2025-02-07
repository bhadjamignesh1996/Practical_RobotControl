using Practical_Mignesh.Interface;

namespace Practical_Mignesh.Models
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        private readonly Dictionary<(int, int), IObstacle> _obstacles = new();

        public Grid(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Grid dimensions must be positive");

            Width = width;
            Height = height;
        }

        public void AddObstacle(int x, int y, IObstacle obstacle)
        {
            if (!IsValidPosition(new Position(x, y)))
                throw new ArgumentException("Obstacle position is outside grid bounds");

            _obstacles[(x, y)] = obstacle ?? throw new ArgumentNullException(nameof(obstacle));
        }

        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < Width &&
                   position.Y >= 0 && position.Y < Height;
        }

        public bool TryGetObstacle(Position position, out IObstacle? obstacle)
        {
            return _obstacles.TryGetValue((position.X, position.Y), out obstacle);
        }
    }
}