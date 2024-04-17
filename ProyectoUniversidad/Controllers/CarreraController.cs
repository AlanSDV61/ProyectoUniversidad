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
    public class CarreraController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CarreraController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Carrera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrera>>> GetCarrera()
        {
            // Registro del evento de solicitud de obtención de todas las carreras
            Log.Information("Solicitud de obtención de todas las carreras.");

            return await _context.Carrera.ToListAsync();
        }

        // GET: api/Carrera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(int id)
        {
            // Registro del evento de solicitud de obtención de una carrera por su ID
            Log.Information("Solicitud de obtención de la carrera con ID {ID}.", id);

            var carrera = await _context.Carrera.FindAsync(id);

            if (carrera == null)
            {
                // Registro del evento cuando no se encuentra la carrera
                Log.Warning("La carrera con ID {ID} no fue encontrada.", id);
                return NotFound();
            }

            return carrera;
        }

        // PUT: api/Carrera/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrera(int id, Carrera carrera)
        {
            if (id != carrera.carrera_id)
            {
                // Registro del evento de error de solicitud incorrecta
                Log.Error("La ID de la carrera en la ruta no coincide con la ID de la carrera proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
                {
                    // Registro del evento cuando la carrera no se encuentra para actualización
                    Log.Warning("La carrera con ID {ID} no fue encontrada para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Registro del evento de actualización exitosa de la carrera
            Log.Information("Carrera con ID {ID} actualizada correctamente.", id);

            return NoContent();
        }

        // POST: api/Carrera
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(Carrera carrera)
        {
            _context.Carrera.Add(carrera);
            await _context.SaveChangesAsync();

            // Registro del evento de creación exitosa de una nueva carrera
            Log.Information("Nueva carrera creada con ID {ID}.", carrera.carrera_id);

            return CreatedAtAction("GetCarrera", new { id = carrera.carrera_id }, carrera);
        }

        // DELETE: api/Carrera/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                // Registro del evento cuando la carrera no se encuentra para eliminación
                Log.Warning("La carrera con ID {ID} no fue encontrada para eliminación.", id);
                return NotFound();
            }

            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();

            // Registro del evento de eliminación exitosa de la carrera
            Log.Information("Carrera con ID {ID} eliminada correctamente.", id);

            return NoContent();
        }

        // GET: api/Carrera/{idCarrera}/Pensum
        [HttpGet("{idCarrera}/Pensum")]
        public async Task<ActionResult<IEnumerable<PensumViewModel>>> GetPensumByCarrera(int idCarrera)
        {
            // Busca la carrera por su ID
            var carrera = await _context.Carrera.FindAsync(idCarrera);

            // Si la carrera no existe, devuelve un error 404
            if (carrera == null)
            {
                Log.Warning("No se encontró la carrera con ID {ID}.", idCarrera);
                return NotFound();
            }

            // Busca las filas de Pensum asociadas a la carrera específica
            var pensum = await _context.Pensum
                                        .Where(p => p.carrera_id == idCarrera)
                                        .ToListAsync();

            // Lista para almacenar los datos del pensum
            var pensumViewModels = new List<PensumViewModel>();

            // Itera sobre cada fila de Pensum
            foreach (var filaPensum in pensum)
            {
                // Busca el nombre de la asignatura por su ID
                var asignatura = await _context.Asignatura.FindAsync(filaPensum.asignatura_id);

                // Si la asignatura existe, agrega su nombre junto con el trimestre del pensum a la lista de ViewModels
                if (asignatura != null)
                {
                    var pensumViewModel = new PensumViewModel
                    {
                        asignatura_nombre = asignatura.asignatura_nombre,
                        trimestre_pensum = filaPensum.pensum_trimestre,
                        asignatura_creditos = asignatura.asignatura_creditos
                    };
                    pensumViewModels.Add(pensumViewModel);
                }
            }

            // Registro del evento de obtención exitosa del pensum de la carrera
            Log.Information("Pensum de la carrera con ID {ID} obtenido correctamente.", idCarrera);

            return pensumViewModels;
        }

        private bool CarreraExists(int id)
        {
            return _context.Carrera.Any(e => e.carrera_id == id);
        }
    }
}
