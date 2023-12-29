using MoviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Dtos
{
    public class MovieDto
    {

        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public double Rate { get; set; }
        public int year { get; set; }
        public IFormFile? Poster { get; set; }
        public int GenreId { get; set; }
        
    }
}
