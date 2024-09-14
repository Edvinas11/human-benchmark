using System;
using System.IO;
using AimReactionAPI.Models;

/*
TEST .TXT FILE EXAMPLE
1, Hard, 5, 10, 120
*/

namespace AimReactionAPI.Services
{
    public class FileService
    {
        public GameConfig parseTextFile(string filePath) {
            GameConfig gameConfig = null;

            try {
                using (var reader = new StreamReader(filePath)) {
                    string line = reader.ReadLine();
                    if (line != null) {
                        var values = line.Split(',');

                        // Creating gameConfig with parsed data
                        gameConfig = new GameConfig {
                            UserId = int.Parse(values[0].Trim()),
                            DifficultyLevel = values[1].Trim(),
                            TargetSpeed = int.Parse(values[2].Trim()), 
                            MaxTargets = int.Parse(values[3].Trim()),
                            GameDuration = int.Parse(values[4].Trim()),
                        };
                    }
                }
            } catch (Exception e) {
                Console.WriteLine($"Error while parsing text file: {e.Message}");
            }

            return gameConfig;
        }
    }
}
