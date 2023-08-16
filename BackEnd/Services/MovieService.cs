using BackEnd.Data;
using BackEnd.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
{
    public class MovieService : IMovieInterface
    {
        private readonly TestEFContext _context;

        public MovieService(TestEFContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            if (_context.Movie == null)
            {
                return default;
            }

            var movies = _context.Movie.Adapt<List<MovieDto>>();
            return movies;
        }

        public async Task<MovieDto> Get(int id)
        {
            if (_context.Movie == null)
            {
                return null;
            }
            var movie = _context.Movie.Find(id).Adapt<MovieDto>();

            if (movie == null)
            {
                return null;
            }


            return movie;
        }

        public async Task<Movie> PostMovie(MovieDto movie)
        {
            if (_context.Movie == null)
            {
                return null;
            }

            var movie1 = movie.Adapt<Movie>();
            _context.Movie.Add(movie1);
            await _context.SaveChangesAsync();

            return movie1;
        }

        public async Task<Movie> DeleteMovie(int id)
        {
            if (_context.Movie == null)
            {
                return null;
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return null;
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> PutMovie(int id, MovieDto movie)
        {
            if (id != movie.Id)
            {
                return null;
            }
            var movies = movie.Adapt<Movie>();
            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return movies;
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
