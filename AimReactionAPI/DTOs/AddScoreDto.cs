using AimReactionAPI.Models;

namespace AimReactionAPI.DTOs;

public class AddScoreDto
{
    public int GameId { get; set; }
    public GameType GameType { get; set; }
    public DateTime DateAchieved { get; set; }
    public int Value { get; set; }
}

