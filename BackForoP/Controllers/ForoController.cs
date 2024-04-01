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
    public class ForoController : ControllerBase
    {
        [HttpPost("ListarForosPorDocente")]
        public async Task<ActionResult<List<ForoE>>> ListarForosPorDocente([FromBody] UsuarioE usu)
        {
            var funcion = new ForoD();
            var listaForos = await funcion.listaForosPorDocente(usu.idUsuario);
            return Ok(listaForos);
        }

        [HttpPost("ListarForosPorEstudiante")]
        public async Task<ActionResult<List<ForoE>>> ListarForosPorEstudiante([FromBody] UsuarioE usu)
        {
            var funcion = new ForoD();
            var listaForos = await funcion.listaForosPorEstudiante(usu.idUsuario);
            return Ok(listaForos);
        }
    }
}
