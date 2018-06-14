using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2048.MVC.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace game2048.Server.Controllers.AI
{
    [Route("api/random")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        // GET: api/Random
        [HttpGet]
        public List<int> GetAIRandom()
        {
            var g = new Grid();
            g.InitGame();
            return g.Json();
        }

        [HttpGet("{id}", Name = "GetAIRandom")]
        public List<int> GetAIRandomMove([FromBody] List<int> grid)
        {
            return null;
        }

        // GET: api/Random/5
        [HttpGet("{id}", Name = "GetAIRandom")]
        public string GetAIRandomID(int id)
        {
            return "value";
        }

        // POST: api/Random
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Random/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
