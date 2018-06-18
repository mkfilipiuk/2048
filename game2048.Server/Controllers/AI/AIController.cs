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
        Dictionary<string, IAI> ais = new Dictionary<string, IAI> { { new RandomAI().ToString(), new RandomAI() }, { new MonteCarloRonenz().ToString() , new MonteCarloRonenz() } };

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
            return CreatedAtAction("Grid", new { id = 2137 }, r);// JsonConvert.SerializeObject(ais[name].Move(new Grid(l)).Json())); //JsonConvert.SerializeObject( ais[name].Move(new Grid(l)).Json()));
        }

    }
}
