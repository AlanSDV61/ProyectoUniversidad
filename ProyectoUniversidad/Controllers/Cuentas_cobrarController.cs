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
    public class Cuentas_cobrarController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Cuentas_cobrarController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas_cobrar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuentas_cobrar>>> GetCuentas_cobrar()
        {
            return await _context.Cuentas_cobrar.ToListAsync();
        }

        // GET: api/Cuentas_cobrar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuentas_cobrar>> GetCuentas_cobrar(int id)
        {
            var cuentas_cobrar = await _context.Cuentas_cobrar.FindAsync(id);

            if (cuentas_cobrar == null)
            {
                return NotFound();
            }

            return cuentas_cobrar;
        }

        // PUT: api/Cuentas_cobrar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentas_cobrar(int id, Cuentas_cobrar cuentas_cobrar)
        {
            if (id != cuentas_cobrar.cuenta_cobrar_id)
            {
                return BadRequest();
            }

            _context.Entry(cuentas_cobrar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cuentas_cobrarExists(id))
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

        // POST: api/Cuentas_cobrar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuentas_cobrar>> PostCuentas_cobrar(Cuentas_cobrar cuentas_cobrar)
        {
            _context.Cuentas_cobrar.Add(cuentas_cobrar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentas_cobrar", new { id = cuentas_cobrar.cuenta_cobrar_id }, cuentas_cobrar);
        }

        // DELETE: api/Cuentas_cobrar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentas_cobrar(int id)
        {
            var cuentas_cobrar = await _context.Cuentas_cobrar.FindAsync(id);
            if (cuentas_cobrar == null)
            {
                return NotFound();
            }

            _context.Cuentas_cobrar.Remove(cuentas_cobrar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cuentas_cobrarExists(int id)
        {
            return _context.Cuentas_cobrar.Any(e => e.cuenta_cobrar_id == id);
        }
    }
}
