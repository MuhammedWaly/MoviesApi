using MoviesApi.Models;

namespace MoviesApi.Reposaitory
{
    public interface IMovieReposaitory
    {
        Task<IEnumerable<Movie>> GeTAll(int genreId=0);
        Task<Movie> CreateMovie(Movie movie);
        Movie Update(Movie movie);
        Movie DeleteMovie(Movie movie);
        Task<Movie> GetMovieById(int Id);
    }
}
