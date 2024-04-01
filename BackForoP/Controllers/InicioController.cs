using BackForoP.Data;
using BackForoP.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

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

        [HttpPost("EnviarContraseña")]
        public async Task<IActionResult> EnviarContraseña([FromBody] UsuarioE usu)
        {
            // Verifico si el usuario existe y obtener su información
            var usuario = await usuarioDatos.verificarEmail(usu.email);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Valido que el correo proporcionado no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(usu.email))
            {
                return BadRequest("La dirección de correo electrónico del usuario es inválida");
            }

            

            // Obtener la contraseña del usuario
            var contraseña = usuario.password;

            try
            {
                // Mensaje de correo electrónico
                MailMessage correo = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                correo.From = new MailAddress("institucioncardenasgutierrez@gmail.com");
                correo.To.Add(usu.email);
                correo.Subject = "Recuperación de Contraseña";
                correo.Body = "Tu contraseña es: " + contraseña; 
                correo.IsBodyHtml = true;

                // Conf el cliente SMTP
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("institucioncardenasgutierrez@gmail.com", "btbhlqdsgsqgitmf");
                SmtpServer.EnableSsl = true;

                // Envio el correo electrónico
                SmtpServer.Send(correo);

                return Ok("Correo electrónico enviado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al enviar el correo electrónico: {ex.Message}");
            }
        }

    }




}

