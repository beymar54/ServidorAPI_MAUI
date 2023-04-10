using System.ComponentModel.DataAnnotations;

namespace ServidorAPI_MAUI.Models
{
    public class Plato
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public string Ingredientes { get; set; } 
    }
}
