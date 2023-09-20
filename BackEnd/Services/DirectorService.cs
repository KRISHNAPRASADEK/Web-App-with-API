using BackEnd.Data;
using BackEnd.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace BackEnd.Services
{
    public class DirectorService : IDirectorInterface
    {
        private readonly TestEFContext _context;
        private readonly IConfiguration _configuration;

        public DirectorService(TestEFContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<DirectorDto>> GetAll()
        {
            if (_context.Director == null)
            {
                return default;
            }

            var directors = _context.Director.Adapt<List<DirectorDto>>();
            return directors;
        }
        public async Task<IEnumerable<DirectorDetailes>> GetDirectorDetailes()
        {
            if (_context.Director == null)
            {
                return default;
            }

            string conn = _configuration.GetConnectionString("TestEFContext");
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                string query = @"select MovieName=m.Title, DirectorName = d.Name, ProducerName = p.Name
                                   from Director d
                                   inner join Movie m on d.Id = m.DirectorId 
                                   inner join Producer p on p.Id = m.ProducerId
                                   where m.Title LIKE 'd%'";
                var result = connection.Query(query).ToList();
                var directors = result.Adapt<List<DirectorDetailes>>();

                return directors;
            }

            //var directors = _context.Director.Adapt<List<DirectorDto>>();
            //return directors;
        }

        public async Task<DirectorDto> Get(int id)
        {
            if (_context.Director == null)
            {
                return null;
            }
            var director = _context.Director.Find(id).Adapt<DirectorDto>();

            if (director == null)
            {
                return null;
            }


            return director;
        }

        public async Task<Director> Post(DirectorDto director)
        {
            if (_context.Director == null)
            {
                return null;
            }

            var director1 = director.Adapt<Director>();
            _context.Director.Add(director1);
            await _context.SaveChangesAsync();

            return director1;
        }

        public async Task<Director> Delete(int id)
        {
            if (_context.Director == null)
            {
                return null;
            }
            var director = await _context.Director.FindAsync(id);
            if (director == null)
            {
                return null;
            }

            _context.Director.Remove(director);
            await _context.SaveChangesAsync();

            return director;
        }

        public async Task<Director> Put(int id, DirectorDto director)
        {
            if (id != director.Id)
            {
                return null;
            }
            var directors = director.Adapt<Director>();
            _context.Entry(directors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return directors;
        }

        private bool DirectorExists(int id)
        {
            return (_context.Director?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
