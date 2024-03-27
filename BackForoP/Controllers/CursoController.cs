﻿using BackForoP.Data;
using BackForoP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackForoP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        [HttpGet("ListarCursos")]
        public async Task<ActionResult<List<CursoE>>> ListarCursos([FromBody] UsuarioE usu)
        {
            var funcion = new CursoD();
            var listaCursos = await funcion.listarCursosPorDocente(usu.idUsuario);
            return Ok(listaCursos);
        }
    }
}
