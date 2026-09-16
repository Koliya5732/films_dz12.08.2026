using System.ComponentModel.DataAnnotations;
using films_dz12._08._2026.Validation;

namespace films_dz12._08._2026.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название фильма")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите режиссёра")]
        [StringLength(100, ErrorMessage = "Имя режиссёра не должно превышать 100 символов")]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите жанр")]
        public string Genre { get; set; } = string.Empty;

        [MovieYear]
        public int Year { get; set; }

        public string? Poster { get; set; }

        [Required(ErrorMessage = "Введите описание фильма")]
        public string Description { get; set; } = string.Empty;
    }
}