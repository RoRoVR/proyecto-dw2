using System.ComponentModel.DataAnnotations;

namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Genero
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
    }
}
