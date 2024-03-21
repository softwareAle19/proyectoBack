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
    }
}
