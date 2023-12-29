using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Reposaitory
{
    public class MovieReposaitory : IMovieReposaitory
    {
        private readonly ApplicationDbContext _context;

        public MovieReposaitory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return (movie);
        }

        public Movie DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GeTAll(int genreId = 0)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0).
                Include(m => m.Genre).
                OrderByDescending(m => m.Rate).ToListAsync();
        }

        public async Task<Movie> GetMovieById(int Id)
        {
            return await _context.Movies.FindAsync(Id);   
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
