using Microsoft.EntityFrameworkCore;
using NUEVO_PROYECTO_FINAL.Models;

namespace NUEVO_PROYECTO_FINAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Pelicula> peliculas { get; set; }
        public DbSet<Genero> generos { get; set; }
        public DbSet<Actor> actores { get; set; }
        public DbSet<Director> directores { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Pelicula_Genero> pelicula_genero { get; set; }
        public DbSet<Actor_Pelicula> actor_pelicula { get; set; }
        public DbSet<Director_Pelicula> director_pelicula { get; set; }

    }
}
