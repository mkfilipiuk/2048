using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2048.MVC.Model;

namespace _2048.csfunctions_for_js
{
    public static class ArrowsHandler
    {
        public static Grid grid { get; set; }
        public static Action action { get; set; }
        public static void DealArrow(string keyID_s)
        {
            int keyID = 0;
            Console.WriteLine(keyID_s);
            try
            {
                keyID = Int32.Parse(keyID_s);
            }
            catch
            {
                Console.WriteLine("Mamy jakiś błąd, bo {0}", keyID_s);
            }
            bool result = true;
            if (keyID == 38)
            {
                result = grid.Move(Direction.UP);
            }
            else if (keyID == 40)
            {
                result = grid.Move(Direction.DOWN);
            }
            else if (keyID == 37)
            {
                result = grid.Move(Direction.LEFT);
            }
            else if (keyID == 39)
            {
                result = grid.Move(Direction.RIGHT);
            }
            grid.Print();
            //if (!result) grid.Close();
            action();
        }
    }
}
