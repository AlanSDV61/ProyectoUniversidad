namespace ProyectoUniversidad.Models
{
    public class LoginResponse
    {
        public string NombreUsuario { get; set; }
        public int Rol { get; set; }
        public bool Success { get; set; }
    }
}
