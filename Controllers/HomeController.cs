using films_dz12._08._2026.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace films_dz12._08._2026.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Ночь в музее",
                    Director = "Шон Леви",
                    Genre = "Фэнтези, комедия, приключения",
                    Year = 2006,
                    Poster = "/images/3840x.webp",
                    Description = "Ларри устраивается ночным охранником в музей, но обнаруживает, что по ночам экспонаты оживают."
                },

                new Movie
                {
                    Title = "Джуманджи",
                    Director = "Джо Джонстон",
                    Genre = "Фэнтези, комедия, приключения",
                    Year = 1995,
                    Poster = "/images/Jumanji.webp",
                    Description = "Таинственная настольная игра переносит героев в мир джунглей, где героям предстоит закончить игру."
                },

                new Movie
                {
                    Title = "Тайна печати дракона",
                    Director = "Олег Степченко",
                    Genre = "Приключения, фэнтези",
                    Year = 2019,
                    Poster = "/images/dragon.webp",
                    Description = "Путешествие английского картографа приводит его в Китай, где он сталкивается с удивительными событиями."
                },

                new Movie
                {
                    Title = "Бешеные псы",
                    Director = "Квентин Тарантино",
                    Genre = "Криминал, триллер, драма",
                    Year = 1992,
                    Poster = "/images/Фильм_американский.jpg",
                    Description = "После неудачного ограбления преступники пытаются выяснить, кто среди них является предателем."
                },

                new Movie
                {
                    Title = "1+1",
                    Director = "Оливье Накаш, Эрик Толедано",
                    Genre = "Драма, комедия",
                    Year = 2011,
                    Poster = "/images/38420x (1).webp",
                    Description = "После несчастного случая богатый аристократ нанимает помощником молодого человека, и между ними постепенно возникает настоящая дружба."
                }
            };

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}