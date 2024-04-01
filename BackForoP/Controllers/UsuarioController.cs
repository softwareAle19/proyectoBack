using BackForoP.Data;
using BackForoP.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BackForoP.Controllers
{
    [EnableCors("ReglasCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioD usuarioDatos;

        public UsuarioController(UsuarioD usuarioD)
        {
            usuarioDatos = usuarioD;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<List<UsuarioE>>> ListarUsuarios()
        {
            var listaUsuarios = await usuarioDatos.listarUsuariosSP();
            return Ok(listaUsuarios);
        }

        [HttpPost("EditarUsuarios")]
        public async Task<ActionResult<string>> EditarUsuarios([FromBody] UsuarioE usu)
        {
            int rst = await usuarioDatos.editarUsuarios(usu.idUsuario,usu.documento, usu.nombre, usu.apellido, usu.email, usu.password, usu.imagen);
            if (rst != 0)
            {
                return Ok("Usuario Editado");
            }
            else
            {
                return BadRequest("No se pudo editar el usuario");
            }
        }

    }
}
