using films_dz12._08._2026.Models;
using films_dz12._08._2026.Repositories;

namespace films_dz12._08._2026.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return movies.ToList();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Movies.GetByIdAsync(id);
        }

        public async Task CreateAsync(
            Movie movie,
            IFormFile? posterFile)
        {
            if (posterFile != null && posterFile.Length > 0)
            {
                movie.Poster = await SavePosterAsync(posterFile);
            }

            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(
            Movie movie,
            IFormFile? posterFile)
        {
            var existingMovie =
                await _unitOfWork.Movies.GetByIdAsync(movie.Id);

            if (existingMovie == null)
            {
                return;
            }

            if (posterFile != null && posterFile.Length > 0)
            {
                var newPoster = await SavePosterAsync(posterFile);

                DeletePoster(existingMovie.Poster);

                existingMovie.Poster = newPoster;
            }

            existingMovie.Title = movie.Title;
            existingMovie.Director = movie.Director;
            existingMovie.Genre = movie.Genre;
            existingMovie.Year = movie.Year;
            existingMovie.Description = movie.Description;

            _unitOfWork.Movies.Update(existingMovie);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);

            if (movie == null)
            {
                return;
            }

            DeletePoster(movie.Poster);

            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<string> SavePosterAsync(
            IFormFile posterFile)
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