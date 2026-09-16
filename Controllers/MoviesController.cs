using films_dz12._08._2026.Models;
using films_dz12._08._2026.Services;
using Microsoft.AspNetCore.Mvc;

namespace films_dz12._08._2026.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Movie movie,
            IFormFile? posterFile)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movie, posterFile);

                return RedirectToAction("Index", "Home");
            }

            return View(movie);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Movie movie,
            IFormFile? posterFile)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingMovie = await _movieService.GetByIdAsync(id);

                if (existingMovie == null)
                {
                    return NotFound();
                }

                await _movieService.UpdateAsync(movie, posterFile);

                return RedirectToAction("Index", "Home");
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            await _movieService.DeleteAsync(id);

            return RedirectToAction("Index", "Home");
        }
    }
}