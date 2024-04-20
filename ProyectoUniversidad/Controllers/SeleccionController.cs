using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using UniversidadAPI.Models;
using Serilog; // Importar Serilog para el registro de eventos

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleccionController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SeleccionController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Seleccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seleccion>>> GetSeleccion()
        {
            // Registro del evento de solicitud de obtención de todas las selecciones
            Log.Information("Solicitud de obtención de todas las selecciones.");

            return await _context.Seleccion.ToListAsync();
        }

        // GET: api/Seleccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seleccion>> GetSeleccion(int id)
        {
            // Registro del evento de solicitud de obtención de una selección por su ID
            Log.Information("Solicitud de obtención de la selección con ID {ID}.", id);

            var seleccion = await _context.Seleccion.FindAsync(id);

            if (seleccion == null)
            {
                // Registro del evento cuando no se encuentra la selección
                Log.Warning("La selección con ID {ID} no fue encontrada.", id);
                return NotFound();
            }

            return seleccion;
        }

        // PUT: api/Seleccion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeleccion(int id, Seleccion seleccion)
        {
            if (id != seleccion.seleccion_id)
            {
                // Registro del evento de error de solicitud incorrecta
                Log.Error("La ID de la selección en la ruta no coincide con la ID de la selección proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(seleccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeleccionExists(id))
                {
                    // Registro del evento cuando la selección no se encuentra para actualización
                    Log.Warning("La selección con ID {ID} no fue encontrada para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Registro del evento de actualización exitosa de la selección
            Log.Information("Selección con ID {ID} actualizada correctamente.", id);

            return NoContent();
        }

        // POST: api/Seleccion
        [HttpPost]
        public async Task<ActionResult<Seleccion>> PostSeleccion(Seleccion seleccion)
        {
            _context.Seleccion.Add(seleccion);
            await _context.SaveChangesAsync();

            // Registro del evento de creación exitosa de una nueva selección
            Log.Information("Nueva selección creada con ID {ID}.", seleccion.seleccion_id);

            return CreatedAtAction("GetSeleccion", new { id = seleccion.seleccion_id }, seleccion);
        }

        // DELETE: api/Seleccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeleccion(int id)
        {
            var seleccion = await _context.Seleccion.FindAsync(id);
            if (seleccion == null)
            {
                // Registro del evento cuando la selección no se encuentra para eliminación
                Log.Warning("La selección con ID {ID} no fue encontrada para eliminación.", id);
                return NotFound();
            }

            _context.Seleccion.Remove(seleccion);
            await _context.SaveChangesAsync();

            // Registro del evento de eliminación exitosa de la selección
            Log.Information("Selección con ID {ID} eliminada correctamente.", id);

            return NoContent();
        }

        private bool SeleccionExists(int id)
        {
            return _context.Seleccion.Any(e => e.seleccion_id == id);
        }
    }
}
