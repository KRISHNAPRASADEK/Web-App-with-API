using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Services
{
    public interface IDirectorInterface
    {
        Task<IEnumerable<DirectorDto>> GetAll();
        Task<DirectorDto> Get(int id);
        Task<Director> Post(DirectorDto director);
        Task<Director> Delete(int id);
        Task<Director> Put(int id, DirectorDto director);
    }
}
