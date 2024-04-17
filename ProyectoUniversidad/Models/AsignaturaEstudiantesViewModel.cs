﻿namespace ProyectoUniversidad.Models
{
    public class AsignaturaEstudiantesViewModel
    {
        public int asignatura_id { get; set; }
        public string? asignatura_nombre { get; set; }
        public string? asignatura_aula { get; set; }

        public int asignatura_creditos { get; set; }
        public int profesor_id { get; set; }
        public string? profesor_nombre { get; set; }

        public List<Estudiante>? Estudiantes { get; set; }
    }
}