using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _2RazorPagesMoveCore.Models
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
