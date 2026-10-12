using films_dz12._08._2026.Models;
using films_dz12._08._2026.Services;
using Microsoft.AspNetCore.Mvc;

namespace films_dz12._08._2026.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAll()
        {
            var movies = await _movieService.GetAllAsync();

            return Ok(movies);
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromForm] Movie movie,
            IFormFile? posterFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _movieService.CreateAsync(movie, posterFile);

            return CreatedAtAction(
                nameof(GetById),
                new { id = movie.Id },
                movie);
        }

        // PUT: api/movies/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            int id,
            [FromForm] Movie movie,
            IFormFile? posterFile)
        {
            if (id != movie.Id)
            {
                return BadRequest("ID в URL не совпадает с ID фильма.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMovie = await _movieService.GetByIdAsync(id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            await _movieService.UpdateAsync(movie, posterFile);

            return Ok(movie);
        }

        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            await _movieService.DeleteAsync(id);

            return NoContent();
        }
    }
}