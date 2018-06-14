using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2048.MVC.Model
{
    public class Tile
    {
        private const int initValue = 0;
        private int lastMove = -1;
        public int Value { get; set; }

        public Tile()
        {
            Value = initValue;
        }

        public bool IsLegal(Tile t)
        {
            if (this.Value == 0 && t.Value == 0) return false;
            if (this.Value == t.Value) return true;
            if (Value == 0) return true;
            return false;
        }

        public int Move(Tile t, int noOfTurn)
        {
            if (this.Value != 0 && t.Value != 0 && (this.lastMove == noOfTurn || t.lastMove == noOfTurn)) return 0;
            if (Value == 0)
            {
                Console.WriteLine("Przesuwamy");
                this.Value = t.Value;
                t.Value = 0;
                return 0;
            }
            if (this.Value == t.Value)
            {
                lastMove = noOfTurn;
                t.lastMove = noOfTurn;
                t.Value = 0;
                ++Value;
                Console.WriteLine("Mergujemy {0}", Math.Pow(2, Value));
                return (int)Math.Pow(2, Value);
            }
            return 0;
        }
        public override string ToString()
        {
            if (Value == 0) return "";
            return Math.Pow(2, Value).ToString();
        }

        public Tile(int v)
        {
            Value = v;
        }
    }
}
