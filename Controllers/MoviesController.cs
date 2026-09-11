using films_dz12._08._2026.Data;
using films_dz12._08._2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace films_dz12._08._2026.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();

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
        public async Task<IActionResult> Create(Movie movie, IFormFile? posterFile)
        {
            if (ModelState.IsValid)
            {
                if (posterFile != null && posterFile.Length > 0)
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

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await posterFile.CopyToAsync(stream);
                    }

                    movie.Poster = "/images/movies/" + fileName;
                }

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

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

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

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

            var movie = await _context.Movies.FindAsync(id);

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
                try
                {
                    var oldMovie = await _context.Movies
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (oldMovie == null)
                    {
                        return NotFound();
                    }

                    // Если загрузили новый постер
                    if (posterFile != null && posterFile.Length > 0)
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

                        movie.Poster = "/images/movies/" + fileName;

                        // Удаляем старый постер
                        if (!string.IsNullOrEmpty(oldMovie.Poster) &&
                            oldMovie.Poster.StartsWith("/images/movies/"))
                        {
                            var oldFilePath = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot",
                                oldMovie.Poster.TrimStart('/')
                                    .Replace('/', Path.DirectorySeparatorChar));

                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                    }
                    else
                    {
                        // Новый постер не загружали — оставляем старый
                        movie.Poster = oldMovie.Poster;
                    }

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

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

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

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
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return View("~/Views/Shared/Error404.cshtml");
            }

            // Удаляем файл постера
            if (!string.IsNullOrEmpty(movie.Poster) &&
                movie.Poster.StartsWith("/images/movies/"))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    movie.Poster.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}