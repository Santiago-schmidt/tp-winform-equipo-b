using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal sealed class MarcaRepositorio : IRepositorioMarca
    {
        public List<Marca> Listar()
        {
            List<Marca> marcas = new List<Marca>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, Descripcion FROM Marcas";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        marcas.Add(new Marca
                        {
                            Id = (int)lector["Id"],
                            Descripcion = (string)lector["Descripcion"]
                        });
                    }
                }
            }

            return marcas;
        }

        public Marca ObtenerPorId(int id)
        {
            Marca marca = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, Descripcion FROM Marcas WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        marca = new Marca
                        {
                            Id = (int)lector["Id"],
                            Descripcion = (string)lector["Descripcion"]
                        };
                    }
                }
            }

            return marca;
        }

        public void Agregar(Marca marca)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "INSERT INTO Marcas (Descripcion) VALUES (@Descripcion)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Descripcion", marca.Descripcion);

                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Marca marca)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Marcas SET Descripcion = @Descripcion WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                comando.Parameters.AddWithValue("@Id", marca.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int id)
        {
            // La base real no tiene columna Eliminado, así que esto es una baja física.
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "DELETE FROM Marcas WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}
