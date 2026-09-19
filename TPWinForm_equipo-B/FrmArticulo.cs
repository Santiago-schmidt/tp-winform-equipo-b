using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_B
{
    public partial class FrmArticulo : Form
    {
        private Articulo articulo;

        public FrmArticulo()
        {
            InitializeComponent();
            CargarCombos();
        }

        public FrmArticulo(Articulo articulo) : this()
        {
            this.articulo = articulo;
            this.Text = "Editar Artículo";

            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();

            if (articulo.Marca != null)
                cmbMarca.SelectedValue = articulo.Marca.Id;

            if (articulo.Categoria != null)
                cmbCategoria.SelectedValue = articulo.Categoria.Id;

            CargarGaleria();
        }

        private void CargarGaleria()
        {
            if (articulo == null || articulo.Id == 0)
                return;

            ImagenRepositorio repo = new ImagenRepositorio();
            List<Imagen> imagenes = repo.Listar().Where(i => i.IdArticulo == articulo.Id).ToList();

            foreach (Imagen imagen in imagenes)
            {
                PictureBox miniatura = new PictureBox();
                miniatura.Width = 80;
                miniatura.Height = 80;
                miniatura.SizeMode = PictureBoxSizeMode.Zoom;
                miniatura.Margin = new Padding(5);

                try
                {
                    using (WebClient cliente = new WebClient())
                    {
                        byte[] datos = cliente.DownloadData(imagen.ImagenUrl);

                        using (MemoryStream memoria = new MemoryStream(datos))
                        {
                            miniatura.Image = new Bitmap(Image.FromStream(memoria));
                        }
                    }
                }
                catch
                {
                    miniatura.Image = null;
                }

                flpGaleria.Controls.Add(miniatura);
            }
        }

        private void CargarCombos()
        {
            MarcaRepositorio marcaRepo = new MarcaRepositorio();
            cmbMarca.DataSource = marcaRepo.Listar();
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Id";

            CategoriaRepositorio categoriaRepo = new CategoriaRepositorio();
            cmbCategoria.DataSource = categoriaRepo.Listar();
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArticuloRepositorio repo = new ArticuloRepositorio();

            if (articulo == null)
            {
                articulo = new Articulo();
            }

            articulo.Codigo = txtCodigo.Text;
            articulo.Nombre = txtNombre.Text;
            articulo.Descripcion = txtDescripcion.Text;
            articulo.Precio = decimal.Parse(txtPrecio.Text);
            articulo.Marca = (Marca)cmbMarca.SelectedItem;
            articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;

            if (articulo.Id == 0)
            {
                repo.Agregar(articulo);
            }
            else
            {
                repo.Modificar(articulo);
            }

            this.Close();
        }
    }
}
