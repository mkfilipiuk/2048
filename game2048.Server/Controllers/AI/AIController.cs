using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using game2048.Shared.Logic;
using game2048.Shared.Logic.AI;
using _2048.MVC.Model;
using game2048.Client.Controllers;
using Newtonsoft.Json;


namespace game2048.Server.Controllers.AI
{
    [Route("api/ais")]
    [ApiController]
    public class AIController : ControllerBase
    {
        Dictionary<string, IAI> ais = new Dictionary<string, IAI> { { new RandomAI().ToString(), new RandomAI() }, { new MonteCarloRonenz(1).ToString(), new MonteCarloRonenz(1) }, { new MonteCarloRonenz(10).ToString(), new MonteCarloRonenz(10) }, { new MonteCarloRonenz(20).ToString(), new MonteCarloRonenz(20) }, { new MonteCarloRonenz(50).ToString(), new MonteCarloRonenz(50) }, { new MonteCarloRonenz(100).ToString() , new MonteCarloRonenz(100) } };

        [HttpGet]
        public IEnumerable<string> GetAIs()
        {
            return from ai in ais select ai.Key;
        }

        [HttpPost("{name}", Name = "AI")]
        public ActionResult<GridWrapper> GetMove([FromRoute] string name, [FromBody] GridWrapper grid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<int> l = grid.Grid;
            Console.WriteLine(name);
            Console.WriteLine(l.Capacity);
            if (!ais.ContainsKey(name)) return BadRequest(ModelState);
            var r = new GridWrapper(ais[name].Move(new Grid(l)).Json());
            Console.WriteLine("Wysyłamy odpowiedź");
            return CreatedAtAction("Grid", new { id = 1 }, r);
        }

    }
}
