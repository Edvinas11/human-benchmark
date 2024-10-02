using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class GameDto
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    public string GameDescription { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int GameDuration { get; set; }
    public GameType GameType { get; set; }
}
