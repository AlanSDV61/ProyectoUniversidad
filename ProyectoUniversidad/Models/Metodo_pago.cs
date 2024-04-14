using System.ComponentModel.DataAnnotations;

namespace ProyectoUniversidad.Models
{
    public class Metodo_pago
    {
        [Key]
        public int metodo_pago_id { get; set; }

        public required string nombre_metodo { get; set; }
    }
}
