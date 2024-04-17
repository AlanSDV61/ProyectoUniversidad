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
            return await _context.Carrera.ToListAsync();
        }

        // GET: api/Carrera/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            return carrera;
        }

        // PUT: api/Carrera/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrera(int id, Carrera carrera)
        {
            if (id != carrera.carrera_id)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carrera
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(Carrera carrera)
        {
            _context.Carrera.Add(carrera);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrera", new { id = carrera.carrera_id }, carrera);
        }

        // DELETE: api/Carrera/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{idCarrera}/Pensum")]
        public async Task<ActionResult<IEnumerable<PensumViewModel>>> GetPensumByCarrera(int idCarrera)
        {
            // Busca la carrera por su ID
            var carrera = await _context.Carrera.FindAsync(idCarrera);

            // Si la carrera no existe, devuelve un error 404
            if (carrera == null)
            {
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

            return pensumViewModels;
        }

        private bool CarreraExists(int id)
        {
            return _context.Carrera.Any(e => e.carrera_id == id);
        }
    }
}
