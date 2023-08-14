using Microsoft.AspNetCore.Mvc;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Services
{
    public interface IProducerInterface
    {
        Task<IEnumerable<ProducerDto>> GetAll();
        Task<ProducerDto> Get(int id);
        Task<Producer> Post(ProducerDto producer);
        Task<Producer> Delete(int id);
        Task<Producer> Put(int id, ProducerDto producer);
    }
}