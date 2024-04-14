using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using ProyectoUniversidad.Models;

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Asignatura_seleccionController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Asignatura_seleccionController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Asignatura_seleccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura_seleccion>>> GetAsignatura_seleccion()
        {
            return await _context.Asignatura_seleccion.ToListAsync();
        }

        // GET: api/Asignatura_seleccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura_seleccion>> GetAsignatura_seleccion(int id)
        {
            var asignatura_seleccion = await _context.Asignatura_seleccion.FindAsync(id);

            if (asignatura_seleccion == null)
            {
                return NotFound();
            }

            return asignatura_seleccion;
        }

        // PUT: api/Asignatura_seleccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignatura_seleccion(int id, Asignatura_seleccion asignatura_seleccion)
        {
            if (id != asignatura_seleccion.seleccion_id)
            {
                return BadRequest();
            }

            _context.Entry(asignatura_seleccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignatura_seleccionExists(id))
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

        // POST: api/Asignatura_seleccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignatura_seleccion>> PostAsignatura_seleccion(Asignatura_seleccion asignatura_seleccion)
        {
            _context.Asignatura_seleccion.Add(asignatura_seleccion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Asignatura_seleccionExists(asignatura_seleccion.seleccion_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAsignatura_seleccion", new { id = asignatura_seleccion.seleccion_id }, asignatura_seleccion);
        }

        // DELETE: api/Asignatura_seleccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura_seleccion(int id)
        {
            var asignatura_seleccion = await _context.Asignatura_seleccion.FindAsync(id);
            if (asignatura_seleccion == null)
            {
                return NotFound();
            }

            _context.Asignatura_seleccion.Remove(asignatura_seleccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Asignatura_seleccionExists(int id)
        {
            return _context.Asignatura_seleccion.Any(e => e.seleccion_id == id);
        }
    }
}
