using BackEnd.Data;
using BackEnd.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BackEnd.Services
{
    public class MovieService : IMovieInterface
    {
        private readonly TestEFContext _context;
        private readonly IConfiguration _configuration;

        public MovieService(TestEFContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            try
            {
                if (_context.Movie == null)
                {
                    return default;
                }

                var movies = _context.Movie.Adapt<List<MovieDto>>();
                return movies;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<MovieDetailes>> GetMovieDetailes()
        {
            try
            {
                if (_context.Movie == null)
                {
                    return default;
                }

                // linq query syntax

                //var result = from a in _context.Movie
                //             join b in _context.Director
                //             on a.DirectorId equals b.Id
                //             join c in _context.Producer
                //             on a.ProducerId equals c.Id

                //             //where a.Title.StartsWith("f") 
                //             //orderby b.Name 
                //             select new { MovieId = a.Id, MovieName = a.Title, DirectorName = b.Name, ProducerName = c.Name };


                // linq method syntax

                //var result = _context.Movie.Join(_context.Director, M => M.DirectorId, D => D.Id, (M, D) => new { M, D })
                //    .Join(_context.Producer, MD => MD.M.ProducerId, P => P.Id, (MD, P) => new { MD, P })
                //    .Where(MDP => MDP.P.Name.StartsWith("F"))
                //    .OrderBy(MDP => MDP.MD.M.Title)
                //    .Select(s => new
                //    {
                //        MovieId = s.MD.M.Id,
                //        MovieName = s.MD.M.Title,
                //        DirectorName = s.MD.D.Name,
                //        ProducerName = s.P.Name
                //    });

                // dapper

                string conn = _configuration.GetConnectionString("TestEFContext");
                using(var connection=new SqlConnection(conn))
                {
                    connection.Open();
                    string query = @"select MovieId = m.Id, MovieName = m.Title, DirectorName = d.Name, ProducerName = p.Name 
                                   from Movie m
                                   inner join Director d on m.DirectorId = d.Id 
                                   inner join Producer p on p.Id = m.ProducerId
                                   where m.Language LIKE 'E%'";
                    var result = connection.Query(query).ToList();
                    var movies = result.Adapt<List<MovieDetailes>>();

                    return movies;
                }


    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MovieDto> Get(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PostMovie(MovieDto movie)
        {
            //using(TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
                try
                {
                    if (_context.Movie == null)
                    {
                        return null;
                    }

                    var movie1 = movie.Adapt<Movie>();
                    //_context.Movie.Add(movie1);
                    //await _context.SaveChangesAsync();
                    //transactionScope.Complete();
                    //return movie1;

                    string conn = _configuration.GetConnectionString("TestEFContext");
                    using (var connection = new SqlConnection(conn))
                    {
                        connection.Open();
                        string query = @"INSERT INTO [Movie]
                                       ([Title]
                                       ,[Language]
                                       ,[ReleaseYear]
                                       ,[ProducerId]
                                       ,[DirectorId])
                                     VALUES
                                       (@Title
                                       ,@Language
                                       ,@ReleaseYear
                                       ,@ProducerId
                                       ,@DirectorId)";
                        int result = connection.Execute(query, movie1);
                        
                        return result + " row is successfully inserted";
                    }
                    

                }
                catch (Exception ex)
                {
                    //transactionScope.Dispose();
                    throw ex;
                }
            //}
        }

        public async Task<string> DeleteMovie(int id)
        {
            try
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

                //_context.Movie.Remove(movie);
                //await _context.SaveChangesAsync();

                //return movie;

                string conn = _configuration.GetConnectionString("TestEFContext");
                using (var connection = new SqlConnection(conn))
                {
                    connection.Open();
                    string query = @"DELETE FROM [Movie]
                                      WHERE [Id]=@Id";
                    var result = connection.Execute(query,movie);
                    return result + " row is successfully deleted";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PutMovie(int id, MovieDto movie)
        {
            try
            {
                if (id != movie.Id)
                {
                    return null;
                }
                var movie1 = movie.Adapt<Movie>();
                //_context.Entry(movies).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                //return movies;

                string conn = _configuration.GetConnectionString("TestEFContext");
                using (var connection = new SqlConnection(conn))
                {
                    connection.Open();
                    string query = @"UPDATE [Movie]
                                      SET 
                                        [Title]=@Title
                                       ,[Language]=@Language
                                       ,[ProducerId]=@ProducerId
                                       ,[DirectorId]=@DirectorId
                                       WHERE [Id]=@Id";
                    var result = connection.Execute(query, movie1);
                    return result + " row is successfully updated";
                }

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
           
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
