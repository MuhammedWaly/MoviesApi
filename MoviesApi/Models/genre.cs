using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [MaxLength(90)]
        public required string Name { get; set; }
    }
}
