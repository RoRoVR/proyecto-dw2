using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUEVO_PROYECTO_FINAL.Context;
using NUEVO_PROYECTO_FINAL.Models;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace NUEVO_PROYECTO_FINAL.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors("Politica1")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly string secretkey;
        public UsuarioController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
        }
        [HttpGet]
        [Authorize]
        [Route("api/[controller]/ListarUsuarios")]
        public async Task<ActionResult<List<Usuario>>> Listar()
        {
            return await _context.usuarios.ToListAsync();
        }

        [HttpPost]
        [Route("api/[controller]/Registar_Atualizar_Usuario")]
        public async Task<ActionResult> Registrar_Actualizar(Usuario u)
        {
            if (u.id == 0)
            {
                u.descripcion = "normal";
                _context.usuarios.Add(u);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.id == u.id);
                if (usuario != null)
                {
                    usuario.nombre = u.nombre;
                    usuario.apellido = u.apellido;
                    usuario.username = u.username;
                    usuario.password = u.password;
                    usuario.descripcion = u.descripcion;
                    usuario.foto = u.foto;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/[controller]/Usuario_Login")]
        public async Task<ActionResult<Usuario>> validar(string user, string pass)
        {
            var usuario = await _context.usuarios.SingleOrDefaultAsync(x=>x.username.Equals(user) && x.password.Equals(pass));
            if (usuario.descripcion.Equals("admin"))
            {
                if (usuario != null)
                {
                    var keybytes = Encoding.ASCII.GetBytes(secretkey);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.username));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenconfig = tokenHandler.CreateToken(tokenDescriptor);
                    var tokencreado = tokenHandler.WriteToken(tokenconfig);
                    return StatusCode(StatusCodes.Status200OK, new { token = tokencreado, usuario = usuario });
                }
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { usuario = usuario });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("api/[controller]/EliminarUsuario")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.id == id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
