using System.ComponentModel.DataAnnotations;

namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Pelicula_Genero
    {
        [Key]
        public int id { get; set; }
        public int peliculaid { get; set; }
        public Pelicula pelicula { get; set; }
        public int generoid { get; set; }
        public Genero genero { get; set; }
    }
}
