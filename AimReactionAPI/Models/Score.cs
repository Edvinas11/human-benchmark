using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public record Score
    (
        int ScoreId,
        int Value,
        DateTime Timestamp,
        GameType GameType
    );
}
