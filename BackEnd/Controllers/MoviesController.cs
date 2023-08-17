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
    public class MoviesController : ControllerBase
    {
        private readonly IMovieInterface _movieInterface;

        public MoviesController(IMovieInterface movieInterface)
        {
            _movieInterface = movieInterface;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAll()
        {
            var movie = await _movieInterface.GetMovies();

            if (movie == null)
            {
                return default;
            }

            return movie;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {

            var movie = await _movieInterface.Get(id);

            if (movie == null)
            {
                return NotFound();
            }


            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }


            var movie1 = await _movieInterface.PutMovie(id, movie);

            if (movie1 == null)
            {
                return NotFound();
            }

            return Ok("Successfully updated");
        }


        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieDto movie)
        {
            var movie1 = await _movieInterface.PostMovie(movie);

            if (movie1 == null)
            {
                return Problem("Entity set 'TestEFContext.Movie'  is null.");
            }

            return CreatedAtAction("Get", new { id = movie1.Id }, movie1);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieInterface.DeleteMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
