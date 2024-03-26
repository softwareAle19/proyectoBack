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
    public class EstudianteController : ControllerBase
    {

        [HttpGet("ListarEstudiantes")]
        public async Task<ActionResult<List<UsuarioE>>> ListarEstudiantes()
        {
            var funcion = new EstudianteD();
            var listaEstudiantes = await funcion.listarEstudiantesSP();
            return Ok(listaEstudiantes);
        }

        [HttpPost("BuscarEstudiante")]
        public async Task<IActionResult> BuscarPorNombre([FromBody] UsuarioE usu)
        {
            if (string.IsNullOrWhiteSpace(usu.nombre))
            {
                return BadRequest("El nombre de usuario no puede estar vacío");
            }

            var funcion = new EstudianteD();
            var listaEstudiantes = await funcion.listarEstudiantesSP();

            var nombreBusqueda = usu.nombre.ToLower(); // Convertir nombre de búsqueda a minúsculas

            var estudiantesEncontrados = listaEstudiantes.Where(estudiante => estudiante.nombre.ToLower().Contains(nombreBusqueda)).ToList();
            
            if (estudiantesEncontrados.Any())
            {
                return Ok(estudiantesEncontrados);
            }
            else
            {
                return NotFound("Estudiante no encontrado");
            }
        }
    }
}
