using Practical_Mignesh.Interface;
using Practical_Mignesh.Models;

namespace Practical_Mignesh.Obstacles
{
    public class Hole : IObstacle
    {
        private readonly Position _exitPosition;

        public Hole(Position exitPosition)
        {
            _exitPosition = exitPosition ?? throw new ArgumentNullException(nameof(exitPosition));
        }

        public void AffectRobot(Robot robot)
        {
            robot.Position = new Position(_exitPosition.X, _exitPosition.Y);
        }
    }
}