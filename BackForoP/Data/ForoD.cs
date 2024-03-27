using BackForoP.Models;
using System.Data.SqlClient;
using System.Data;

namespace BackForoP.Data
{
    public class ForoD
    {
        ConexionBD conexion = new ConexionBD();

        public async Task<List<ForoE>> listarForos()
        {
            List<ForoE> listaForos = new List<ForoE>();

            using (var cadenaSQL = new SqlConnection(conexion.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("sp_ListarForos", cadenaSQL))
                {
                    await cadenaSQL.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            ForoE foro = new ForoE();
                            foro.idForo = item["idForo"] is int ? (int)item["idForo"] : 0;
                            foro.nombreForo = item["nombreForo"] != DBNull.Value ? (string)item["nombreForo"] : null;
                            foro.descripcion = item["descripcion"] != DBNull.Value ? (string)item["descripcion"] : null;
                            foro.imagenForo = item["imagenForo"] != DBNull.Value ? (byte[])item["imagenForo"] : null;
                            foro.fechaInicio = item["fechaInicio"] != DBNull.Value ? (DateTime)item["fechaInicio"] : DateTime.MinValue;
                            foro.fechaFin = item["fechaFin"] != DBNull.Value ? (DateTime)item["fechaFin"] : DateTime.MinValue;
                            foro.numero = item["numero"] is int ? (int)item["numero"] : 0;
                            foro.nomenclatura = item["nomenclatura"] != DBNull.Value ? (string)item["nomenclatura"] : null;

                            listaForos.Add(foro);

                        }
                    }
                }
            }
            return listaForos;

        }
    }
}
