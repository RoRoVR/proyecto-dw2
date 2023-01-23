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
    public class DirectorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DirectorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/[controller]/ListarDirectores")]
        public async Task<ActionResult<List<Director>>> Listar()
        {
            return await _context.directores.ToListAsync();
        }
        [HttpPost]
        [Route("api/[controller]/RegistrarDirector")]
        public async Task<ActionResult> Registrar(Director d)
        {
            if (d.id == 0)
            {
                _context.directores.Add(d);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var director = await _context.directores.FirstOrDefaultAsync(x => x.id == d.id);
                if (director != null)
                {
                    director.nombre = d.nombre;
                    director.apellido = d.apellido;
                    director.descripcion = d.descripcion;
                    director.foto = d.foto;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/[controller]/EliminarDirector")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var director = await _context.directores.FirstOrDefaultAsync(x => x.id == id);
            if (director != null)
            {
                _context.directores.Remove(director);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
