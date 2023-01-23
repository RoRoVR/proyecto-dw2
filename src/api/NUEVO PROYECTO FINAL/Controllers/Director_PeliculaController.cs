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
    public class Director_PeliculaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public Director_PeliculaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/Listar")]
        public async Task<ActionResult<List<Director_Pelicula>>> Listar()
        {
            return await _context.director_pelicula.Include("director").Include("pelicula").ToListAsync();
        }
        [HttpGet]
        [Route("api/[controller]/ListarPorPelicula")]
        public async Task<ActionResult<List<Director_Pelicula>>> ListarPorPelicula(int id)
        {
            return await _context.director_pelicula.Where(x => x.peliculaid == id).Include("director").Include("pelicula").ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/Relacionar_director_pelicula")]
        public async Task<ActionResult> Registrar(Director_Pelicula dp)
        {
            if (dp.id == 0)
            {
                _context.director_pelicula.Add(dp);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var director_Pelicula = await _context.director_pelicula.FirstOrDefaultAsync(x => x.id == dp.id);
                if (director_Pelicula != null)
                {
                    director_Pelicula.directorid = dp.directorid;
                    director_Pelicula.peliculaid = dp.peliculaid;
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
            var director_Pelicula = await _context.director_pelicula.FirstOrDefaultAsync(x => x.id == id);
            if (director_Pelicula != null)
            {
                _context.director_pelicula.Remove(director_Pelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
