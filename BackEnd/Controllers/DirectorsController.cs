using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorInterface _directorInterface;

        public DirectorsController(IDirectorInterface directorInterface)
        {
            _directorInterface = directorInterface;
        }

        [HttpGet]
        public async Task<IEnumerable<DirectorDto>> GetAll()
        {
            var director = await _directorInterface.GetAll();

            if (director == null)
            {
                return default;
            }

            return director;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDto>> Get(int id)
        {

            var director = await _directorInterface.Get(id);

            if (director == null)
            {
                return NotFound();
            }


            return Ok(director);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DirectorDto director)
        {
            if (id != director.Id)
            {
                return BadRequest();
            }


            var director1 = await _directorInterface.Put(id, director);

            if (director1 == null)
            {
                return NotFound();
            }

            return Ok("Successfully updated");
        }


        [HttpPost]
        public async Task<ActionResult<DirectorDto>> Post(DirectorDto director)
        {
            var director1 = await _directorInterface.Post(director);

            if (director1 == null)
            {
                return Problem("Entity set 'TestEFContext.Director'  is null.");
            }

            return CreatedAtAction("Get", new { id = director1.Id }, director1);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var director = await _directorInterface.Delete(id);

            if (director == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}




