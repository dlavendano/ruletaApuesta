using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIRuleta.Context;
using APIRuleta.Entities;

namespace APIRuleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apuestasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public apuestasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/apuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<apuesta>>> Getapuesta_1()
        {
            return await _context.apuesta.ToListAsync();
        }

        // GET: api/apuestas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<apuesta>> Getapuesta(int id)
        {
            var apuesta = await _context.apuesta.FindAsync(id);

            if (apuesta == null)
            {
                return NotFound();
            }

            return apuesta;
        }

        // PUT: api/apuestas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putapuesta(int id, apuesta apuesta)
        {
            if (id != apuesta.id)
            {
                return BadRequest();
            }

            _context.Entry(apuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!apuestaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/apuestas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<apuesta>> Postapuesta(apuesta apuesta)
        {
            _context.apuesta.Add(apuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getapuesta", new { id = apuesta.id }, apuesta);
        }

        // DELETE: api/apuestas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<apuesta>> Deleteapuesta(int id)
        {
            var apuesta = await _context.apuesta.FindAsync(id);
            if (apuesta == null)
            {
                return NotFound();
            }

            _context.apuesta.Remove(apuesta);
            await _context.SaveChangesAsync();

            return apuesta;
        }

        private bool apuestaExists(int id)
        {
            return _context.apuesta.Any(e => e.id == id);
        }
    }
}
