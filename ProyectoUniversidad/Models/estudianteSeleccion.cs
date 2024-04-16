using UniversidadAPI.Models;

namespace ProyectoUniversidad.Models
{
    public class estudianteSeleccion
    {
        public int estudiante_id { get; set; }

        public List<Asignatura> lista_asignaturas { get; set; }
    }
}
