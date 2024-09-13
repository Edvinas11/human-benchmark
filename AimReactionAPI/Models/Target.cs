using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class Target
    {
        [Key]
        public int TargetId { get; set; }

        [Required]
        public string Shape { get; set; }

        [Required]
        public int Size { get; set; }

        public int Speed { get; set; }
    }
}
