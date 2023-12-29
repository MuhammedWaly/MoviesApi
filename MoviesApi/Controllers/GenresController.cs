using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Reposaitory;
using System.Reflection.Metadata.Ecma335;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreReposaitory _GenreReposaitory;

        public GenresController(IGenreReposaitory GenreReposaitory)
        {
            _GenreReposaitory = GenreReposaitory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var genera = await _GenreReposaitory.GeTAll();


            return Ok(genera);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenereDto Dto)
        {
            var genre = new Genre() { Name = Dto.Name };
            await _GenreReposaitory.CreateGenre(genre);
            
            return Ok(genre);

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGenre (int Id)
        {
           var genre = await _GenreReposaitory.GetGenreById(Id);
            _GenreReposaitory.DeleteGenre(genre);
            return Ok(genre);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> EditGenre(int Id , [FromBody]CreateGenereDto dto)
        {
            var genere = await _GenreReposaitory.GetGenreById(Id);
            if (genere == null)  return NotFound($"No Genre Found with this ID {Id}");
            genere.Name = dto.Name;
            _GenreReposaitory.Update(genere);
            return Ok(genere);

        }
    }
}
