using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoUniversidad.Context;
using ProyectoUniversidad.Models;
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
            return await _context.Asignatura.ToListAsync();
        }

        // GET: api/Asignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetAsignatura(int id)
        {
            var asignatura = await _context.Asignatura.FindAsync(id);

            if (asignatura == null)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Asignatura
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asignatura>> PostAsignatura(Asignatura asignatura)
        {
            _context.Asignatura.Add(asignatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignatura", new { id = asignatura.asignatura_id }, asignatura);
        }

        // DELETE: api/Asignatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura(int id)
        {
            var asignatura = await _context.Asignatura.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            _context.Asignatura.Remove(asignatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpGet("EstudiantesPorAsignatura/{id}")]
        //public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantesPorAsignatura(int id)
        //{
        //    var asignaturaSeleccion = await _context.Asignatura_seleccion
        //                                        .Where(asigSel => asigSel.asignatura_id == id)
        //                                        .ToListAsync();

        //    var estudiantes = new List<Estudiante>();

        //    foreach (var asigSel in asignaturaSeleccion)
        //    {
        //        var seleccion = await _context.Seleccion.FindAsync(asigSel.seleccion_id);

        //        if (seleccion != null)
        //        {
        //            var estudiante = await _context.Estudiante.FindAsync(seleccion.estudiante_id);
        //            if (estudiante != null)
        //            {
        //                estudiantes.Add(estudiante);
        //            }
        //        }
        //    }

        //    return estudiantes;
        //}

        //[HttpGet("EstudiantesPorAsignatura/{id}")]
        //public async Task<ActionResult<AsignaturaEstudiantesViewModel>> GetEstudiantesPorAsignatura(int id)
        //{
        //    var asignaturaSeleccion = await _context.Asignatura_seleccion
        //                                        .Where(asigSel => asigSel.asignatura_id == id)
        //                                        .ToListAsync();

        //    var estudiantes = new List<Estudiante>();

        //    foreach (var asigSel in asignaturaSeleccion)
        //    {
        //        var seleccion = await _context.Seleccion.FindAsync(asigSel.seleccion_id);

        //        if (seleccion != null)
        //        {
        //            var estudiante = await _context.Estudiante.FindAsync(seleccion.estudiante_id);
        //            if (estudiante != null)
        //            {
        //                estudiantes.Add(estudiante);
        //            }
        //        }
        //    }

        //    // Obtener los datos de la asignatura
        //    var asignatura = await _context.Asignatura.FindAsync(id);

        //    // Construir el modelo de vista
        //    var viewModel = new AsignaturaEstudiantesViewModel
        //    {
        //        asignatura_id = id,
        //        asignatura_nombre = asignatura != null ? asignatura.asignatura_nombre : null,
        //        Estudiantes = estudiantes
        //    };

        //    return viewModel;
        //}

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
