using BackForoP.Data;
using BackForoP.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace BackForoP.Controllers
{
    [EnableCors("ReglasCors")] //Habilitar los cors 

    [Route("api/[controller]")]
    [ApiController]
    public class InicioController : ControllerBase
    {
        private readonly UsuarioD usuarioDatos;

        public InicioController(UsuarioD usuarioD)
        {
            usuarioDatos = usuarioD;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<List<UsuarioE>>> ListarUsuarios()
        {
            var listaUsuarios = await usuarioDatos.listarUsuariosSP();
            return Ok(listaUsuarios);
        }

        [HttpPost("IniciarSesion")]
        public async Task<ActionResult<UsuarioE>> IniciarSesion([FromBody] UsuarioE usu)
        {
            // Verificar que se han proporcionado credenciales
            if (string.IsNullOrEmpty(usu.email) || string.IsNullOrEmpty(usu.password))
            {
                return BadRequest("El correo electrónico y la contraseña son obligatorios.");
            }

            var listaUsuarios = await usuarioDatos.listarUsuariosSP();

            // Buscar al usuario por correo electrónico en la lista de usuarios
            var usuario = listaUsuarios.FirstOrDefault(u => u.email == usu.email);

            // Verificar si se encontró un usuario y si la contraseña coincide
            if (usuario != null && usuario.password == usu.password)
            {
                var usuarioInfo = new UsuarioE
                {
                    idUsuario = usuario.idUsuario,
                    documento = usuario.documento,
                    nombre = usuario.nombre,
                    apellido = usuario.apellido,
                    email = usuario.email,
                    estado = usuario.estado,
                    imagen = usuario.imagen,
                    rol = usuario.rol
                };
                return Ok(usuarioInfo);
            }

            // Devolver una respuesta 401 Unauthorized si las credenciales son inválidas
            return Unauthorized("Credenciales inválidas");
        }

    }




}

