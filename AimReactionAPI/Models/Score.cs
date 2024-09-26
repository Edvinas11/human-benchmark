﻿using System.ComponentModel.DataAnnotations;

namespace AimReactionAPI.Models
{
    public class Score
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

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
