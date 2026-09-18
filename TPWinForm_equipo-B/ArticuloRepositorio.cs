using System;
using System.Collections.Generic;

namespace TPWinForm_equipo_B
{
    public class ArticuloRepositorio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, m.Id AS IdMarca, m.Descripcion AS DescripcionMarca, c.Id AS IdCategoria, c.Descripcion AS DescripcionCategoria FROM Articulos a LEFT JOIN Marcas m ON m.Id = a.IdMarca LEFT JOIN Categorias c ON c.Id = a.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = datos.Lector["Codigo"] is DBNull ? string.Empty : (string)datos.Lector["Codigo"];
                    aux.Nombre = datos.Lector["Nombre"] is DBNull ? string.Empty : (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Precio = datos.Lector["Precio"] is DBNull ? 0 : (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (string)datos.Lector["DescripcionMarca"];
                    }

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (string)datos.Lector["DescripcionCategoria"];
                    }

                    articulos.Add(aux);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Articulo ObtenerPorId(int id)
        {
            Articulo articulo = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, m.Id AS IdMarca, m.Descripcion AS DescripcionMarca, c.Id AS IdCategoria, c.Descripcion AS DescripcionCategoria FROM Articulos a LEFT JOIN Marcas m ON m.Id = a.IdMarca LEFT JOIN Categorias c ON c.Id = a.IdCategoria WHERE a.Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = datos.Lector["Codigo"] is DBNull ? string.Empty : (string)datos.Lector["Codigo"];
                    articulo.Nombre = datos.Lector["Nombre"] is DBNull ? string.Empty : (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        articulo.Descripcion = (string)datos.Lector["Descripcion"];

                    articulo.Precio = datos.Lector["Precio"] is DBNull ? 0 : (decimal)datos.Lector["Precio"];

                    articulo.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                        articulo.Marca.Descripcion = (string)datos.Lector["DescripcionMarca"];
                    }

                    articulo.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        articulo.Categoria.Descripcion = (string)datos.Lector["DescripcionCategoria"];
                    }
                }

                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria)");
                datos.setearParametro("@Codigo", (object)articulo.Codigo ?? DBNull.Value);
                datos.setearParametro("@Nombre", (object)articulo.Nombre ?? DBNull.Value);
                datos.setearParametro("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca != null && articulo.Marca.Id > 0 ? (object)articulo.Marca.Id : DBNull.Value);
                datos.setearParametro("@IdCategoria", articulo.Categoria != null && articulo.Categoria.Id > 0 ? (object)articulo.Categoria.Id : DBNull.Value);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria WHERE Id = @Id");
                datos.setearParametro("@Codigo", (object)articulo.Codigo ?? DBNull.Value);
                datos.setearParametro("@Nombre", (object)articulo.Nombre ?? DBNull.Value);
                datos.setearParametro("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca != null && articulo.Marca.Id > 0 ? (object)articulo.Marca.Id : DBNull.Value);
                datos.setearParametro("@IdCategoria", articulo.Categoria != null && articulo.Categoria.Id > 0 ? (object)articulo.Categoria.Id : DBNull.Value);
                datos.setearParametro("@Id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Articulos WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, a.Precio, m.Id AS IdMarca, m.Descripcion AS DescripcionMarca, c.Id AS IdCategoria, c.Descripcion AS DescripcionCategoria FROM Articulos a LEFT JOIN Marcas m ON m.Id = a.IdMarca LEFT JOIN Categorias c ON c.Id = a.IdCategoria WHERE ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "a.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "a.Precio < " + filtro;
                            break;
                        default:
                            consulta += "a.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Nombre LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "a.Nombre LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Nombre LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    // Asumimos que la última opción de búsqueda es "Descripción"
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "a.Descripcion LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "a.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "a.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = datos.Lector["Codigo"] is DBNull ? string.Empty : (string)datos.Lector["Codigo"];
                    aux.Nombre = datos.Lector["Nombre"] is DBNull ? string.Empty : (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Precio = datos.Lector["Precio"] is DBNull ? 0 : (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (string)datos.Lector["DescripcionMarca"];
                    }

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (string)datos.Lector["DescripcionCategoria"];
                    }

                    articulos.Add(aux);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}