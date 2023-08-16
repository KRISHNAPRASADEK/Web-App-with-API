using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Services
{
    public interface IMovieInterface
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<MovieDto> Get(int id);
        Task<Movie> PostMovie(MovieDto movie);
        Task<Movie> DeleteMovie(int id);
        Task<Movie> PutMovie(int id, MovieDto movie);
    }
}
