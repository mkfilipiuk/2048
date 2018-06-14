using System;
using System.Collections.Generic;
using System.Text;
using _2048.MVC.Model;

namespace game2048.Shared.Logic.AI
{
    public class RandomAI : IAI
    {
        public Grid Move(Grid grid)
        {
            List<Direction> directions = new List<Direction>();
            foreach (Direction d in Enum.GetValues(typeof(Direction)))
            {
                if(grid.IsMovePossible(d))
                {
                    directions.Add(d);
                }
            }
            if (directions.Count == 0) return grid;
            Random rnd = new Random();
            grid.Move(directions[rnd.Next(directions.Count)]);
            return grid;
        }

        public override string ToString()
        {
            return "random";
        }
    }
}
