using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Reposaitory;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieReposaitory _MovieReposaitory;
        private readonly IGenreReposaitory _genreReposaitory;

        private List<string> _allowedExtensions = new List<string>() { ".jpg", ".png" };
        private long _maxAllowedSize = 1048576;

        public MoviesController(IMovieReposaitory movieReposaitory, IGenreReposaitory genreReposaitory)
        {
            _MovieReposaitory = movieReposaitory;
            _genreReposaitory = genreReposaitory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDto Dto)
        {
            if (Dto.Poster == null)
                return BadRequest();


            using var dataStrem = new MemoryStream();
            await Dto.Poster.CopyToAsync(dataStrem);

            if (!_allowedExtensions.Contains(Path.GetExtension(Dto.Poster.FileName.ToLower())))
                return BadRequest("Invalid Extension, Only .png and .jpg");

            if (Dto.Poster.Length > _maxAllowedSize)
                return BadRequest("File must Be 1MB or smaller");

            var AllowedGenre = await _genreReposaitory.AllowedGenre(Dto.GenreId);
            if (!AllowedGenre) return BadRequest("Invalid Genre");

            var movie = new Movie()
            {
                GenreId = Dto.GenreId,
                Title = Dto.Title,
                Rate = Dto.Rate,
                StoryLine = Dto.StoryLine,
                year = Dto.year,
                Poster = dataStrem.ToArray()
            };
            await _MovieReposaitory.CreateMovie(movie);
            
            return Ok(movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int genreId=0 )
        {
            var movie = await _MovieReposaitory.GeTAll(genreId);
            return Ok(movie);
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Edit(int Id, [FromForm] MovieDto Dto)
        {
            var SelectedMovie = await _MovieReposaitory.GetMovieById(Id);
            if (SelectedMovie is null) return BadRequest("No Movie Found");
            
            var AllowedGenre = await _genreReposaitory.AllowedGenre(Dto.GenreId);
            if (!AllowedGenre) return BadRequest("Invalid Genre");

            if (Dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(Dto.Poster.FileName.ToLower())))
                    return BadRequest("Invalid Extension, Only .png and .jpg");

                if (Dto.Poster.Length > _maxAllowedSize)
                    return BadRequest("File must Be 1MB or smaller");
                using var dataStrem = new MemoryStream();
                await Dto.Poster.CopyToAsync(dataStrem);
                SelectedMovie.Poster = dataStrem.ToArray();
            }

            SelectedMovie.StoryLine = Dto.StoryLine;
            SelectedMovie.Rate = Dto.Rate;
            SelectedMovie.GenreId = Dto.GenreId;
            SelectedMovie.Title = Dto.Title;
            SelectedMovie.year = Dto.year;
            _MovieReposaitory.Update(SelectedMovie);
            return Ok(SelectedMovie);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMovie(int Id)
        {
            var Movie = await _MovieReposaitory.GetMovieById(Id);
            if (Movie == null) return BadRequest();
            _MovieReposaitory.DeleteMovie(Movie);
            return Ok(Movie);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById (int Id)
        {
            var movie = await _MovieReposaitory.GetMovieById(Id);
            if (movie == null) return NotFound("No Movie With this Id");
            return Ok(movie);

        }
    }
}
