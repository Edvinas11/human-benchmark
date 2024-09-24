using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int Value { get; set; }
        public DateTime Timestamp { get; set; }
        public GameType GameType { get; set; }

    }
}
