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
    public class DeleteModel : PageModel
    {
        private readonly game2048.Data.Context _context;

        public DeleteModel(game2048.Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Score Score { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Score = await _context.Scores.FindAsync(id);

            if (Score != null)
            {
                _context.Scores.Remove(Score);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
