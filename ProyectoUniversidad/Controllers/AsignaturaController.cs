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
    public class AsignaturaController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AsignaturaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Asignatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetAsignatura()
        {
            // Registro del evento de solicitud de obtención de todas las asignaturas
            Log.Information("Solicitud de obtención de todas las asignaturas.");

            return await _context.Asignatura.ToListAsync();
        }

        // GET: api/Asignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura(int id)
        {
            // Registro del evento de solicitud de obtención de una asignatura por su ID
            Log.Information("Solicitud de obtención de la asignatura con ID {ID}.", id);

            var asignatura = await _context.Asignatura.FindAsync(id);

            if (asignatura == null)
            {
                // Registro del evento cuando no se encuentra la asignatura
                Log.Warning("La asignatura con ID {ID} no fue encontrada.", id);
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
                // Registro del evento de error de solicitud incorrecta
                Log.Error("La ID de la asignatura en la ruta no coincide con la ID de la asignatura proporcionada en el cuerpo de la solicitud.");
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
                    // Registro del evento cuando la asignatura no se encuentra para actualización
                    Log.Warning("La asignatura con ID {ID} no fue encontrada para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Registro del evento de actualización exitosa de la asignatura
            Log.Information("Asignatura con ID {ID} actualizada correctamente.", id);

            return NoContent();
        }

        // POST: api/Asignatura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignatura>> PostAsignatura(Asignatura asignatura)
        {
            _context.Asignatura.Add(asignatura);
            await _context.SaveChangesAsync();

            // Registro del evento de creación exitosa de una nueva asignatura
            Log.Information("Nueva asignatura creada con ID {ID}.", asignatura.asignatura_id);

            return CreatedAtAction("GetAsignatura", new { id = asignatura.asignatura_id }, asignatura);
        }

        // DELETE: api/Asignatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura(int id)
        {
            var asignatura = await _context.Asignatura.FindAsync(id);
            if (asignatura == null)
            {
                // Registro del evento cuando la asignatura no se encuentra para eliminación
                Log.Warning("La asignatura con ID {ID} no fue encontrada para eliminación.", id);
                return NotFound();
            }

            _context.Asignatura.Remove(asignatura);
            await _context.SaveChangesAsync();

            // Registro del evento de eliminación exitosa de la asignatura
            Log.Information("Asignatura con ID {ID} eliminada correctamente.", id);

            return NoContent();
        }

        [HttpGet("EstudiantesPorAsignatura/{id}")]
        public async Task<ActionResult<AsignaturaEstudiantesViewModel>> GetEstudiantesPorAsignatura(int id)
        {
            // Obtener los datos de la asignatura
            var asignatura = await _context.Asignatura.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound(); // Si la asignatura no se encuentra, devolver un error 404
            }

            // Obtener los datos del profesor asociado a la asignatura
            var profesor = await _context.Profesor.FindAsync(asignatura.profesor_id);
            if (profesor == null)
            {
                return NotFound(); // Si el profesor no se encuentra, devolver un error 404
            }

            // Obtener todas las selecciones relacionadas con la asignatura
            var asignaturaSeleccion = await _context.Asignatura_seleccion
                                                .Where(asigSel => asigSel.asignatura_id == id)
                                                .ToListAsync();

            // Lista para almacenar los estudiantes encontrados
            var estudiantes = new List<Estudiante>();

            foreach (var asigSel in asignaturaSeleccion)
            {
                // Buscar la selección asociada a esta entrada en AsignaturaSeleccion
                var seleccion = await _context.Seleccion.FindAsync(asigSel.seleccion_id);

                // Si la selección existe, buscar el estudiante asociado
                if (seleccion != null)
                {
                    var estudiante = await _context.Estudiante.FindAsync(seleccion.estudiante_id);
                    if (estudiante != null)
                    {
                        estudiantes.Add(estudiante);
                    }
                }
            }

            // Construir el modelo de vista
            var viewModel = new AsignaturaEstudiantesViewModel
            {
                asignatura_id = asignatura.asignatura_id,
                asignatura_nombre = asignatura.asignatura_nombre,
                asignatura_aula = asignatura.asignatura_aula,
                asignatura_creditos = asignatura.asignatura_creditos,
                profesor_id = profesor.profesor_id,
                profesor_nombre = profesor.profesor_nombres + " " + profesor.profesor_apellidos,
                Estudiantes = estudiantes
            };

            return viewModel;
        }

        private bool AsignaturaExists(int id)
        {
            return _context.Asignatura.Any(e => e.asignatura_id == id);
        }
    }
}
