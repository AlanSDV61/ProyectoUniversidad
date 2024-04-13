using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AsignaturaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Asignatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetAsignaturas()
        {
            return await _context.Asignaturas.ToListAsync();
        }

        // GET: api/Asignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura(int id)
        {
            var asignatura = await _context.Asignaturas.FindAsync(id);

            if (asignatura == null)
            {
                return NotFound();
            }

            return asignatura;
        }

        // PUT: api/Asignatura/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignatura(int id, Asignatura asignatura)
        {
            if (id != asignatura.asignatura_id)
            {
                return BadRequest();
            }

            _context.Entry(asignatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaturaExists(id))
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

        // POST: api/Asignatura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignatura>> PostAsignatura(Asignatura asignatura)
        {
            _context.Asignaturas.Add(asignatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignatura", new { id = asignatura.asignatura_id }, asignatura);
        }

        // DELETE: api/Asignatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura(int id)
        {
            var asignatura = await _context.Asignaturas.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            _context.Asignaturas.Remove(asignatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsignaturaExists(int id)
        {
            return _context.Asignaturas.Any(e => e.asignatura_id == id);
        }
    }
}
