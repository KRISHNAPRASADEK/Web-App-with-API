using BackEnd.Data;
using BackEnd.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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

        public async Task<IEnumerable<MovieDetailes>> GetMovieDetailes()
        {
            if (_context.Movie == null)
            {
                return default;
            }

            //var result = from a in _context.Movie
            //             join b in _context.Director
            //             on a.DirectorId equals b.Id
            //             join c in _context.Producer
            //             on a.ProducerId equals c.Id

            //             //where a.Title.StartsWith("f") 
            //             //orderby b.Name 
            //             select new { MovieId = a.Id, MovieName = a.Title, DirectorName = b.Name, ProducerName = c.Name };

            var result = _context.Movie.Join(_context.Director, M => M.DirectorId, D => D.Id, (M, D) => new { M, D })
                .Join(_context.Producer, MD => MD.M.ProducerId, P => P.Id, (MD, P) => new { MD, P })
                .Where(MDP => MDP.P.Name.StartsWith("F"))
                .OrderBy(MDP => MDP.MD.M.Title)
                .Select(s => new
                {
                    MovieId = s.MD.M.Id,
                    MovieName = s.MD.M.Title,
                    DirectorName = s.MD.D.Name,
                    ProducerName = s.P.Name
                });

            var movies = result.Adapt<List<MovieDetailes>>();

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
            using(TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (_context.Movie == null)
                    {
                        return null;
                    }

                    var movie1 = movie.Adapt<Movie>();
                    _context.Movie.Add(movie1);
                    await _context.SaveChangesAsync();
                    transactionScope.Complete(); 
                    return movie1;

                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    throw ex;
                }
            }
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
