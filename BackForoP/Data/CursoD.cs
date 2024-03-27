using BackForoP.Models;
using System.Data.SqlClient;
using System.Data;

namespace BackForoP.Data
{
    public class CursoD
    {
        ConexionBD conexion = new ConexionBD();

        public async Task<List<CursoE>> listarCursos()
        {
            List<CursoE> listaCursos = new List<CursoE>();

            using (var cadenaSQL = new SqlConnection(conexion.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("sp_ListarCursos", cadenaSQL))
                {
                    await cadenaSQL.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            CursoE curso = new CursoE();
                            curso.idCurso = item["idCurso"] is int ? (int)item["idCurso"] : 0;
                            curso.nomenclatura = item["nomenclatura"] != DBNull.Value ? (string)item["nomenclatura"] : null;
                            curso.numero = item["numero"] is int ? (int)item["numero"] : 0;

                            listaCursos.Add(curso);

                        }
                    }
                }
            }
            return listaCursos;

        }
    }
}
