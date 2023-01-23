using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUEVO_PROYECTO_FINAL.Context;
using NUEVO_PROYECTO_FINAL.Models;

namespace NUEVO_PROYECTO_FINAL.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors("Politica1")]
    //[Authorize]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PeliculaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/ListarPeliculas")]
        public async Task<ActionResult<List<Pelicula>>> Listar()
        {
            return await _context.peliculas.ToListAsync();
        }
        [HttpGet]
        [Route("api/[controller]/PeliculaActores")]
        public async Task<ActionResult<List<Actor_Pelicula>>> MostrarActores(int id)
        {
            return await _context.actor_pelicula.Where(x=>x.pelicula.id == id).Include("actor").Include("pelicula").ToListAsync();
        }
        [HttpPost]
        [Authorize]
        [Route("api/[controller]/RegistrarPelicula")]
        public async Task<ActionResult> Registrar(Pelicula p)
        {
            if(p.id == 0) {
                p.estado = 1;
                _context.peliculas.Add(p);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var pelicula = await _context.peliculas.FirstOrDefaultAsync(x=>x.id == p.id);
                if(pelicula != null)
                {
                    pelicula.titulo = p.titulo;
                    pelicula.titulo_original = p.titulo_original;
                    pelicula.duracion = p.duracion;
                    pelicula.anio = p.anio;
                    pelicula.portada = p.portada;
                    pelicula.sinopsis = p.sinopsis;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        [Route("api/[controller]/DesactivarPelicula")]
        public async Task<ActionResult> Desactivar(int id)
        {
            var pelicula = await _context.peliculas.FirstOrDefaultAsync(x => x.id == id);
            if (pelicula != null)
            {
                pelicula.estado = 0;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPost]
        [Authorize]
        [Route("api/[controller]/ActivarPelicula")]
        public async Task<ActionResult> Activar(int id)
        {
            var pelicula = await _context.peliculas.FirstOrDefaultAsync(x => x.id == id);
            if (pelicula != null)
            {
                pelicula.estado = 1;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
        [HttpGet]
        [Route("api/[controller]/BuscarPeliculaNombre")]
        public async Task<ActionResult<List<Pelicula>>> Buscar(string nombre)
        {
            return await _context.peliculas.Where(x => x.titulo.Contains(nombre)).ToListAsync();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/[controller]/EliminarPelicula")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var pelicula = await _context.peliculas.FirstOrDefaultAsync(x => x.id == id);
            if (pelicula != null)
            {
                _context.peliculas.Remove(pelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
