using films_dz12._08._2026.Models;

namespace films_dz12._08._2026.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Movie> Movies { get; }

        Task<int> SaveChangesAsync();
    }
}