using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Routing;
namespace _2048.MVC.Model
{
    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }

    public class Grid
    {
        public bool isFinished
        {
            get
            {
                return !IsMovePossible(Direction.DOWN) && !IsMovePossible(Direction.LEFT) && !IsMovePossible(Direction.RIGHT) && !IsMovePossible(Direction.UP);
            }
        }
        public int Score { set; get; }
        private int noOfTurn;
        private const int size = 4;
        private int numberOfEmpty
        {
            get
            {
                int counter = 0;
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        if (GridOfTiles[i, j].Value == 0) ++counter;
                    }
                }
                return counter;
            }
        }
        public Tile[,] GridOfTiles { get; internal set; }
        private Random rnd;

        private void InitializeGrid()
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    GridOfTiles[i, j] = new Tile();
                }
            }
        }

        public bool IsMovePossible(Direction d)
        {
            bool result = false;
            if (d == Direction.UP)
            {
                for (int i = 0; i + 1 < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        result = GridOfTiles[i, j].IsLegal(GridOfTiles[i + 1, j]) || result;
                    }
                }
            }
            if (d == Direction.DOWN)
            {
                for (int i = 0; i + 1 < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        //Console.WriteLine("Testujemy ({0},{1})->{2} na które wchodzi ({3},{4})->{5}",i+1,j, GridOfTiles[i+1, j],i, j,GridOfTiles[i, j]); 
                        result = GridOfTiles[i + 1, j].IsLegal(GridOfTiles[i, j]) || result;
                        //Console.WriteLine("Result: {0}", result);
                    }
                }
            }
            if (d == Direction.LEFT)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j + 1 < size; ++j)
                    {
                        result = GridOfTiles[i, j].IsLegal(GridOfTiles[i, j + 1]) || result;
                    }
                }
            }
            if (d == Direction.RIGHT)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j + 1 < size; ++j)
                    {
                        result = GridOfTiles[i, j + 1].IsLegal(GridOfTiles[i, j]) || result;
                    }
                }
            }
            return result;
        }

        public bool Move(Direction d)
        {
            if (!IsMovePossible(d)) return IsMovePossible(Direction.DOWN) || IsMovePossible(Direction.UP) || IsMovePossible(Direction.RIGHT) || IsMovePossible(Direction.LEFT);
            if (d == Direction.DOWN)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        for (int k = size - 2; k >= 0; --k)
                        {
                            //Console.WriteLine("Będziemy łączyć ({0},{1}) z ({2},{3})", k + 1, j, k, j);
                            Score += GridOfTiles[k + 1, j].Move(GridOfTiles[k, j], noOfTurn);
                        }
                    }
                }
            }
            if (d == Direction.UP)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        for (int k = 0; k < size - 1; ++k)
                        {
                            //Console.WriteLine("Będziemy łączyć ({0},{1}) z ({2},{3})", k, j, k + 1, j);
                            Score += GridOfTiles[k, j].Move(GridOfTiles[k + 1, j], noOfTurn);
                        }
                    }
                }
            }
            if (d == Direction.LEFT)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        for (int k = 0; k < size - 1; ++k)
                        {
                            //Console.WriteLine("Będziemy łączyć ({0},{1}) z ({2},{3})", i, k, i, k + 1);
                            Score += GridOfTiles[i, k].Move(GridOfTiles[i, k + 1], noOfTurn);
                        }
                    }
                }
            }
            if (d == Direction.RIGHT)
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        for (int k = size - 2; k >= 0; --k)
                        {
                            //Console.WriteLine("Będziemy łączyć ({0},{1}) z ({2},{3})", i, k + 1, i, k);
                            int s = Score;
                            Score += GridOfTiles[i, k + 1].Move(GridOfTiles[i, k], noOfTurn);
                            if (s < Score) Console.WriteLine("Dodajemy {0} punktów", Score - s);
                        }
                    }
                }
            }
            ++noOfTurn;
            RandomTile();
            return IsMovePossible(Direction.DOWN) || IsMovePossible(Direction.UP) || IsMovePossible(Direction.RIGHT) || IsMovePossible(Direction.LEFT);
        }

        public String GetElement(int x, int y)
        {
            return GridOfTiles[x, y].ToString();
        }

        public Grid()
        {
            GridOfTiles = new Tile[4, 4];
            InitializeGrid();
            rnd = new Random();
            noOfTurn = 0;
            Score = 0;
        }

        public void RandomTile()
        {
            int nBefore = numberOfEmpty;
            if (numberOfEmpty == 0) return;
            if (rnd == null) rnd = new Random();
            int r = rnd.Next(numberOfEmpty);
            int counter = 0;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (GridOfTiles[i, j].Value == 0)
                    {
                        if (counter == r)
                        {
                            Console.WriteLine("Wstawiamy na ({0},{1})", i, j);
                            GridOfTiles[i, j].Value = rnd.Next(2) + 1;
                        }
                        counter++;
                    }
                }
            }
            if (nBefore == numberOfEmpty) Console.WriteLine("Zepsute");
        }

        public void InitGame()
        {
            RandomTile();
            RandomTile();
        }

        public void Print()
        {
            Console.WriteLine("Numer tury: {0}", noOfTurn);
            Console.WriteLine("Liczba pustych: {0}", numberOfEmpty);
            Console.WriteLine("Wynik: {0}", Score);

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Console.Write(GridOfTiles[i, j].Value);
                }
                Console.Write('\n');
            }
        }

        public void Close()
        {
            //isFinished = true;
        }

        public List<int> Json()
        {
            List<int> l = new List<int>();
            l.Add(Score);
            for(int i = 0; i < 4; ++i)
            {
                for(int j = 0; j < 4; ++j)
                {
                    l.Add(GridOfTiles[i, j].Value);
                }
            }
            return l;
        }

        public Grid(List<int> grid)
        {
            GridOfTiles = new Tile[4, 4];
            Score = grid[0];
            for(int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                    GridOfTiles[i,j] = new Tile(grid[size * i + j + 1]);
            }

        }


    }
}
