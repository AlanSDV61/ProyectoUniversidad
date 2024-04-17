using UniversidadAPI.Models;

namespace ProyectoUniversidad.Models
{
    public class EstudianteAsignaturaCalificacionViewModel
    {
        public int EstudianteId { get; set; }
        public string EstudianteNombre { get; set; }
        public List<Asignatura> Asignaturas { get; set; }

        public List<Asignatura_seleccion> Asignatura_seleccion { get; set; }


    }
}
