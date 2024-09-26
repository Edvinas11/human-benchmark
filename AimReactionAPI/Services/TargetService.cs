using AimReactionAPI.Models;
using AimReactionAPI.Services;
using AimReactionAPI.Extensions;
using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using AimReactionAPI.Data;

namespace AimReactionAPI.Services
{
    public class TargetService
    {
        private readonly AppDbContext _context;

        public TargetService(AppDbContext context)
        {
            _context = context;
        }
        public List<Target> GenerateTargets(int maxTargets = 10, int targetSpeed = 10)
        {
            var targets = new List<Target>();
            for (int i = 0; i < maxTargets; i++)
            {
                targets.Add(new Target
                {
                    X = new Random().Next(0, 100),
                    Y = new Random().Next(0, 100),
                    Size = new Random().Next(1, 10),
                    Speed = targetSpeed
                });
            }

            return targets;
        }
    }
}