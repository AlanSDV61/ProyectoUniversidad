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
    public class ServicioController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ServicioController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicio()
        {
            try
            {
                // Registrar que se está solicitando la lista de servicios
                Log.Information("Solicitando lista de servicios");

                // Obtener la lista de servicios desde la base de datos
                var servicios = await _context.Servicio.ToListAsync();

                // Registrar que se ha obtenido la lista de servicios
                Log.Information("Lista de servicios obtenida");

                // Devolver la lista de servicios
                return servicios;
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción que ocurra
                Log.Error(ex, "Error al obtener la lista de servicios");
                throw;
            }
        }

        // GET: api/Servicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            try
            {
                // Registrar que se está solicitando un servicio específico por su ID
                Log.Information("Solicitando servicio con ID {ID}", id);

                // Obtener el servicio desde la base de datos por su ID
                var servicio = await _context.Servicio.FindAsync(id);

                // Si el servicio no se encuentra, registrar y devolver un error 404
                if (servicio == null)
                {
                    Log.Warning("Servicio con ID {ID} no encontrado", id);
                    return NotFound();
                }

                // Registrar que se ha obtenido el servicio
                Log.Information("Servicio con ID {ID} obtenido", id);

                // Devolver el servicio
                return servicio;
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción que ocurra
                Log.Error(ex, "Error al obtener el servicio con ID {ID}", id);
                throw;
            }
        }

        // PUT: api/Servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            try
            {
                // Registrar que se está actualizando un servicio por su ID
                Log.Information("Actualizando servicio con ID {ID}", id);

                // Verificar si el ID en la URL coincide con el ID del servicio proporcionado
                if (id != servicio.servicio_id)
                {
                    // Registrar y devolver un error de solicitud incorrecta
                    Log.Warning("ID en la URL ({URLID}) no coincide con el ID del servicio proporcionado ({BodyID})", id, servicio.servicio_id);
                    return BadRequest();
                }

                // Marcar el estado del servicio como modificado para actualizarlo en la base de datos
                _context.Entry(servicio).State = EntityState.Modified;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Registrar que se ha actualizado el servicio
                Log.Information("Servicio con ID {ID} actualizado", id);

                // Devolver una respuesta sin contenido
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Si hay un problema de concurrencia al actualizar el servicio, registrar y devolver un error 404
                if (!ServicioExists(id))
                {
                    Log.Warning("Servicio con ID {ID} no encontrado para actualizar", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción que ocurra
                Log.Error(ex, "Error al actualizar el servicio con ID {ID}", id);
                throw;
            }
        }

        // POST: api/Servicio
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            try
            {
                // Registrar que se está creando un nuevo servicio
                Log.Information("Creando un nuevo servicio");

                // Agregar el nuevo servicio al contexto
                _context.Servicio.Add(servicio);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Registrar que se ha creado el nuevo servicio
                Log.Information("Nuevo servicio creado con ID {ID}", servicio.servicio_id);

                // Devolver una respuesta con el nuevo servicio creado
                return CreatedAtAction("GetServicio", new { id = servicio.servicio_id }, servicio);
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción que ocurra
                Log.Error(ex, "Error al crear un nuevo servicio");
                throw;
            }
        }

        // DELETE: api/Servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            try
            {
                // Registrar que se está eliminando un servicio por su ID
                Log.Information("Eliminando servicio con ID {ID}", id);

                // Buscar el servicio en la base de datos por su ID
                var servicio = await _context.Servicio.FindAsync(id);

                // Si el servicio no se encuentra, registrar y devolver un error 404
                if (servicio == null)
                {
                    Log.Warning("Servicio con ID {ID} no encontrado para eliminar", id);
                    return NotFound();
                }

                // Eliminar el servicio del contexto
                _context.Servicio.Remove(servicio);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Registrar que se ha eliminado el servicio
                Log.Information("Servicio con ID {ID} eliminado", id);

                // Devolver una respuesta sin contenido
                return NoContent();
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción que ocurra
                Log.Error(ex, "Error al eliminar el servicio con ID {ID}", id);
                throw;
            }
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.servicio_id == id);
        }
    }
}
