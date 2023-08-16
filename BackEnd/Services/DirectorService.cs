using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPPEF.Data;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Services
{
    public class DirectorService : IDirectorInterface
    {
        private readonly TestEFContext _context;

        public DirectorService(TestEFContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DirectorDto>> GetAll()
        {
            if (_context.Director == null)
            {
                return default;
            }

            var directors =_context.Director.Adapt<List<DirectorDto>>();
            return directors;
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
