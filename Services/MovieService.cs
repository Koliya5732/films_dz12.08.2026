using films_dz12._08._2026.Data;
using films_dz12._08._2026.Models;
using Microsoft.EntityFrameworkCore;

namespace films_dz12._08._2026.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(Movie movie, IFormFile? posterFile)
        {
            if (posterFile != null && posterFile.Length > 0)
            {
                movie.Poster = await SavePosterAsync(posterFile);
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie, IFormFile? posterFile)
        {
            var oldMovie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (oldMovie == null)
            {
                return;
            }

            if (posterFile != null && posterFile.Length > 0)
            {
                movie.Poster = await SavePosterAsync(posterFile);

                DeletePoster(oldMovie.Poster);
            }
            else
            {
                movie.Poster = oldMovie.Poster;
            }

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return;
            }

            DeletePoster(movie.Poster);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        private async Task<string> SavePosterAsync(IFormFile posterFile)
        {
            var fileName = Guid.NewGuid().ToString() +
                           Path.GetExtension(posterFile.FileName);

            var folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "movies");

            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await posterFile.CopyToAsync(stream);
            }

            return "/images/movies/" + fileName;
        }

        private void DeletePoster(string? poster)
        {
            if (string.IsNullOrEmpty(poster) ||
                !poster.StartsWith("/images/movies/"))
            {
                return;
            }

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                poster.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}