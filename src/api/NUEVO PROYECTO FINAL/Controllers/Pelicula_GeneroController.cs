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
    public class Pelicula_GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public Pelicula_GeneroController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/Listar")]
        public async Task<ActionResult<List<Pelicula_Genero>>> Listar()
        {
            return await _context.pelicula_genero.Include("genero").Include("pelicula").ToListAsync();
        }

        [HttpGet]
        [Route("api/[controller]/ListarPorPelicula")]
        public async Task<ActionResult<List<Pelicula_Genero>>> ListarPorPelicula(int id)
        {
            return await _context.pelicula_genero.Where(x=>x.peliculaid == id).Include("genero").Include("pelicula").ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/Relacionar_genero_pelicula")]
        public async Task<ActionResult> Registrar(Pelicula_Genero pg)
        {
            if (pg.id == 0)
            {
                _context.pelicula_genero.Add(pg);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var pelicula_genero = await _context.pelicula_genero.FirstOrDefaultAsync(x => x.id == pg.id);
                if (pelicula_genero != null)
                {
                    pelicula_genero.generoid = pg.generoid;
                    pelicula_genero.peliculaid = pg.peliculaid;
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
            var pelicula_genero = await _context.pelicula_genero.FirstOrDefaultAsync(x => x.id == id);
            if (pelicula_genero != null)
            {
                _context.pelicula_genero.Remove(pelicula_genero);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
