namespace NUEVO_PROYECTO_FINAL.Models
{
    public class Director_Pelicula
    {
        public int id { get; set; }
        public int peliculaid { get; set; }
        public Pelicula pelicula { get; set; }
        public int directorid { get; set; }
        public Director director { get; set; }
    }
}
