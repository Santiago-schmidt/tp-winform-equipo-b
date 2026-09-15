using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_B
{
    internal sealed class ArticuloRepositorio : IRepositorioArticulo
    {
        private const string ConsultaBase =
            "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, " +
            "m.Id AS IdMarca, m.Descripcion AS DescripcionMarca, " +
            "c.Id AS IdCategoria, c.Descripcion AS DescripcionCategoria " +
            "FROM Articulos a " +
            "INNER JOIN Marcas m ON m.Id = a.IdMarca " +
            "INNER JOIN Categorias c ON c.Id = a.IdCategoria ";

        private Articulo LeerArticulo(SqlDataReader lector)
        {
            return new Articulo
            {
                Id = (int)lector["Id"],
                Codigo = (string)lector["Codigo"],
                Nombre = (string)lector["Nombre"],
                Descripcion = lector["Descripcion"] as string,
                Precio = (decimal)lector["Precio"],
                Marca = new Marca
                {
                    Id = (int)lector["IdMarca"],
                    Descripcion = (string)lector["DescripcionMarca"]
                },
                Categoria = new Categoria
                {
                    Id = (int)lector["IdCategoria"],
                    Descripcion = (string)lector["DescripcionCategoria"]
                }
            };
        }

        public List<Articulo> Listar()
        {
            List<Articulo> articulos = new List<Articulo>();

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = ConsultaBase + "WHERE a.Eliminado = 0";
                SqlCommand comando = new SqlCommand(consulta, conexion);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        articulos.Add(LeerArticulo(lector));
                    }
                }
            }

            return articulos;
        }

        public Articulo ObtenerPorId(int id)
        {
            Articulo articulo = null;

            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = ConsultaBase + "WHERE a.Id = @Id AND a.Eliminado = 0";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        articulo = LeerArticulo(lector);
                    }
                }
            }

            return articulo;
        }

        public void Agregar(Articulo articulo)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, Eliminado) " +
                                   "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria, 0)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
                comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                comando.Parameters.AddWithValue("@IdMarca", articulo.Marca.Id);
                comando.Parameters.AddWithValue("@IdCategoria", articulo.Categoria.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Articulo articulo)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Articulos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, " +
                                   "Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
                comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                comando.Parameters.AddWithValue("@IdMarca", articulo.Marca.Id);
                comando.Parameters.AddWithValue("@IdCategoria", articulo.Categoria.Id);
                comando.Parameters.AddWithValue("@Id", articulo.Id);

                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int id)
        {
            using (SqlConnection conexion = Conexion.ObtenerConexion())
            {
                string consulta = "UPDATE Articulos SET Eliminado = 1 WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}
