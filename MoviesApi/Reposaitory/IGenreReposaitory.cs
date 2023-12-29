using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Reposaitory
{
    public interface IGenreReposaitory
    {
        Task<IEnumerable<Genre>> GeTAll();
        Task<Genre> CreateGenre(Genre genre);
        Genre Update(Genre genre);
        Genre DeleteGenre(Genre genre);
        Task<Genre> GetGenreById(int Id);
        Task<bool> AllowedGenre(int id);
    }
}
