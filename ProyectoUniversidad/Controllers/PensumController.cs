using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using ProyectoUniversidad.Models;
using Serilog;

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensumController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PensumController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Pensum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pensum>>> GetPensum()
        {
            Log.Information("Solicitud de obtención de todos los pensum.");
            return await _context.Pensum.ToListAsync();
        }

        // GET: api/Pensum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pensum>> GetPensum(int id)
        {
            Log.Information("Solicitud de obtención del pensum con ID {ID}.", id);
            var pensum = await _context.Pensum.FindAsync(id);

            if (pensum == null)
            {
                Log.Warning("El pensum con ID {ID} no fue encontrado.", id);
                return NotFound();
            }

            return pensum;
        }

        // PUT: api/Pensum/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensum(int id, Pensum pensum)
        {
            if (id != pensum.carrera_id)
            {
                Log.Error("La ID del pensum en la ruta no coincide con la ID proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(pensum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("Pensum con ID {ID} actualizado correctamente.", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensumExists(id))
                {
                    Log.Warning("El pensum con ID {ID} no fue encontrado para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pensum
        [HttpPost]
        public async Task<ActionResult<Pensum>> PostPensum(Pensum pensum)
        {
            _context.Pensum.Add(pensum);
            try
            {
                await _context.SaveChangesAsync();
                Log.Information("Nuevo pensum creado con ID {ID}.", pensum.carrera_id);
                return CreatedAtAction("GetPensum", new { id = pensum.carrera_id }, pensum);
            }
            catch (DbUpdateException)
            {
                if (PensumExists(pensum.carrera_id))
                {
                    Log.Warning("El pensum con ID {ID} ya existe.", pensum.carrera_id);
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Pensum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(int id)
        {
            var pensum = await _context.Pensum.FindAsync(id);
            if (pensum == null)
            {
                Log.Warning("El pensum con ID {ID} no fue encontrado para eliminación.", id);
                return NotFound();
            }

            _context.Pensum.Remove(pensum);
            await _context.SaveChangesAsync();

            Log.Information("Pensum con ID {ID} eliminado correctamente.", id);
            return NoContent();
        }

        private bool PensumExists(int id)
        {
            return _context.Pensum.Any(e => e.carrera_id == id);
        }
    }
}
