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
    public class EstudianteController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EstudianteController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Estudiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiante()
        {
            // Registro del evento de solicitud de obtención de todos los estudiantes
            Log.Information("Solicitud de obtención de todos los estudiantes.");

            return await _context.Estudiante.ToListAsync();
        }

        // GET: api/Estudiante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            // Registro del evento de solicitud de obtención de un estudiante por su ID
            Log.Information("Solicitud de obtención del estudiante con ID {ID}.", id);

            var estudiante = await _context.Estudiante.FindAsync(id);

            if (estudiante == null)
            {
                // Registro del evento cuando no se encuentra el estudiante
                Log.Warning("El estudiante con ID {ID} no fue encontrado.", id);
                return NotFound();
            }

            return estudiante;
        }

        // PUT: api/Estudiante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            // Registro del evento de solicitud de actualización de un estudiante por su ID
            Log.Information("Solicitud de actualización del estudiante con ID {ID}.", id);

            if (id != estudiante.estudiante_id)
            {
                // Registro del evento de error de solicitud incorrecta
                Log.Error("La ID del estudiante en la ruta no coincide con la ID del estudiante proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    // Registro del evento cuando el estudiante no se encuentra para actualización
                    Log.Warning("El estudiante con ID {ID} no fue encontrado para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Registro del evento de actualización exitosa del estudiante
            Log.Information("Estudiante con ID {ID} actualizado correctamente.", id);

            return NoContent();
        }

        // POST: api/Estudiante
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            // Registro del evento de creación de un nuevo estudiante
            Log.Information("Creación de un nuevo estudiante.");

            _context.Estudiante.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiante", new { id = estudiante.estudiante_id }, estudiante);
        }

        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            // Registro del evento de solicitud de eliminación de un estudiante por su ID
            Log.Information("Solicitud de eliminación del estudiante con ID {ID}.", id);

            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                // Registro del evento cuando el estudiante no se encuentra para eliminación
                Log.Warning("El estudiante con ID {ID} no fue encontrado para eliminación.", id);
                return NotFound();
            }

            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();

            // Registro del evento de eliminación exitosa del estudiante
            Log.Information("Estudiante con ID {ID} eliminado correctamente.", id);

            return NoContent();
        }

        // GET: api/Estudiante/5/Selecciones
        [HttpGet("{id}/Selecciones")]
        public async Task<ActionResult<IEnumerable<Seleccion>>> GetEstudianteSelecciones(int id)
        {
            // Registro del evento de solicitud de obtención de selecciones de un estudiante por su ID
            Log.Information("Solicitud de obtención de selecciones del estudiante con ID {ID}.", id);

            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
                Log.Warning("El estudiante con ID {ID} no fue encontrado para obtener selecciones.", id);
                return NotFound();
            }

            // Busca las selecciones relacionadas con este estudiante
            var selecciones = await _context.Seleccion
                                    .Where(s => s.estudiante_id == id)
                                    .ToListAsync();

            return selecciones;
        }

        // GET: api/Estudiante/5/Asignaturas
        [HttpGet("{id}/Asignaturas")]
        public async Task<ActionResult<IEnumerable<AsignaturaViewModel>>> GetEstudianteAsignaturas(int id)
        {
            // Registro del evento de solicitud de obtención de asignaturas de un estudiante por su ID
            Log.Information("Solicitud de obtención de asignaturas del estudiante con ID {ID}.", id);

            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
                Log.Warning("El estudiante con ID {ID} no fue encontrado para obtener asignaturas.", id);
                return NotFound();
            }

            // Busca las selecciones del estudiante
            var selecciones = await _context.Seleccion
                                        .Where(s => s.estudiante_id == id)
                                        .ToListAsync();

            // Lista para almacenar las asignaturas
            var asignaturas = new List<AsignaturaViewModel>();

            // Itera sobre cada selección del estudiante
            foreach (var seleccion in selecciones)
            {
                // Busca las entradas en la tabla puente asignatura_seleccion para esta selección
                var asignaturasSeleccion = await _context.Asignatura_seleccion
                                                .Where(asignSel => asignSel.seleccion_id == seleccion.seleccion_id)
                                                .ToListAsync();

                // Para cada entrada en asignatura_seleccion, busca el objeto completo de Asignatura
                foreach (var asignaturaSeleccion in asignaturasSeleccion)
                {
                    // Busca la asignatura por su ID
                    var asignatura = await _context.Asignatura.FindAsync(asignaturaSeleccion.asignatura_id);

                    // Si la asignatura existe, crea un objeto AsignaturaViewModel y asigna el nombre del profesor
                    if (asignatura != null)
                    {
                        var profesor = await _context.Profesor.FindAsync(asignatura.profesor_id);
                        if (profesor != null)
                        {
                            var asignaturaViewModel = new AsignaturaViewModel()
                            {
                                profesor_id = profesor.profesor_id,
                                asignatura_id = asignatura.asignatura_id,
                                asignatura_nombre = asignatura.asignatura_nombre,
                                profesor_nombre = profesor.profesor_nombres + " " + profesor.profesor_apellidos,
                                asignatura_aula = asignatura.asignatura_aula,
                                asignatura_creditos = asignatura.asignatura_creditos
                            };
                            asignaturas.Add(asignaturaViewModel);
                        }
                    }
                }
            }

            return asignaturas;
        }

        // GET: api/Estudiante/5/CuentasPorCobrar
        [HttpGet("{id}/CuentasPorCobrar")]
        public async Task<ActionResult<IEnumerable<Cuentas_cobrar>>> GetEstudianteCuentasPorCobrar(int id)
        {
            // Registro del evento de solicitud de obtención de cuentas por cobrar de un estudiante por su ID
            Log.Information("Solicitud de obtención de cuentas por cobrar del estudiante con ID {ID}.", id);

            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
                Log.Warning("El estudiante con ID {ID} no fue encontrado para obtener cuentas por cobrar.", id);
                return NotFound();
            }

            // Busca las cuentas por cobrar del estudiante
            var cuentasPorCobrar = await _context.Cuentas_cobrar
                                    .Where(c => c.estudiante_id == id)
                                    .ToListAsync();

            return cuentasPorCobrar;
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.estudiante_id == id);
        }
    }
}
