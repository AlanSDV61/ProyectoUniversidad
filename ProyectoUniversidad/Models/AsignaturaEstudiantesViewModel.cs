namespace ProyectoUniversidad.Models
{
    public class AsignaturaEstudiantesViewModel
    {
        public int asignatura_id { get; set; }
        public string? asignatura_nombre { get; set; }

        public List<Estudiante>? Estudiantes { get; set; }
    }
}
