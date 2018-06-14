using game2048.Shared.Logic.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace game2048.Shared.Logic
{
    static class AIManager
    {
        static public List<IAI> AIs { get { return new List<IAI>{ new RandomAI() }; } }
    }
}
