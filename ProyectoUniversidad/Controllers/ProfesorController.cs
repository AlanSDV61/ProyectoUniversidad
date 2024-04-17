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
            return await _context.Profesor.ToListAsync();
        }

        // GET: api/Profesor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);

            if (profesor == null)
            {
                return NotFound();
            }

            return profesor;
        }

        // PUT: api/Profesor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(int id, Profesor profesor)
        {
            if (id != profesor.profesor_id)
            {
                return BadRequest();
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // POST: api/Profesor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            _context.Profesor.Add(profesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesor", new { id = profesor.profesor_id }, profesor);
        }

        // DELETE: api/Profesor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();

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

            return asignaturas;
        }


        private bool ProfesorExists(int id)
        {
            return _context.Profesor.Any(e => e.profesor_id == id);
        }
    }
}
