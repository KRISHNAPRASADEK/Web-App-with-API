using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPPEF.Data;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Services
{
    public class ProducerService : IProducerInterface
    {
        private readonly TestEFContext _context;

        public ProducerService(TestEFContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProducerDto>> GetAll()
        {
            if (_context.Producer == null)
            {
                return default;
            }

            var producers = _context.Producer.Adapt<List<ProducerDto>>();
            return producers;
        }

        public async Task<ProducerDto> Get(int id)
        {
            if (_context.Producer == null)
            {
                return null;
            }
            var movie = _context.Producer.Find(id).Adapt<ProducerDto>();

            if (movie == null)
            {
                return null;
            }


            return movie;
        }

        public async Task<Producer> Post(ProducerDto producer)
        {
            if (_context.Producer == null)
            {
                return null;
            }

            var producer1 = producer.Adapt<Producer>();
            _context.Producer.Add(producer1);
            await _context.SaveChangesAsync();

            return producer1;
        }

        public async Task<Producer> Delete(int id)
        {
            if (_context.Producer == null)
            {
                return null;
            }
            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return null;
            }

            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();

            return producer;
        }

        public async Task<Producer> Put(int id, ProducerDto producer)
        {
            if (id != producer.Id)
            {
                return null;
            }
            var producers = producer.Adapt<Producer>();
            _context.Entry(producers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return producers;
        }

        private bool ProducerExists(int id)
        {
            return (_context.Producer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
