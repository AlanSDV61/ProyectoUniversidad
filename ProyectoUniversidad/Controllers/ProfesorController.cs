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
using UniversidadAPI.Models;

namespace ProyectoUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProfesorController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesor()
        {
            Log.Information("Solicitud de obtención de todos los profesores.");
            return await _context.Profesor.ToListAsync();
        }

        // GET: api/Profesor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            Log.Information("Solicitud de obtención del profesor con ID {ID}.", id);
            var profesor = await _context.Profesor.FindAsync(id);

            if (profesor == null)
            {
                Log.Warning("El profesor con ID {ID} no fue encontrado.", id);
                return NotFound();
            }

            return profesor;
        }

        // PUT: api/Profesor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(int id, Profesor profesor)
        {
            if (id != profesor.profesor_id)
            {
                Log.Error("La ID del profesor en la ruta no coincide con la ID proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("Profesor con ID {ID} actualizado correctamente.", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
                {
                    Log.Warning("El profesor con ID {ID} no fue encontrado para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Profesor
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            _context.Profesor.Add(profesor);
            await _context.SaveChangesAsync();

            Log.Information("Nuevo profesor creado con ID {ID}.", profesor.profesor_id);
            return CreatedAtAction("GetProfesor", new { id = profesor.profesor_id }, profesor);
        }

        // DELETE: api/Profesor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null)
            {
                Log.Warning("El profesor con ID {ID} no fue encontrado para eliminación.", id);
                return NotFound();
            }

            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();

            Log.Information("Profesor con ID {ID} eliminado correctamente.", id);
            return NoContent();
        }

        [HttpGet("{id}/Asignaturas")]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetProfesorAsignaturas(int id)
        {
            // Busca el profesor por su ID
            var profesor = await _context.Profesor.FindAsync(id);

            // Si el profesor no existe, devuelve un error 404
            if (profesor == null)
            {
                Log.Warning("No se encontró el profesor con ID {ID}.", id);
                return NotFound();
            }

            var profesorAsignaturas = await _context.Asignatura
                                                .Where(pa => pa.profesor_id == id)
                                                .ToListAsync();

            // Lista para almacenar las asignaturas
            var asignaturas = new List<Asignatura>();

            // Itera sobre cada entrada en la tabla puente
            foreach (var profesorAsignatura in profesorAsignaturas)
            {
                // Busca la asignatura por su ID
                var asignatura = await _context.Asignatura.FindAsync(profesorAsignatura.asignatura_id);

                // Si la asignatura existe, agrégala a la lista de asignaturas
                if (asignatura != null)
                {
                    asignaturas.Add(asignatura);
                }
            }

            Log.Information("Asignaturas del profesor con ID {ID} obtenidas correctamente.", id);
            return asignaturas;
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesor.Any(e => e.profesor_id == id);
        }
    }
}
