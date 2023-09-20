using BackEnd.Data;
using BackEnd.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services
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
            try
            {
                if (_context.Producer == null)
                {
                    return default;
                }

                var producers = _context.Producer.Adapt<List<ProducerDto>>();
                return producers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ProducerDto> Get(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Producer> Post(ProducerDto producer)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Producer> Delete(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Producer> Put(int id, ProducerDto producer)
        {
            try
            {
                if (id != producer.Id)
                {
                    return null;
                }
                var producers = producer.Adapt<Producer>();
                _context.Entry(producers).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return producers;
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
        }

        private bool ProducerExists(int id)
        {
            return (_context.Producer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
