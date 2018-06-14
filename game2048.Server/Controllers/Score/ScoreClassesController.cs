using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using game2048.Data;
using game2048.Models;

namespace game2048.Server.Controllers.Score
{
    [Route("api/ScoreClasses")]
    [ApiController]
    public class ScoreClassesController : ControllerBase
    {
        private readonly Context _context;

        public ScoreClassesController(Context context)
        {
            _context = context;
        }

        // GET: api/ScoreClasses
        [HttpGet]
        public IEnumerable<ScoreClass> GetScores()
        {
            Console.WriteLine("W środku");
            return _context.Scores;
        }

        // GET: api/ScoreClasses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScoreClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scoreClass = await _context.Scores.FindAsync(id);

            if (scoreClass == null)
            {
                return NotFound();
            }

            return Ok(scoreClass);
        }

        // PUT: api/ScoreClasses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScoreClass([FromRoute] int id, [FromBody] ScoreClass scoreClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scoreClass.ID)
            {
                return BadRequest();
            }

            _context.Entry(scoreClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ScoreClasses
        [HttpPost]
        public async Task<ActionResult<ScoreClass>> PostScoreClass([FromBody] ScoreClass scoreClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Scores.Add(scoreClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScoreClass", new { id = scoreClass.ID }, scoreClass);
        }

        // DELETE: api/ScoreClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScoreClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scoreClass = await _context.Scores.FindAsync(id);
            if (scoreClass == null)
            {
                return NotFound();
            }

            _context.Scores.Remove(scoreClass);
            await _context.SaveChangesAsync();

            return Ok(scoreClass);
        }

        private bool ScoreClassExists(int id)
        {
            return _context.Scores.Any(e => e.ID == id);
        }
    }
}