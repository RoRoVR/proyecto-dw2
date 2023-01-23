using System.ComponentModel.DataAnnotations;

namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Pelicula
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public string titulo_original { get; set; }
        public string anio { get; set; }
        public string duracion { get; set; }
        public string sinopsis { get; set; }
        public string portada { get; set; }
        public byte estado { get; set; }

    }
}
