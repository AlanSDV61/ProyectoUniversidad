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
    public class RolController : ControllerBase
    {
        private readonly AppDBContext _context;

        public RolController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Rol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRol()
        {
            Log.Information("Solicitud de obtención de todos los roles.");
            return await _context.Rol.ToListAsync();
        }

        // GET: api/Rol/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            Log.Information("Solicitud de obtención del rol con ID {ID}.", id);
            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
            {
                Log.Warning("El rol con ID {ID} no fue encontrado.", id);
                return NotFound();
            }

            return rol;
        }

        // PUT: api/Rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, Rol rol)
        {
            if (id != rol.rol_id)
            {
                Log.Error("La ID del rol en la ruta no coincide con la ID proporcionada en el cuerpo de la solicitud.");
                return BadRequest();
            }

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("Rol con ID {ID} actualizado correctamente.", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
                {
                    Log.Warning("El rol con ID {ID} no fue encontrado para actualización.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rol
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.Rol.Add(rol);
            await _context.SaveChangesAsync();

            Log.Information("Nuevo rol creado con ID {ID}.", rol.rol_id);
            return CreatedAtAction("GetRol", new { id = rol.rol_id }, rol);
        }

        // DELETE: api/Rol/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _context.Rol.FindAsync(id);
            if (rol == null)
            {
                Log.Warning("El rol con ID {ID} no fue encontrado para eliminación.", id);
                return NotFound();
            }

            _context.Rol.Remove(rol);
            await _context.SaveChangesAsync();

            Log.Information("Rol con ID {ID} eliminado correctamente.", id);
            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _context.Rol.Any(e => e.rol_id == id);
        }
    }
}
