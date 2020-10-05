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
    public class ruletasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ruletasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ruletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ruleta>>> Getruleta()
        {
            return await _context.ruleta.ToListAsync();
        }

        // GET: api/ruletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ruleta>> Getruleta(int id)
        {
            var ruleta = await _context.ruleta.FindAsync(id);

            if (ruleta == null)
            {
                return NotFound();
            }


            return ruleta;
        }

        // PUT: api/ruletas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putruleta(int id, ruleta ruleta)
        {
            if (id != ruleta.id)
            {
                return BadRequest();
            }

            _context.Entry(ruleta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ruletaExists(id))
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

        // POST: api/ruletas
        [HttpPost]
        public async Task<ActionResult<ruleta>> Postruleta(ruleta ruleta)
        {
            _context.ruleta.Add(ruleta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getruleta", new { id = ruleta.id }, ruleta);
        }

        // DELETE: api/ruletas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ruleta>> Deleteruleta(int id)
        {
            var ruleta = await _context.ruleta.FindAsync(id);
            if (ruleta == null)
            {
                return NotFound();
            }

            _context.ruleta.Remove(ruleta);
            await _context.SaveChangesAsync();

            return ruleta;
        }

        private bool ruletaExists(int id)
        {
            return _context.ruleta.Any(e => e.id == id);
        }
    }
}
