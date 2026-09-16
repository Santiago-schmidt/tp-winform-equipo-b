using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal sealed class CategoriaRepositorio : IRepositorioCategoria
    {
        public List<Categoria> Listar()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, Descripcion FROM Categorias";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            Id = (int)lector["Id"],
                            Descripcion = (string)lector["Descripcion"]
                        });
                    }
                }
            }

            return categorias;
        }

        public Categoria ObtenerPorId(int id)
        {
            Categoria categoria = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, Descripcion FROM Categorias WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        categoria = new Categoria
                        {
                            Id = (int)lector["Id"],
                            Descripcion = (string)lector["Descripcion"]
                        };
                    }
                }
            }

            return categoria;
        }

        public void Agregar(Categoria categoria)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "INSERT INTO Categorias (Descripcion) VALUES (@Descripcion)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Categoria categoria)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Categorias SET Descripcion = @Descripcion WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                comando.Parameters.AddWithValue("@Id", categoria.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int id)
        {
            // La base real no tiene columna Eliminado, así que esto es una baja física.
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "DELETE FROM Categorias WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}
