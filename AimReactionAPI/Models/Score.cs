﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models;

public record Score
{
    public int ScoreId { get; set; }

    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Score value cannot be negative.");
            }
            _value = value; 
        }
    }

    public DateTime DateAchieved { get; set; }
    public int ReactionTime { get; set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }

    public GameType GameType { get; set; }
    public int GameId { get; set; }

    [JsonIgnore]
    public Game Game { get; set; }
}
