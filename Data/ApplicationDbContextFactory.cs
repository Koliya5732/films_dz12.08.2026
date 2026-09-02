using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace films_dz12._08._2026.Data
{
    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=FilmsDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}