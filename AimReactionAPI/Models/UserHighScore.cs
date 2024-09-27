namespace AimReactionAPI.Models
{
    public class UserHighScore
    {
        public int UserId { get; set; }
        public GameType GameType { get; set; }
        public int HighScore { get; set; }
    }
}