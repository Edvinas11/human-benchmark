using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class Target
    {
        public int TargetId { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int GameId { get; set; }
    }
}
