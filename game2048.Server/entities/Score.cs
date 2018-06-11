using System;
using System.Collections.Generic;

namespace game2048.Models
{
    public class Score
    {
        public int ID { get; set; }
        public string Nickname { get; set; }
        public string TypeOfScorer { get; set; }
        public int Points { get; set; }
    }
}