using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal sealed class ImagenRepositorio : IRepositorioImagen
    {
        public List<Imagen> Listar()
        {
            List<Imagen> imagenes = new List<Imagen>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, IdArticulo, Url FROM Imagenes WHERE Eliminado = 0";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        imagenes.Add(new Imagen
                        {
                            Id = (int)lector["Id"],
                            IdArticulo = (int)lector["IdArticulo"],
                            ImagenUrl = (string)lector["Url"]
                        });
                    }
                }
            }

            return imagenes;
        }

        public Imagen ObtenerPorId(int id)
        {
            Imagen imagen = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "SELECT Id, IdArticulo, Url FROM Imagenes WHERE Id = @Id AND Eliminado = 0";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        imagen = new Imagen
                        {
                            Id = (int)lector["Id"],
                            IdArticulo = (int)lector["IdArticulo"],
                            ImagenUrl = (string)lector["Url"]
                        };
                    }
                }
            }

            return imagen;
        }

        public void Agregar(Imagen imagen)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "INSERT INTO Imagenes (IdArticulo, Url, EsPrincipal, Eliminado) VALUES (@IdArticulo, @Url, 0, 0)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdArticulo", imagen.IdArticulo);
                comando.Parameters.AddWithValue("@Url", imagen.ImagenUrl);

                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Imagen imagen)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Imagenes SET IdArticulo = @IdArticulo, Url = @Url WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdArticulo", imagen.IdArticulo);
                comando.Parameters.AddWithValue("@Url", imagen.ImagenUrl);
                comando.Parameters.AddWithValue("@Id", imagen.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int id)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Imagenes SET Eliminado = 1 WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}
