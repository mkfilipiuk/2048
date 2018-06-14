using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game2048.Client.Controllers
{
    public class GridWrapper
    {
        public List<int> Grid { set; get; }

        public GridWrapper() { }
        public GridWrapper(List<int> grid)
        {
            Grid = grid;
        }
    }
}
