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
        private bool cargandoCombos;

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
            flpGaleria.Controls.Clear();

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
                miniatura.Tag = imagen;
                miniatura.DoubleClick += Miniatura_DoubleClick;

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

        private void Miniatura_DoubleClick(object sender, EventArgs e)
        {
            PictureBox miniatura = (PictureBox)sender;
            Imagen imagen = (Imagen)miniatura.Tag;

            string nuevaUrl = Microsoft.VisualBasic.Interaction.InputBox("Nueva URL de la imagen:", "Modificar imagen", imagen.ImagenUrl);

            if (nuevaUrl == "")
                return;

            imagen.ImagenUrl = nuevaUrl;

            ImagenRepositorio repo = new ImagenRepositorio();
            repo.Modificar(imagen);

            CargarGaleria();
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (articulo == null || articulo.Id == 0)
            {
                MessageBox.Show("Primero guardá el artículo para poder agregarle imágenes.");
                return;
            }

            if (txtImagenUrl.Text.Trim() == "")
                return;

            Imagen nueva = new Imagen();
            nueva.IdArticulo = articulo.Id;
            nueva.ImagenUrl = txtImagenUrl.Text.Trim();

            ImagenRepositorio repo = new ImagenRepositorio();
            repo.Agregar(nueva);

            txtImagenUrl.Text = "";
            CargarGaleria();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarCombos()
        {
            cargandoCombos = true;

            MarcaRepositorio marcaRepo = new MarcaRepositorio();
            List<Marca> marcas = marcaRepo.Listar();
            marcas.Add(new Marca { Id = -1, Descripcion = "+ Nueva marca..." });
            cmbMarca.DataSource = marcas;
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Id";

            CategoriaRepositorio categoriaRepo = new CategoriaRepositorio();
            List<Categoria> categorias = categoriaRepo.Listar();
            categorias.Add(new Categoria { Id = -1, Descripcion = "+ Nueva categoría..." });
            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";

            cargandoCombos = false;
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoCombos)
                return;

            Marca seleccionada = cmbMarca.SelectedItem as Marca;
            if (seleccionada == null || seleccionada.Id != -1)
                return;

            string nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre de la marca nueva:", "Nueva marca", "");

            if (!Validaciones.TextoValido(nombre) || !Validaciones.LongitudValida(nombre, 50))
            {
                if (nombre != "")
                    MessageBox.Show("La marca tiene que tener un nombre de hasta 50 caracteres.");

                CargarCombos();
                return;
            }

            Marca nueva = new Marca();
            nueva.Descripcion = nombre.Trim();

            MarcaRepositorio repo = new MarcaRepositorio();
            repo.Agregar(nueva);

            CargarCombos();

            List<Marca> lista = (List<Marca>)cmbMarca.DataSource;
            Marca creada = lista.FirstOrDefault(m => m.Descripcion == nueva.Descripcion);
            if (creada != null)
                cmbMarca.SelectedValue = creada.Id;
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoCombos)
                return;

            Categoria seleccionada = cmbCategoria.SelectedItem as Categoria;
            if (seleccionada == null || seleccionada.Id != -1)
                return;

            string nombre = Microsoft.VisualBasic.Interaction.InputBox("Nombre de la categoría nueva:", "Nueva categoría", "");

            if (!Validaciones.TextoValido(nombre) || !Validaciones.LongitudValida(nombre, 50))
            {
                if (nombre != "")
                    MessageBox.Show("La categoría tiene que tener un nombre de hasta 50 caracteres.");

                CargarCombos();
                return;
            }

            Categoria nueva = new Categoria();
            nueva.Descripcion = nombre.Trim();

            CategoriaRepositorio repo = new CategoriaRepositorio();
            repo.Agregar(nueva);

            CargarCombos();

            List<Categoria> lista = (List<Categoria>)cmbCategoria.DataSource;
            Categoria creada = lista.FirstOrDefault(c => c.Descripcion == nueva.Descripcion);
            if (creada != null)
                cmbCategoria.SelectedValue = creada.Id;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.TextoValido(txtCodigo.Text))
            {
                MessageBox.Show("El código es obligatorio.");
                return;
            }

            if (!Validaciones.TextoValido(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio tiene que ser un número.");
                return;
            }

            if (!Validaciones.PrecioValido(precio))
            {
                MessageBox.Show("El precio tiene que ser mayor a cero.");
                return;
            }

            Marca marcaSeleccionada = cmbMarca.SelectedItem as Marca;
            if (marcaSeleccionada == null || marcaSeleccionada.Id == -1)
            {
                MessageBox.Show("Elegí una marca.");
                return;
            }

            Categoria categoriaSeleccionada = cmbCategoria.SelectedItem as Categoria;
            if (categoriaSeleccionada == null || categoriaSeleccionada.Id == -1)
            {
                MessageBox.Show("Elegí una categoría.");
                return;
            }

            if (articulo == null)
            {
                articulo = new Articulo();
            }

            articulo.Codigo = txtCodigo.Text.Trim();
            articulo.Nombre = txtNombre.Text.Trim();
            articulo.Descripcion = txtDescripcion.Text;
            articulo.Precio = precio;
            articulo.Marca = marcaSeleccionada;
            articulo.Categoria = categoriaSeleccionada;

            ArticuloRepositorio repo = new ArticuloRepositorio();

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
