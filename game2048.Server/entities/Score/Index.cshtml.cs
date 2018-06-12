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
    public class IndexModel : PageModel
    {
        private readonly game2048.Data.Context _context;

        public IndexModel(game2048.Data.Context context)
        {
            _context = context;
        }

        public IList<Score> Score { get;set; }

        public async Task OnGetAsync()
        {
            Score = await _context.Scores.ToListAsync();
        }
    }
}
