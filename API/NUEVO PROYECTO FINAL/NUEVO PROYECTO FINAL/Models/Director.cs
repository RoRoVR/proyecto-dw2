using System.ComponentModel.DataAnnotations;

namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Director
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }

    }
}
