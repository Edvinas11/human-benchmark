using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GameConfigId { get; set; }

        [Required]
        public int ScoreValue { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        public User User { get; set; }
        public GameConfig GameConfig { get; set; }
    }
}
