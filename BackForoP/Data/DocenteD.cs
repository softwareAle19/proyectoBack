using System.Data.SqlClient;
using System.Data;
using BackForoP.Models;

namespace BackForoP.Data
{
    public class DocenteD
    {

        ConexionBD conexion = new ConexionBD();

        public async Task<List<UsuarioE>> listarDocentesSP()
        {
            List<UsuarioE> listaDocentes = new List<UsuarioE>();

            using (var cadenaSQL = new SqlConnection(conexion.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("sp_ListarDocentes", cadenaSQL))
                {
                    await cadenaSQL.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            UsuarioE usuario = new UsuarioE();
                            usuario.idUsuario = item["idUsuario"] is int ? (int)item["idUsuario"] : 0;
                            usuario.documento = item["documento"] != DBNull.Value ? (string)item["documento"] : null;
                            usuario.nombre = item["nombre"] != DBNull.Value ? (string)item["nombre"] : null;
                            usuario.apellido = item["apellido"] != DBNull.Value ? (string)item["apellido"] : null;
                            usuario.email = item["email"] != DBNull.Value ? (string)item["email"] : null;
                            usuario.estado = item["estado"] != DBNull.Value ? (string)item["estado"] : null;
                            usuario.rol = item["rol"] != DBNull.Value ? (string)item["rol"] : null;

                            listaDocentes.Add(usuario);
                        }
                    }
                }
            }
            return listaDocentes;

        }
    }
}
