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
                string consulta = "SELECT Id, IdArticulo, ImagenUrl FROM Imagenes";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        imagenes.Add(new Imagen
                        {
                            Id = (int)lector["Id"],
                            IdArticulo = (int)lector["IdArticulo"],
                            ImagenUrl = (string)lector["ImagenUrl"]
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
                string consulta = "SELECT Id, IdArticulo, ImagenUrl FROM Imagenes WHERE Id = @Id";
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
                            ImagenUrl = (string)lector["ImagenUrl"]
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
                string consulta = "INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdArticulo", imagen.IdArticulo);
                comando.Parameters.AddWithValue("@ImagenUrl", imagen.ImagenUrl);

                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Imagen imagen)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Imagenes SET IdArticulo = @IdArticulo, ImagenUrl = @ImagenUrl WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@IdArticulo", imagen.IdArticulo);
                comando.Parameters.AddWithValue("@ImagenUrl", imagen.ImagenUrl);
                comando.Parameters.AddWithValue("@Id", imagen.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int id)
        {
            // La base real no tiene columna Eliminado, así que esto es una baja física.
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "DELETE FROM Imagenes WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}
