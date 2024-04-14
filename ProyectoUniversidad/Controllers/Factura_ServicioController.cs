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
    public class Factura_ServicioController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Factura_ServicioController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Factura_Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura_Servicio>>> GetFactura_servicio()
        {
            return await _context.Factura_servicio.ToListAsync();
        }

        // GET: api/Factura_Servicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura_Servicio>> GetFactura_Servicio(int id)
        {
            var factura_Servicio = await _context.Factura_servicio.FindAsync(id);

            if (factura_Servicio == null)
            {
                return NotFound();
            }

            return factura_Servicio;
        }

        // PUT: api/Factura_Servicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura_Servicio(int id, Factura_Servicio factura_Servicio)
        {
            if (id != factura_Servicio.factura_id)
            {
                return BadRequest();
            }

            _context.Entry(factura_Servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Factura_ServicioExists(id))
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

        // POST: api/Factura_Servicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura_Servicio>> PostFactura_Servicio(Factura_Servicio factura_Servicio)
        {
            _context.Factura_servicio.Add(factura_Servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura_Servicio", new { id = factura_Servicio.factura_id }, factura_Servicio);
        }

        // DELETE: api/Factura_Servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura_Servicio(int id)
        {
            var factura_Servicio = await _context.Factura_servicio.FindAsync(id);
            if (factura_Servicio == null)
            {
                return NotFound();
            }

            _context.Factura_servicio.Remove(factura_Servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Factura_ServicioExists(int id)
        {
            return _context.Factura_servicio.Any(e => e.factura_id == id);
        }
    }
}
