using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public ICollection<Score> Scores { get; set; } = new List<Score>();
    [JsonIgnore]
    public ICollection<GameSession> GameSessions { get; set; }
}
