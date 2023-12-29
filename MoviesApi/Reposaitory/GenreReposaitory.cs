using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Reposaitory
{
    public class GenreReposaitory : IGenreReposaitory
    {
        public readonly ApplicationDbContext _context;

        public GenreReposaitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AllowedGenre(int id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreId == id);
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GeTAll()
        {

            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> GetGenreById(int Id)
        {
            var genere = await _context.Genres.FindAsync(Id);
            return genere;

        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
