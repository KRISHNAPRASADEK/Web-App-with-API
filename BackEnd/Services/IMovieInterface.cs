using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Services
{
    public interface IMovieInterface
    {
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<IEnumerable<MovieDetailes>> GetMovieDetailes();
        Task<MovieDto> Get(int id);
        Task<string> PostMovie(MovieDto movie);
        Task<string> DeleteMovie(int id);
        Task<string> PutMovie(int id, MovieDto movie);
    }
}
