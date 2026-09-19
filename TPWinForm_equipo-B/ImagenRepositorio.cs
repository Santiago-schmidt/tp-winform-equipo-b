using System;
using System.Collections.Generic;

namespace TPWinForm_equipo_B
{
    public class ImagenRepositorio
    {
        public List<Imagen> Listar()
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM Imagenes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    imagenes.Add(aux);
                }

                return imagenes;
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

        public Imagen ObtenerPorId(int id)
        {
            Imagen imagen = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM Imagenes WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    imagen = new Imagen();
                    imagen.Id = (int)datos.Lector["Id"];
                    imagen.IdArticulo = (int)datos.Lector["IdArticulo"];
                    imagen.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                }

                return imagen;
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

        public void Agregar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", imagen.IdArticulo);
                datos.setearParametro("@ImagenUrl", (object)imagen.ImagenUrl ?? DBNull.Value);
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

        public void Modificar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Imagenes SET IdArticulo = @IdArticulo, ImagenUrl = @ImagenUrl WHERE Id = @Id");
                datos.setearParametro("@IdArticulo", imagen.IdArticulo);
                datos.setearParametro("@ImagenUrl", (object)imagen.ImagenUrl ?? DBNull.Value);
                datos.setearParametro("@Id", imagen.Id);
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

        public List<Imagen> ListarPorArticulo(int idArticulo)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM Imagenes WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    imagenes.Add(aux);
                }

                return imagenes;
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
                datos.setearConsulta("DELETE FROM Imagenes WHERE Id = @Id");
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
    }
}