using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUEVO_PROYECTO_FINAL.Context;

namespace NUEVO_PROYECTO_FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Politica1")]
    public class ImagenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ImagenController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        [HttpPost]
        [Route("api/[controller]/SubirFoto")]
        public async Task<ActionResult> Set_Image([FromForm] IFormFile image)
        {
            string Nombre = Path.GetFileNameWithoutExtension(image.FileName);
            string Extencion = Path.GetExtension(image.FileName);
            Nombre = Nombre + DateTime.Now.ToString("yymmssfff") + Extencion;
            if (image == null || image.Length == 0)
                return BadRequest("No se ha proporcionado archivo");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", Nombre);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return Content(path);
        }*/
        [HttpPost]
        [Route("api/[controller]/SubirFoto")]
        public async Task<ActionResult> Set_Image([FromForm] IFormFile image)
        {
            string Nombre = Path.GetFileNameWithoutExtension(image.FileName);
            string Extencion = Path.GetExtension(image.FileName);
            Nombre = Nombre + DateTime.Now.ToString("yymmssfff") + Extencion;
            if (image == null || image.Length == 0)
                return BadRequest("No se ha proporcionado archivo");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes", Nombre);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var fileBytes = System.IO.File.ReadAllBytes(path);
            string s = Convert.ToBase64String(fileBytes);
            s = "data:image/png;base64,"+s;
            return Content(s);
        }

        [HttpPost]
        [Route("api/[controller]/EliminarImagen")]
        public IActionResult Delete_Image(string direccion)
        {
            try
            {
                System.IO.File.Delete(direccion);
                return Content("eliminado");
            }
            catch
            {
                return Content("error no se encontro la imgen");
            }
        }
    }
}
