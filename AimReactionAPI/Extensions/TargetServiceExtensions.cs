using AimReactionAPI.Models;
using System;


namespace AimReactionAPI.Extensions
{
    public static class TargetServiceExtensions
    {   
        ////Return a list of targets within acceptable distance for a certain position
        //public static List<Target> GetTargetInDistance(this List<Target> _targets, Coordinates position, int distance)
        //{
        //    List<Target> targetsInDistance = new List<Target>();
        //    if (!_targets.Any())
        //    {
        //        return targetsInDistance;
        //    }
        //    foreach (var target in _targets)
        //    {
        //        if(CalculateDistance(target.Position, position) < distance)
        //        {
        //            targetsInDistance.Add(target);
        //        }
        //    }
        //    return targetsInDistance;
        //}

        //// calculate absolute distance between two coordinates
        //public static int CalculateDistance(Coordinates pos1, Coordinates pos2)
        //{
        //    int distanceX = Math.Abs(pos2.X - pos1.X);
        //    int distanceY = Math.Abs(pos2.Y - pos1.Y);
        //    double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        //    return (int)distance;
        //}
    }
}