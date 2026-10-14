using films_dz12._08._2026.Data;
using films_dz12._08._2026.Models;

namespace films_dz12._08._2026.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Movie> Movies { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Movies = new Repository<Movie>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}