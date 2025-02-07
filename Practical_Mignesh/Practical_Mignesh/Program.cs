using Practical_Mignesh.Enum;
using Practical_Mignesh.Models;
using Practical_Mignesh.Obstacles;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Enter grid size (Width Height):");
            string[] gridSize = Console.ReadLine()?.Split() ?? throw new InvalidOperationException("Invalid input");
            if (gridSize.Length != 2 || !int.TryParse(gridSize[0], out int width) || !int.TryParse(gridSize[1], out int height))
            {
                throw new ArgumentException("Invalid grid dimensions");
            }

            var grid = new Grid(width, height);

            // Add some sample obstacles
            grid.AddObstacle(2, 2, new Rock());
            grid.AddObstacle(3, 3, new Hole(new Position(4, 4)));
            grid.AddObstacle(1, 1, new Spinner(90));

            Console.WriteLine("Enter robot start position (X Y Direction):");
            string[] startInfo = Console.ReadLine()?.Split() ?? throw new InvalidOperationException("Invalid input");
            if (startInfo.Length != 3 || !int.TryParse(startInfo[0], out int startX) ||
                !int.TryParse(startInfo[1], out int startY) ||
                !Enum.TryParse<Direction>(startInfo[2], true, out Direction startDir))
            {
                throw new ArgumentException("Invalid start position");
            }

            var startPosition = new Position(startX, startY);
            if (!grid.IsValidPosition(startPosition))
            {
                throw new ArgumentException("Start position is outside grid bounds");
            }

            var robot = new Robot(startPosition, startDir, grid);

            Console.WriteLine("Enter movement commands (e.g., LFFFRFFL):");
            string? commands = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(commands) || !commands.All(c => "LRF".Contains(c)))
            {
                throw new ArgumentException("Invalid commands");
            }

            robot.ExecuteCommands(commands);

            Console.WriteLine($"Final Position: {robot.Position} Facing: {robot.Facing}");
            Console.WriteLine("Path Traversed:");
            foreach (var pos in robot.Path)
            {
                Console.WriteLine(pos);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}