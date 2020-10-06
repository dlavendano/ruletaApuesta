using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIRuleta.Context;
using APIRuleta.Entities;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        // PUT: api/ruletas/open/5
        [HttpPut("open/{id}")]
        public async Task<IActionResult> OpenRuleta(int id)
        {
            var ruleta = await _context.ruleta.FindAsync(id);

            if (id != ruleta.id)
            {
                return BadRequest();
            }

            ruleta.estado = true;

            _context.SaveChanges();

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

        // PUT: api/ruletas/close/5
        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseRuleta(int id)
        {
            Random random = new System.Random();
            int winnerNumber = random.Next(36);
            DateTime localDate = DateTime.Now;
            var ruleta = await _context.ruleta.FindAsync(id);

            if (id != ruleta.id)
            {
                return BadRequest();
            }

            if (ruleta.estado == false)
            {
                return BadRequest();
            }
            ruleta.estado = false;
            ruleta.numero = winnerNumber;
            ruleta.fecha_juego = localDate;

            _context.SaveChanges();

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

            calcularApuestas(ruleta.id, winnerNumber);


            return NoContent();
        }

        //Actualiza resultados
        public async void calcularApuestas(int id, int winnerNumber)
        {
            var apuestas = _context.apuesta.Where(x => x.id_ruleta == id).ToList();
            foreach (var element in apuestas)
            {
                if (element.numero_apostado == winnerNumber)
                {
                    element.monto_ganado = element.monto_apostado * 5;
                    element.resultado = true;
                    _context.SaveChanges();
                    _context.Entry(element).State = EntityState.Modified;

                    
                }
                else
                {
                    element.monto_ganado = 0;
                    element.resultado = false;
                    _context.SaveChanges();
                    _context.Entry(element).State = EntityState.Modified;

                   
                }
                
            }
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
