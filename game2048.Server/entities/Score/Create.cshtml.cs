using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using game2048.Data;
using game2048.Models;

namespace game2048.Server.entities.Score
{
    public class CreateModel : PageModel
    {
        private readonly game2048.Data.Context _context;

        public CreateModel(game2048.Data.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Score Score { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Scores.Add(Score);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}