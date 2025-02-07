using Practical_Mignesh.Enum;
using Practical_Mignesh.Interface;
using Practical_Mignesh.Models;

namespace Practical_Mignesh.Obstacles
{
    public class Spinner : IObstacle
    {
        private readonly int _rotation;

        public Spinner(int rotation)
        {
            if (rotation % 90 != 0)
                throw new ArgumentException("Rotation must be in 90-degree increments");

            _rotation = rotation;
        }

        public void AffectRobot(Robot robot)
        {
            int currentDirection = (int)robot.Facing;
            int rotationSteps = _rotation / 90;
            int newDirection = (currentDirection + rotationSteps + 4) % 4;
            robot.Facing = (Direction)newDirection;
        }
    }
}