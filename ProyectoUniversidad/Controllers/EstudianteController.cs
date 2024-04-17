﻿using System;
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
            return await _context.Estudiante.ToListAsync();
        }

        // GET: api/Estudiante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            
            var estudiante = await _context.Estudiante.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return estudiante;
        }

        // PUT: api/Estudiante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            
            if (id != estudiante.estudiante_id)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estudiante
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            _context.Estudiante.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiante", new { id = estudiante.estudiante_id }, estudiante);
        }

        // DELETE: api/Estudiante/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiante.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiante.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Estudiante/5/Selecciones
        [HttpGet("{id}/Selecciones")]
        public async Task<ActionResult<IEnumerable<Seleccion>>> GetEstudianteSelecciones(int id)
        {


            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
                return NotFound();
            }

            // Busca las selecciones relacionadas con este estudiante
            var selecciones = await _context.Seleccion
                                    .Where(s => s.estudiante_id == id)
                                    .ToListAsync();

            return selecciones;
        }

        [HttpGet("{id}/Asignaturas")]
        public async Task<ActionResult<IEnumerable<string>>> GetEstudianteAsignaturas(int id)
        {
            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
                return NotFound();
            }

            // Busca las selecciones del estudiante
            var selecciones = await _context.Seleccion
                                    .Where(s => s.estudiante_id == id)
                                    .ToListAsync();

            // Lista para almacenar los nombres de las asignaturas
            var nombresAsignaturas = new List<string>();

            // Itera sobre cada selección del estudiante
            foreach (var seleccion in selecciones)
            {
                // Busca las entradas en la tabla puente asignatura_seleccion para esta selección
                var asignaturasSeleccion = await _context.Asignatura_seleccion
                                                .Where(asignSel => asignSel.seleccion_id == seleccion.seleccion_id)
                                                .ToListAsync();

                // Para cada entrada en asignatura_seleccion, busca el nombre de la asignatura
                foreach (var asignaturaSeleccion in asignaturasSeleccion)
                {
                    // Busca el nombre de la asignatura por su ID
                    var asignatura = await _context.Asignatura.FindAsync(asignaturaSeleccion.asignatura_id);

                    // Si la asignatura existe, agrega su nombre a la lista de nombres de asignaturas
                    if (asignatura != null)
                    {
                        nombresAsignaturas.Add(asignatura.asignatura_nombre);

                    }
                }
            }

            return nombresAsignaturas;
        }

        [HttpGet("{id}/CuentasPorCobrar")]
        public async Task<ActionResult<IEnumerable<Cuentas_cobrar>>> GetEstudianteCuentasPorCobrar(int id)
        {
            // Busca el estudiante por su ID
            var estudiante = await _context.Estudiante.FindAsync(id);

            // Si el estudiante no existe, devuelve un error 404
            if (estudiante == null)
            {
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
