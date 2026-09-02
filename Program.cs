using films_dz12._08._2026.Data;
using films_dz12._08._2026.Models;
using Microsoft.EntityFrameworkCore;



namespace films_dz12._08._2026
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Movies.Any())
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

                    context.Movies.AddRange(movies);
                    context.SaveChanges();
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
