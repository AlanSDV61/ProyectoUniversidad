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
    public class Tipo_documentoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Tipo_documentoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Tipo_documento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo_documento>>> GetTipo_documento()
        {
            return await _context.Tipo_documento.ToListAsync();
        }

        // GET: api/Tipo_documento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_documento>> GetTipo_documento(int id)
        {
            var tipo_documento = await _context.Tipo_documento.FindAsync(id);

            if (tipo_documento == null)
            {
                return NotFound();
            }

            return tipo_documento;
        }

        // PUT: api/Tipo_documento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo_documento(int id, Tipo_documento tipo_documento)
        {
            if (id != tipo_documento.tipo_documento_id)
            {
                return BadRequest();
            }

            _context.Entry(tipo_documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_documentoExists(id))
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

        // POST: api/Tipo_documento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipo_documento>> PostTipo_documento(Tipo_documento tipo_documento)
        {
            _context.Tipo_documento.Add(tipo_documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipo_documento", new { id = tipo_documento.tipo_documento_id }, tipo_documento);
        }

        // DELETE: api/Tipo_documento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo_documento(int id)
        {
            var tipo_documento = await _context.Tipo_documento.FindAsync(id);
            if (tipo_documento == null)
            {
                return NotFound();
            }

            _context.Tipo_documento.Remove(tipo_documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Tipo_documentoExists(int id)
        {
            return _context.Tipo_documento.Any(e => e.tipo_documento_id == id);
        }
    }
}
