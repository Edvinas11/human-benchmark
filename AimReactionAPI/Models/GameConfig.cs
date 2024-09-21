using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class GameConfig
    {
        public int GameConfigId { get; set; }

        public int UserId { get; set; }

        public string DifficultyLevel { get; set; }

        public int TargetSpeed { get; set; }

        public int MaxTargets { get; set; }

        public int GameDuration { get; set; }

        public GameType GameType { get; set; }
    }
}
