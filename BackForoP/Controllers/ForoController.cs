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
        [HttpGet("ListarForos")]
        public async Task<ActionResult<List<ForoE>>> ListarForos()
        {
            var funcion = new ForoD();
            var listaForos = await funcion.listarForos();
            return Ok(listaForos);
        }

    }
}
