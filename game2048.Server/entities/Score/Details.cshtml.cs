using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using game2048.Data;
using game2048.Models;

namespace game2048.Server.entities.Score
{
    public class DetailsModel : PageModel
    {
        private readonly game2048.Data.Context _context;

        public DetailsModel(game2048.Data.Context context)
        {
            _context = context;
        }

        public ScoreClass Score { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Score = await _context.Scores.FirstOrDefaultAsync(m => m.ID == id);

            if (Score == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
