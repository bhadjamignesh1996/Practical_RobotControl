using Practical_Mignesh.Enum;
using Practical_Mignesh.Interface;
using Practical_Mignesh.Obstacles;

namespace Practical_Mignesh.Models
{
    public class Robot
    {
        public Position Position { get; set; }
        public Direction Facing { get; set; }
        public List<Position> Path { get; } = new();

        private readonly Grid _grid;
        private const int MovementDelay = 100; // milliseconds

        public Robot(Position start, Direction facing, Grid grid)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            Position = start ?? throw new ArgumentNullException(nameof(start));
            Facing = facing;
            Path.Add(new Position(start.X, start.Y));
        }

        public void ExecuteCommands(string commands)
        {
            foreach (char command in commands)
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(char command)
        {
            switch (command)
            {
                case 'L':
                    TurnLeft();
                    break;
                case 'R':
                    TurnRight();
                    break;
                case 'F':
                    MoveForward();
                    break;
                default:
                    throw new ArgumentException($"Invalid command: {command}");
            }
        }

        private void TurnLeft()
        {
            Facing = (Direction)(((int)Facing - 1 + 4) % 4);
        }

        private void TurnRight()
        {
            Facing = (Direction)(((int)Facing + 1) % 4);
        }

        private void MoveForward()
        {
            Thread.Sleep(MovementDelay);
            var newPosition = GetNextPosition();

            if (!_grid.IsValidPosition(newPosition))
            {
                Console.WriteLine($"Cannot move out of bounds to {newPosition}");
                return;
            }

            if (_grid.TryGetObstacle(newPosition, out var obstacle))
            {
                HandleObstacle(obstacle, newPosition);
                return;
            }

            Position = newPosition;
            Path.Add(new Position(Position.X, Position.Y));
        }

        private void HandleObstacle(IObstacle? obstacle, Position newPosition)
        {
            switch (obstacle)
            {
                case Rock:
                    Console.WriteLine($"Cannot move to {newPosition}, rock detected");
                    break;
                case Hole hole:
                    Console.WriteLine($"Falling through hole at {newPosition}");
                    hole.AffectRobot(this);
                    Path.Add(new Position(Position.X, Position.Y));
                    break;
                case Spinner spinner:
                    Console.WriteLine($"Spinner detected at {newPosition}");
                    spinner.AffectRobot(this);
                    Position = newPosition;
                    Path.Add(new Position(Position.X, Position.Y));
                    break;
                default:
                    Console.WriteLine($"Unknown obstacle at {newPosition}");
                    break;
            }
        }

        private Position GetNextPosition()
        {
            return Facing switch
            {
                Direction.North => new Position(Position.X, Position.Y + 1),
                Direction.East => new Position(Position.X + 1, Position.Y),
                Direction.South => new Position(Position.X, Position.Y - 1),
                Direction.West => new Position(Position.X - 1, Position.Y),
                _ => throw new ArgumentException($"Invalid direction: {Facing}")
            };
        }
    }
}