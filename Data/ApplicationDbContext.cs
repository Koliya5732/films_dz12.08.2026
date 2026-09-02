using films_dz12._08._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace films_dz12._08._2026.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}