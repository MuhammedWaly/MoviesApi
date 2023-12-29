using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Dtos
{
    public class CreateGenereDto
    {
        [MaxLength(90)]
        public string Name { get; set; }
    }
}
