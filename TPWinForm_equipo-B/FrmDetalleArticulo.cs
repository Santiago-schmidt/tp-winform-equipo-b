using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace TPWinForm_equipo_B
{
    public partial class FrmDetalleArticulo : Form
    {
        private Articulo articulo;
        private List<Imagen> imagenes;
        private int indiceImagen;
        public FrmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();

            this.articulo = articulo;
            indiceImagen = 0;

            CargarDatos();
            CargarImagenes();
        }
        private void CargarDatos()
        {
            lblCodigo.Text = articulo.Codigo ?? "";
            lblNombre.Text = articulo.Nombre ?? "";
            lblMarca.Text = articulo.Marca?.Descripcion ?? "";
            lblCategoria.Text = articulo.Categoria?.Descripcion ?? "";
            lblPrecio.Text = articulo.Precio.ToString("C");
            txtDescripcion.Text = articulo.Descripcion ?? "";
        }
        private void CargarImagenes()
        {
            ImagenRepositorio repositorio = new ImagenRepositorio();

            imagenes = repositorio.Listar()
                .Where(x => x.IdArticulo == articulo.Id)
                .ToList();

            if (imagenes.Count > 0)
            {
                indiceImagen = 0;
                MostrarImagen();
            }
            else
            {
                picImagen.Image = null;
                lblContadorImagenes.Text = "Sin imágenes";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
            }
        }
        private void MostrarImagen()
        {
            if (imagenes == null || imagenes.Count == 0)
                return;

            try
            {
                string url = imagenes[indiceImagen].ImagenUrl;

                using (WebClient cliente = new WebClient())
                {
                    byte[] datos = cliente.DownloadData(url);

                    using (MemoryStream memoria = new MemoryStream(datos))
                    {
                        using (Image imagen = Image.FromStream(memoria))
                        {
                            picImagen.Image = new Bitmap(imagen);
                        }
                    }
                }

                lblContadorImagenes.Text =
                    "Imagen " + (indiceImagen + 1) + " de " + imagenes.Count;

                btnAnterior.Enabled = indiceImagen > 0;
                btnSiguiente.Enabled = indiceImagen < imagenes.Count - 1;
            }
            catch
            {
                picImagen.Image = null;

                lblContadorImagenes.Text =
                    "No se pudo cargar la imagen";
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagen > 0)
            {
                indiceImagen--;
                MostrarImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceImagen < imagenes.Count - 1)
            {
                indiceImagen++;
                MostrarImagen();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}