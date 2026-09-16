using films_dz12._08._2026.Models;

namespace films_dz12._08._2026.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();

        Task<Movie?> GetByIdAsync(int id);

        Task CreateAsync(Movie movie, IFormFile? posterFile);

        Task UpdateAsync(Movie movie, IFormFile? posterFile);

        Task DeleteAsync(int id);
    }
}