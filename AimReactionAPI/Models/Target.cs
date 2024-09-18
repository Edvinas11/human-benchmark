using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public struct Target
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }

        public Target(int x, int y, int size, int speed)
        {
            X = x;
            Y = y;
            Size = size;
            Speed = speed;
        }
    }
}
