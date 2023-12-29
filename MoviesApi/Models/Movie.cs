using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public double Rate { get; set; }
        public int year { get; set; }
        public byte[] Poster {  get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        
    }
}
