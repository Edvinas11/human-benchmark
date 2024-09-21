using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public struct Coordinates {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Target
    {
        public int Size { get; set; }
        public int Speed { get; set; }

        public Coordinates Position {get; set;}

        public Target(Coordinates position, int size, int speed)
        {
            Position = position;
            Size = size;
            Speed = speed;
        }
    }
}
