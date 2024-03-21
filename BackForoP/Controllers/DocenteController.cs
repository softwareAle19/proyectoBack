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
    }
}
