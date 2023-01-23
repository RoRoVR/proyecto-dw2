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
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ActorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/ListarActores")]
        public async Task<ActionResult<List<Actor>>> Listar()
        {
            return await _context.actores.ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/RegistrarActor")]
        public async Task<ActionResult> Registrar(Actor a)
        {
            if (a.id == 0)
            {
                _context.actores.Add(a);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var actor = await _context.actores.FirstOrDefaultAsync(x => x.id == a.id);
                if (actor != null)
                {
                    actor.nombre = a.nombre;
                    actor.apellido = a.apellido;
                    actor.descripcion = a.descripcion;
                    actor.foto = a.foto;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/[controller]/EliminarActor")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var actor = await _context.actores.FirstOrDefaultAsync(x => x.id == id);
            if (actor != null)
            {
                _context.actores.Remove(actor);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
