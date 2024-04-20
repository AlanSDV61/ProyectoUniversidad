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
    public class Metodo_pagoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Metodo_pagoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Metodo_pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metodo_pago>>> GetMetodo_pago()
        {
            Log.Information("Solicitud de obtención de todos los métodos de pago.");
            return await _context.Metodo_pago.ToListAsync();
        }

        // GET: api/Metodo_pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metodo_pago>> GetMetodo_pago(int id)
        {
            Log.Information("Solicitud de obtención del método de pago con ID {ID}.", id);
            var metodo_pago = await _context.Metodo_pago.FindAsync(id);

            if (metodo_pago == null)
            {
                Log.Warning("El método de pago con ID {ID} no fue encontrado.", id);
                return NotFound();
            }

            return metodo_pago;
        }

        // PUT: api/Metodo_pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodo_pago(int id, Metodo_pago metodo_pago)
        {
            if (id != metodo_pago.metodo_pago_id)
            {
                Log.Error("La ID del método de pago en la ruta no coincide con la ID proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(metodo_pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("Método de pago con ID {ID} actualizado correctamente.", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Metodo_pagoExists(id))
                {
                    Log.Warning("El método de pago con ID {ID} no fue encontrado para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Metodo_pago
        [HttpPost]
        public async Task<ActionResult<Metodo_pago>> PostMetodo_pago(Metodo_pago metodo_pago)
        {
            _context.Metodo_pago.Add(metodo_pago);
            await _context.SaveChangesAsync();

            Log.Information("Nuevo método de pago creado con ID {ID}.", metodo_pago.metodo_pago_id);
            return CreatedAtAction("GetMetodo_pago", new { id = metodo_pago.metodo_pago_id }, metodo_pago);
        }

        // DELETE: api/Metodo_pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodo_pago(int id)
        {
            var metodo_pago = await _context.Metodo_pago.FindAsync(id);
            if (metodo_pago == null)
            {
                Log.Warning("El método de pago con ID {ID} no fue encontrado para eliminación.", id);
                return NotFound();
            }

            _context.Metodo_pago.Remove(metodo_pago);
            await _context.SaveChangesAsync();

            Log.Information("Método de pago con ID {ID} eliminado correctamente.", id);
            return NoContent();
        }

        private bool Metodo_pagoExists(int id)
        {
            return _context.Metodo_pago.Any(e => e.metodo_pago_id == id);
        }
    }
}
