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
            return await _context.Pensum.ToListAsync();
        }

        // GET: api/Pensum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pensum>> GetPensum(int id)
        {
            var pensum = await _context.Pensum.FindAsync(id);

            if (pensum == null)
            {
                return NotFound();
            }

            return pensum;
        }

        // PUT: api/Pensum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensum(int id, Pensum pensum)
        {
            if (id != pensum.carrera_id)
            {
                return BadRequest();
            }

            _context.Entry(pensum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensumExists(id))
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

        // POST: api/Pensum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pensum>> PostPensum(Pensum pensum)
        {
            _context.Pensum.Add(pensum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PensumExists(pensum.carrera_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPensum", new { id = pensum.carrera_id }, pensum);
        }

        // DELETE: api/Pensum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensum(int id)
        {
            var pensum = await _context.Pensum.FindAsync(id);
            if (pensum == null)
            {
                return NotFound();
            }

            _context.Pensum.Remove(pensum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PensumExists(int id)
        {
            return _context.Pensum.Any(e => e.carrera_id == id);
        }
    }
}
