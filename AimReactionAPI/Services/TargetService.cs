using AimReactionAPI.Models;
using AimReactionAPI.Services;
using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace AimReactionAPI.Services
{
    public class TargetService
    {
        public List<Target> _targets = new List<Target>();
        public TargetService()
        {
            // dummy data
            _targets.Add(CreateRandomTarget(100, 100, 10, 1));
            _targets.Add(CreateRandomTarget(100, 100, 10, 1));

        }
        public List<Target> GetAllTargets()
        {
            return _targets;
        }
        public void AddTarget(Target target)
        {
            _targets.Add(target);
        }
        public Target CreateRandomTarget(int maxX, int maxY, int size, int speed)
        {
            Target target = null;
            try
            {
                Random random = new Random();
                int randomX = random.Next(maxX);
                int randomY = random.Next(maxY);
                Coordinates randomPosition = new Coordinates(randomX, randomY);
                target = new Target(randomPosition, size, speed);
                return target;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating target object: {e.Message}");
            }
            return target;
        }
    }
}