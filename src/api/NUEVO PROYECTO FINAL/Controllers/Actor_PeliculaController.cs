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
    public class Actor_PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public Actor_PeliculaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/Listar")]
        public async Task<ActionResult<List<Actor_Pelicula>>> Listar()
        {
            return await _context.actor_pelicula.Include("genero").Include("pelicula").ToListAsync();
        }
        [HttpGet]
        [Route("api/[controller]/ListarPorPelicula")]
        public async Task<ActionResult<List<Actor_Pelicula>>> ListarPorPelicula(int id)
        {
            return await _context.actor_pelicula.Where(x => x.peliculaid == id).Include("actor").Include("pelicula").ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/Relacionar_actor_pelicula")]
        public async Task<ActionResult> Registrar(Actor_Pelicula ap)
        {
            if (ap.id == 0)
            {
                _context.actor_pelicula.Add(ap);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var actor_Pelicula = await _context.actor_pelicula.FirstOrDefaultAsync(x => x.id == ap.id);
                if (actor_Pelicula != null)
                {
                    actor_Pelicula.actorid = ap.actorid;
                    actor_Pelicula.peliculaid = ap.peliculaid;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/[controller]/EliminarRelacion")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var actor_Pelicula = await _context.actor_pelicula.FirstOrDefaultAsync(x => x.id == id);
            if (actor_Pelicula != null)
            {
                _context.actor_pelicula.Remove(actor_Pelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
