using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2RazorPagesMoveCore.Models;

namespace _2RazorPagesMoveCore.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly _2RazorPagesMoveCore.Models.MovieContext _context;

        public IndexModel(_2RazorPagesMoveCore.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
