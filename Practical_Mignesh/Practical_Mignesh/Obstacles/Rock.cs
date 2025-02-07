using Practical_Mignesh.Interface;
using Practical_Mignesh.Models;

namespace Practical_Mignesh.Obstacles
{
    public class Rock : IObstacle
    {
        public void AffectRobot(Robot robot)
        {
            // Rock blocks movement, so no effect needed
        }
    }
}