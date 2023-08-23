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
    public class ProducersController : ControllerBase
    {
        private readonly IProducerInterface _producerrInterface;

        public ProducersController(IProducerInterface producerrInterface)
        {
            _producerrInterface = producerrInterface;
        }

        [HttpGet]
        public async Task<IEnumerable<ProducerDto>> GetAll()
        {
            var producer = await _producerrInterface.GetAll();

            if (producer == null)
            {
                return default;
            }

            return producer;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerDto>> Get(int id)
        {

            var producer = await _producerrInterface.Get(id);

            if (producer == null)
            {
                return NotFound();
            }


            return Ok(producer);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProducerDto producer)
        {
            if (id != producer.Id)
            {
                return BadRequest();
            }


            var producer1 = await _producerrInterface.Put(id, producer);

            if (producer1 == null)
            {
                return NotFound();
            }

            return Ok("Successfully updated");
        }


        [HttpPost]
        public async Task<ActionResult<ProducerDto>> Post(ProducerDto producer)
        {
            var producer1 = await _producerrInterface.Post(producer);

            if (producer1 == null)
            {
                return Problem("Entity set 'TestEFContext.Producer'  is null.");
            }

            return CreatedAtAction("Get", new { id = producer1.Id }, producer1);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _producerrInterface.Delete(id);

            if (producer == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}




