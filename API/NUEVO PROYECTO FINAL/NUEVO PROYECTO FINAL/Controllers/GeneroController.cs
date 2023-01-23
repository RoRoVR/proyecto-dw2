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
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GeneroController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/ListarGeneros")]
        public async Task<ActionResult<List<Genero>>> Listar()
        {
            return await _context.generos.ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/RegistrarGenero")]
        public async Task<ActionResult> Registrar(Genero g)
        {
            if (g.id == 0)
            {
                _context.generos.Add(g);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var genero = await _context.generos.FirstOrDefaultAsync(x => x.id == g.id);
                if (genero != null)
                {
                    genero.nombre = g.nombre;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/[controller]/EliminarGenero")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var genero = await _context.generos.FirstOrDefaultAsync(x => x.id == id);
            if (genero != null)
            {
                _context.generos.Remove(genero);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
