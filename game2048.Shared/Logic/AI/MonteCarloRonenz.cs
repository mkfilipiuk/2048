using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using _2048.MVC.Model;
using StackOverflow.Helpers;

namespace StackOverflow.Helpers
{
    public class Pair<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public Pair(T1 t1, T2 t2)
        {
            Item1 = t1;
            Item2 = t2;
        }
    }
}

namespace game2048.Shared.Logic.AI
{
    public class MonteCarloRonenz : IAI
    {

        private Random r;

        public const int NumberOfRepetitions = 100;

        public MonteCarloRonenz()
        {
            r = new Random();
        }

        public Grid Move(Grid grid)
        {
            List<Pair < Direction,ulong>> directions = new List<Pair<Direction, ulong>>();
            foreach(Direction d in Enum.GetValues(typeof(Direction)))
            {
                if (grid.IsMovePossible(d)) directions.Add(new Pair<Direction,ulong>(d, 0));
            }
            foreach(var t in directions)
            {
                //var thread = new Thread(new ParameterizedThreadStart(myMethod));
                //thread.Start(new Grid(grid.Json()));
                //thread.Join();
                for (int i = 0; i < NumberOfRepetitions; ++i)
                {
                    var g = new Grid(grid.Json());
                    g.Move(t.Item1);
                    t.Item2 += MakeRun(g);
                }
            }
            grid.Move(MaxMove(directions));
            return grid;
        }

        private Direction MaxMove(List<Pair<Direction, ulong>> directions)
        {
            ulong max = 0;
            List<Direction> l = new List<Direction>();
            foreach(var d in directions)
            {
                if (max < d.Item2) max = d.Item2;
            }
            foreach (var d in directions)
            {
                if (max == d.Item2) l.Add(d.Item1);
            }
            return l[r.Next(l.Count)];
        }

        private ulong MakeRun(Grid grid)
        {
            while(!grid.isFinished)
            {
                List<Direction> directions = new List<Direction>();
                foreach (Direction d in Enum.GetValues(typeof(Direction)))
                {
                    if (grid.IsMovePossible(d)) directions.Add(d);
                }
                grid.Move(directions[r.Next(directions.Count)]);
            }
            return (ulong)grid.Score;
        }

        public override string ToString()
        {
            return "Monte_Carlo_Ronenz";
        }
    }
}
