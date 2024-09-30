namespace AimReactionAPI.Models;

public class GameSession
{
    public int GameSessionId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GameType GameType { get; set; }

    public TimeSpan GetDuration()
    {
        return EndTime - StartTime;
    }
}
