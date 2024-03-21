using System.Data;
using System.Data.SqlClient;

namespace BackForoP.Data
{
    public class ConexionBD
    {
        private string connectionString = string.Empty;

        public ConexionBD()
        {
            var contructor = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile
                ("appsettings.json").Build();
            connectionString = contructor.GetSection
            ("ConnectionStrings:CadenaSQL").Value;
        }

        public string cadenaSQL()
        {
            return connectionString;
        }

        public SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaSQL());
            conexion.Open();
            return conexion;
        }

        public void CerrarConexion(SqlConnection conexion)
        {
            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
