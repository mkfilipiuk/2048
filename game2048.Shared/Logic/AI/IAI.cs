using _2048.MVC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace game2048.Shared.Logic
{
    public interface IAI
    {
        Grid Move(Grid grid);
    }
}
