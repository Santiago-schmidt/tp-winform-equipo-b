using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TPWinForm_equipo_B
{
    public partial class frmPrincipal : Form
    {
        private Dictionary<string, Image> cacheImagenes = new Dictionary<string, Image>();
        private bool ordenAscendente = true;
        private List<Imagen> listaImagenesActual;
        private int indiceImagenActual = 0;
        private CursorFlecha modificadorCursorCampo;
        private CursorFlecha modificadorCursorCriterio;
        
        private const uint GW_CHILD = 5;

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            

            // 1. Cargar los datos
            ArticuloRepositorio repo = new ArticuloRepositorio();
            dgvArticulos.DataSource = repo.Listar();

            // 2. Ocultar las columnas de forma segura
            if (dgvArticulos.Columns["Id"] != null)
                dgvArticulos.Columns["Id"].Visible = false;

            if (dgvArticulos.Columns["Descripcion"] != null)
                dgvArticulos.Columns["Descripcion"].Visible = false;

            if (dgvArticulos.Columns["Imagenes"] != null)
                dgvArticulos.Columns["Imagenes"].Visible = false;

            if (dgvArticulos.Columns["MarcaDescripcion"] != null)
                dgvArticulos.Columns["MarcaDescripcion"].Visible = false;

            if (dgvArticulos.Columns["CategoriaDescripcion"] != null)
                dgvArticulos.Columns["CategoriaDescripcion"].Visible = false;

            
            cbCampo.Items.Add("Precio");
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Descripción");

            cbCampo.Text = "Campo...";
            cbCampo.ForeColor = Color.Gray;

            cbCriterio.Text = "Criterio...";
            cbCriterio.ForeColor = Color.Gray;

            IntPtr editCampo = GetWindow(cbCampo.Handle, GW_CHILD);
            IntPtr editCriterio = GetWindow(cbCriterio.Handle, GW_CHILD);

            // Asignar el comportamiento de cursor modificado
            modificadorCursorCampo = new CursorFlecha(editCampo);
            modificadorCursorCriterio = new CursorFlecha(editCriterio);

            // Quitar el foco inicial a los ComboBox
            this.ActiveControl = btnVerDetalle;
            MostrarImagenInicial();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCampo.SelectedItem == null)
                return;

            string opcion = cbCampo.SelectedItem.ToString();
            cbCriterio.Items.Clear();

            if (opcion == "Precio")
            {
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else // Para "Nombre" o "Descripción"
            {
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina con");
                cbCriterio.Items.Add("Contiene");
            }
        }

        private void cbCampo_Enter(object sender, EventArgs e)
        {
            if (cbCampo.Text == "Campo...")
            {
                cbCampo.Text = "";
                cbCampo.ForeColor = Color.Black;
            }
            cbCampo.DroppedDown = true;

            IntPtr editHandle = GetWindow(cbCampo.Handle, GW_CHILD);
            HideCaret(editHandle);
        }

        private void cbCriterio_Enter(object sender, EventArgs e)
        {
            if (cbCriterio.Text == "Criterio...")
            {
                cbCriterio.Text = "";
                cbCriterio.ForeColor = Color.Black;
            }
            cbCriterio.DroppedDown = true;

            IntPtr editHandle = GetWindow(cbCriterio.Handle, GW_CHILD);
            HideCaret(editHandle);
        }

        private void cbCampo_Leave(object sender, EventArgs e)
        {
            if (cbCampo.SelectedIndex == -1) // Si no seleccionó ningún ítem real
            {
                cbCampo.Text = "Campo...";
                cbCampo.ForeColor = Color.Gray;
            }
        }

        private void cbCriterio_Leave(object sender, EventArgs e)
        {
            if (cbCriterio.SelectedIndex == -1)
            {
                cbCriterio.Text = "Criterio...";
                cbCriterio.ForeColor = Color.Gray;
            }
        }

        private void cbCampo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Cancela la pulsación de la tecla
        }

        private void cbCriterio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbCampo_Click(object sender, EventArgs e)
        {
            cbCampo.DroppedDown = true;
        }

        private void cbCriterio_Click(object sender, EventArgs e)
        {
            cbCriterio.DroppedDown = true;
        }

        private void cbCampo_MouseDown(object sender, MouseEventArgs e)
        {
            IntPtr editHandle = GetWindow(cbCampo.Handle, GW_CHILD);
            HideCaret(editHandle);
        }

        private void cbCriterio_MouseDown(object sender, MouseEventArgs e)
        {
            IntPtr editHandle = GetWindow(cbCriterio.Handle, GW_CHILD);
            HideCaret(editHandle);
        }
        private class CursorFlecha : NativeWindow
    {
        public CursorFlecha(IntPtr handle)
        {
            // Se engancha al control interno de texto
            AssignHandle(handle);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SETCURSOR = 0x0020;
            if (m.Msg == WM_SETCURSOR)
            {
                // Fuerza el cursor predeterminado (flecha)
                Cursor.Current = Cursors.Default;
                // Indica a Windows que el evento del cursor ya fue procesado
                m.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref m);
        }
    }

        private void tbFiltro_Enter(object sender, EventArgs e)
        {
            if (tbFiltro.Text == " Filtro...")
            {
                tbFiltro.Text = "";
                tbFiltro.ForeColor = Color.Black;
            }
        }

        private void tbFiltro_Leave(object sender, EventArgs e)
        {
            // string.IsNullOrWhiteSpace valida si el texto está vacío o si el usuario solo tecleó espacios
            if (string.IsNullOrWhiteSpace(tbFiltro.Text))
            {
                tbFiltro.Text = " Filtro...";
                tbFiltro.ForeColor = Color.Gray;
            }
        }
        private void OcultarColumnas()
        {
            if (dgvArticulos.Columns["Id"] != null) dgvArticulos.Columns["Id"].Visible = false;
            if (dgvArticulos.Columns["Descripcion"] != null) dgvArticulos.Columns["Descripcion"].Visible = false;
            if (dgvArticulos.Columns["Imagenes"] != null) dgvArticulos.Columns["Imagenes"].Visible = false;
            if (dgvArticulos.Columns["MarcaDescripcion"] != null) dgvArticulos.Columns["MarcaDescripcion"].Visible = false;
            if (dgvArticulos.Columns["CategoriaDescripcion"] != null) dgvArticulos.Columns["CategoriaDescripcion"].Visible = false;
        }

        private void dgvArticulos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)dgvArticulos.DataSource;
            if (lista == null || lista.Count == 0) return;

            string nombreColumna = dgvArticulos.Columns[e.ColumnIndex].Name;

            switch (nombreColumna)
            {
                case "Codigo":
                    lista.Sort((x, y) => ordenAscendente ? string.Compare(x.Codigo, y.Codigo) : string.Compare(y.Codigo, x.Codigo));
                    break;
                case "Nombre":
                    lista.Sort((x, y) => ordenAscendente ? string.Compare(x.Nombre, y.Nombre) : string.Compare(y.Nombre, x.Nombre));
                    break;
                case "Precio":
                    lista.Sort((x, y) => ordenAscendente ? x.Precio.CompareTo(y.Precio) : y.Precio.CompareTo(x.Precio));
                    break;
                case "Marca":
                    // string.Compare maneja automáticamente los valores nulos para evitar colapsos
                    lista.Sort((x, y) => ordenAscendente ? string.Compare(x.Marca?.Descripcion, y.Marca?.Descripcion) : string.Compare(y.Marca?.Descripcion, x.Marca?.Descripcion));
                    break;
                case "Categoria":
                    lista.Sort((x, y) => ordenAscendente ? string.Compare(x.Categoria?.Descripcion, y.Categoria?.Descripcion) : string.Compare(y.Categoria?.Descripcion, x.Categoria?.Descripcion));
                    break;
            }

            // Invertir la bandera para el próximo clic
            ordenAscendente = !ordenAscendente;

            // Refrescar la grilla
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = lista;
            OcultarColumnas();

            // Limpiar el símbolo de ordenamiento de todas las columnas
            foreach (DataGridViewColumn columna in dgvArticulos.Columns)
            {
                columna.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            // Determinar y aplicar el símbolo a la columna actual
            // Como la variable ordenAscendente se invirtió previamente en este método, 
            // se evalúa a la inversa para reflejar el estado actual.
            dgvArticulos.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                !ordenAscendente ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void dgvArticulos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                dgvArticulos.Cursor = Cursors.Hand;
            }
        }

        private void dgvArticulos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                dgvArticulos.Cursor = Cursors.Default;
            }
        }

        private void dgvArticulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvArticulos.Columns[e.ColumnIndex].Name == "Precio" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal precio))
                {
                     decimal precioTruncado = Math.Truncate(precio * 100) / 100;

                   e.Value = precioTruncado.ToString("C2", new System.Globalization.CultureInfo("es-AR"));
                    e.FormattingApplied = true;
                }
            }
        }
        private void ActualizarInterfazImagen()
        {
            if (listaImagenesActual == null || listaImagenesActual.Count == 0)
            {
                pbxArticulo.Image = Properties.Resources.Imagen_no_disponible;
                lblImagen.Text = "Sin Imágenes";
                lblImagen.Visible = false;
                btnAnterior.Visible = false;
                btnSiguiente.Visible = false;
                return;
            }

            string urlActual = listaImagenesActual[indiceImagenActual].ImagenUrl;
            pbxArticulo.Image = ObtenerImagenOptimizada(urlActual);

            // Mostrar los controles SOLO si hay más de una imagen
            bool hayNavegacion = listaImagenesActual.Count > 1;

            lblImagen.Visible = hayNavegacion;
            btnAnterior.Visible = hayNavegacion;
            btnSiguiente.Visible = hayNavegacion;

            if (hayNavegacion)
            {
                lblImagen.Text = $"Imagen {indiceImagenActual + 1}/{listaImagenesActual.Count}";
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                ImagenRepositorio repoImg = new ImagenRepositorio();
                // Debes asegurarte de tener un método en tu repositorio que busque por IdArticulo
                listaImagenesActual = repoImg.ListarPorArticulo(seleccionado.Id);

                indiceImagenActual = 0; // Reiniciar el índice al seleccionar un artículo nuevo
                ActualizarInterfazImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (listaImagenesActual != null && listaImagenesActual.Count > 0)
            {
                indiceImagenActual++;

                // Si superamos la última imagen, volvemos a la primera (índice 0)
                if (indiceImagenActual >= listaImagenesActual.Count)
                {
                    indiceImagenActual = 0;
                }

                ActualizarInterfazImagen();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (listaImagenesActual != null && listaImagenesActual.Count > 0)
            {
                indiceImagenActual--;

                // Si retrocedemos antes de la primera, vamos a la última
                if (indiceImagenActual < 0)
                {
                    indiceImagenActual = listaImagenesActual.Count - 1;
                }

                ActualizarInterfazImagen();
            }
        }
        private Image ObtenerImagenOptimizada(string url)
        {
            // Si la imagen ya fue descargada, se devuelve desde la memoria
            if (cacheImagenes.ContainsKey(url))
            {
                return cacheImagenes[url];
            }

            try
            {
                // Descarga de imagen utilizando la misma estructura que FrmArticulo
                using (System.Net.WebClient cliente = new System.Net.WebClient())
                {
                    byte[] datos = cliente.DownloadData(url);
                    using (System.IO.MemoryStream memoria = new System.IO.MemoryStream(datos))
                    {
                        Image imgOriginal = Image.FromStream(memoria);

                        // Lógica de redimensionamiento para proteger el caché
                        int tamañoMaximo = 400;
                        int nuevoAncho = imgOriginal.Width;
                        int nuevoAlto = imgOriginal.Height;

                        if (imgOriginal.Width > tamañoMaximo || imgOriginal.Height > tamañoMaximo)
                        {
                            float proporcion = Math.Min((float)tamañoMaximo / imgOriginal.Width, (float)tamañoMaximo / imgOriginal.Height);
                            nuevoAncho = (int)(imgOriginal.Width * proporcion);
                            nuevoAlto = (int)(imgOriginal.Height * proporcion);
                        }

                        Bitmap imgReducida = new Bitmap(imgOriginal, nuevoAncho, nuevoAlto);
                        cacheImagenes.Add(url, imgReducida);

                        return imgReducida;
                    }
                }
            }
            catch
            {
                // Retornar imagen de error si la URL está rota o hay fallo de red
                return Properties.Resources.Imagen_no_disponible;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada en la grilla
            if (dgvArticulos.CurrentRow != null)
            {
                // Extraer el objeto Articulo de la fila actual
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                // Instanciar el formulario de modificación pasando el artículo por parámetro
                FrmArticulo frmModificar = new FrmArticulo(seleccionado);

                // Abrir el formulario de manera modal (restringe el uso de la ventana principal hasta que se cierre)
                frmModificar.ShowDialog();

                // Una vez que el formulario se cierra, recargar la grilla para reflejar los cambios
                ArticuloRepositorio repo = new ArticuloRepositorio();
                dgvArticulos.DataSource = repo.Listar();
                OcultarColumnas();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo de la lista para modificar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada
            if (dgvArticulos.CurrentRow != null)
            {
                // Obtener el artículo seleccionado
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                // Instanciar y mostrar el formulario de detalle de forma modal
                FrmDetalleArticulo frmDetalle = new FrmDetalleArticulo(seleccionado);
                frmDetalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo de la lista para ver el detalle.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar que haya un artículo seleccionado
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                // Instanciar el formulario de confirmación pasándole el artículo
                frmConfirmaEliminacion frmConfirmacion = new frmConfirmaEliminacion(seleccionado);

                // Si el resultado es OK (se eliminó o se eligió modificar y se guardó), refrescar la grilla
                if (frmConfirmacion.ShowDialog() == DialogResult.OK)
                {
                    ArticuloRepositorio repo = new ArticuloRepositorio();
                    dgvArticulos.DataSource = repo.Listar();
                    OcultarColumnas();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo de la lista para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar que los ComboBox tengan una selección real y no el texto gris
                if (cbCampo.SelectedIndex < 0 || cbCampo.Text == "Campo...")
                {
                    MessageBox.Show("Por favor, seleccione un campo para realizar la búsqueda.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbCriterio.SelectedIndex < 0 || cbCriterio.Text == "Criterio...")
                {
                    MessageBox.Show("Por favor, seleccione un criterio para realizar la búsqueda.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();
                string filtro = tbFiltro.Text.Trim();

                // 2. Validar que el TextBox tenga contenido válido
                if (string.IsNullOrWhiteSpace(filtro) || filtro == "Filtro...")
                {
                    MessageBox.Show("Por favor, ingrese un valor para filtrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Validaciones específicas según el campo seleccionado
                if (campo == "Precio")
                {
                    // Reemplazar coma por punto para evitar errores de sintaxis en la consulta SQL
                    filtro = filtro.Replace(",", ".");

                    // Validar que el texto sea un número
                    if (!decimal.TryParse(filtro, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                    {
                        MessageBox.Show("Para filtrar por precio, debe ingresar únicamente valores numéricos.", "Validación de tipo de dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 4. Ejecutar la consulta en la base de datos
                ArticuloRepositorio repo = new ArticuloRepositorio();
                dgvArticulos.DataSource = repo.Filtrar(campo, criterio, filtro);

                // 5. Restablecer la vista de la grilla
                OcultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al intentar filtrar los datos: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarImagenInicial()
        {
            pbxArticulo.Image = Properties.Resources.Phoenix1;
            lblImagen.Text = "";
            lblImagen.Visible = false;
            listaImagenesActual = null;
            indiceImagenActual = 0;
            btnAnterior.Visible = false;
            btnSiguiente.Visible = false;
        }
        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            ArticuloRepositorio repo = new ArticuloRepositorio();
            dgvArticulos.DataSource = repo.Listar();
            OcultarColumnas();

            // Restaurar los placeholders
            cbCampo.SelectedIndex = -1;
            cbCampo.Text = "Campo...";
            cbCampo.ForeColor = Color.Gray;

            cbCriterio.Items.Clear();
            cbCriterio.Text = "Criterio...";
            cbCriterio.ForeColor = Color.Gray;

            tbFiltro.Text = "Filtro...";
            tbFiltro.ForeColor = Color.Gray;
            this.ActiveControl = btnVerDetalle;
            MostrarImagenInicial();
        }

        private void deMarcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarcasYCategorias frmMarcas = new frmMarcasYCategorias(TipoAdministracion.Marcas);
            frmMarcas.ShowDialog();
        }

        private void btnAgregarArticuloNuevo_Click(object sender, EventArgs e)
        {
            // Instanciar el formulario en blanco
            FrmArticulo frmAlta = new FrmArticulo();

            // Abrir el formulario bloqueando la ventana principal
            frmAlta.ShowDialog();

            // Recargar la grilla para reflejar el nuevo ingreso
            ArticuloRepositorio repo = new ArticuloRepositorio();
            dgvArticulos.DataSource = repo.Listar();

            // Volver a ocultar las columnas de sistema
            OcultarColumnas();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMarcasYCategorias frmCategorias = new frmMarcasYCategorias(TipoAdministracion.Categorias);
            frmCategorias.ShowDialog();
        }

        private void dgvArticulos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Limpia la selección automáticamente cada vez que la grilla recarga sus datos
            dgvArticulos.CurrentCell = null;
            dgvArticulos.ClearSelection();
        }
    }

}
