using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
