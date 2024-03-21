using BackForoP.Models;
using System.Data.SqlClient;
using System.Data;

namespace BackForoP.Data
{
    public class UsuarioD
    {
        ConexionBD conexion = new ConexionBD();

        public async Task<List<UsuarioE>> listarUsuariosSP()
        {
            List<UsuarioE> listaUsuarios = new List<UsuarioE>();

            using (var cadenaSQL = new SqlConnection(conexion.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("sp_ListarUsuarios", cadenaSQL))
                {
                    await cadenaSQL.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            UsuarioE usuario = new UsuarioE();

                            usuario.documento = item["documento"] != DBNull.Value ? (string)item["documento"] : null;
                            usuario.nombre = item["nombre"] != DBNull.Value ? (string)item["nombre"] : null;
                            usuario.apellido = item["apellido"] != DBNull.Value ? (string)item["apellido"] : null;
                            usuario.email = item["email"] != DBNull.Value ? (string)item["email"] : null;
                            usuario.password = item["password"] != DBNull.Value ? (string)item["password"] : null;
                            usuario.estado = item["estado"] != DBNull.Value ? (string)item["estado"] : null;
                            usuario.rol = item["rol"] != DBNull.Value ? (string)item["rol"] : null;

                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }
            return listaUsuarios;

        }
    }
}
