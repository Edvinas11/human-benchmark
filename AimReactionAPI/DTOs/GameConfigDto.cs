using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class GameConfigDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DifficultyLevel { get; set; }
    public int TargetSpeed { get; set; }
    public int MaxTargets { get; set; }
    public int GameDuration { get; set; }
    public GameType GameType { get; set; }
}
