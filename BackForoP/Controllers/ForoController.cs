﻿using BackForoP.Data;
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
        [HttpGet("ListarForosPorDocente")]
        public async Task<ActionResult<List<ForoE>>> ListarForosPorDocente([FromBody] UsuarioE usu)
        {
            var funcion = new ForoD();
            var listaForos = await funcion.listarForosPorDocente(usu.idUsuario);
            return Ok(listaForos);
        }



    }
}
