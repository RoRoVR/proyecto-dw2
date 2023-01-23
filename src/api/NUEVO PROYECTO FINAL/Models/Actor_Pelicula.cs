using System.ComponentModel.DataAnnotations;

namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Actor_Pelicula
    {
        [Key]
        public int id { get; set; }
        public int peliculaid { get; set; }
        public Pelicula pelicula { get; set; }
        public int actorid { get; set; }
        public Actor actor { get; set; }
    }
}
