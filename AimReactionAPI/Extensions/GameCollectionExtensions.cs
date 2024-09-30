using AimReactionAPI.Models;

namespace AimReactionAPI.Extensions;

public static class GameCollectionExtensions
{
    public static IEnumerable<Target> FilterTargetsBySpeed(this List<Target> targets, int speedThreshold)
    {
        return targets.Where(t => t.Speed >= speedThreshold);
    }
}
