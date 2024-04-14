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
    public class Factura_pagoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Factura_pagoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Factura_pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura_pago>>> GetFactura_pago()
        {
            return await _context.Factura_pago.ToListAsync();
        }

        // GET: api/Factura_pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura_pago>> GetFactura_pago(int id)
        {
            var factura_pago = await _context.Factura_pago.FindAsync(id);

            if (factura_pago == null)
            {
                return NotFound();
            }

            return factura_pago;
        }

        // PUT: api/Factura_pago/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura_pago(int id, Factura_pago factura_pago)
        {
            if (id != factura_pago.pago_id)
            {
                return BadRequest();
            }

            _context.Entry(factura_pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Factura_pagoExists(id))
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

        // POST: api/Factura_pago
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura_pago>> PostFactura_pago(Factura_pago factura_pago)
        {
            _context.Factura_pago.Add(factura_pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura_pago", new { id = factura_pago.pago_id }, factura_pago);
        }

        // DELETE: api/Factura_pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura_pago(int id)
        {
            var factura_pago = await _context.Factura_pago.FindAsync(id);
            if (factura_pago == null)
            {
                return NotFound();
            }

            _context.Factura_pago.Remove(factura_pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Factura_pagoExists(int id)
        {
            return _context.Factura_pago.Any(e => e.pago_id == id);
        }
    }
}
