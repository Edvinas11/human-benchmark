using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
