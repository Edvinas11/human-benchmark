using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<Score> Scores { get; set; } = new List<Score>();
    public ICollection<GameSession> GameSessions { get; set; }
}
