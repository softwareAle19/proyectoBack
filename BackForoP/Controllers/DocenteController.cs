using BackForoP.Data;
using BackForoP.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackForoP.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class DocenteController : ControllerBase
    {
        [HttpGet("ListarDocentes")]
        public async Task<ActionResult<List<UsuarioE>>> ListarDocentes()
        {
            var funcion = new DocenteD();
            var listaDocentes = await funcion.listarDocentesSP();
            return Ok(listaDocentes);
        }


        [HttpPost("BuscarDocente")]
        public async Task<IActionResult> BuscarPorNombre([FromBody] UsuarioE usu)
        {
            if (string.IsNullOrWhiteSpace(usu.nombre))
            {
                return BadRequest("El nombre de usuario no puede estar vacío");
            }

            var funcion = new DocenteD();
            var listaDocentes = await funcion.listarDocentesSP();

            var nombreBusqueda = usu.nombre.ToLower(); // Convertir el nombre de búsqueda a minúsculas

            var docentesEncontrados = listaDocentes.Where(docente => docente.nombre.ToLower().Contains(nombreBusqueda)).ToList();

            if (docentesEncontrados.Any())
            {
                return Ok(docentesEncontrados);
            }
            else
            {
                return NotFound("Docente no encontrado");
            }
        }

    }

}

