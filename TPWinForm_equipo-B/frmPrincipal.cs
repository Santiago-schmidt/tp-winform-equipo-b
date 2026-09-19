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
        private bool ordenAscendente = true;
        private List<Imagen> listaImagenesActual;
        private int indiceImagenActual = 0;
        private Image imagenPorDefecto;
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
            this.ActiveControl = dgvArticulos;

            imagenPorDefecto = pbxArticulo.Image;



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            // Si no hay imágenes para el artículo seleccionado
            if (listaImagenesActual == null || listaImagenesActual.Count == 0)
            {
                pbxArticulo.Image = imagenPorDefecto;
                lblImagen.Text = "Sin Imágenes";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            // Si hay imágenes, intentamos cargar la actual
            try
            {
                pbxArticulo.Load(listaImagenesActual[indiceImagenActual].ImagenUrl);
            }
            catch (Exception)
            {
                // Si la URL devuelve error 404 o el enlace está roto, usamos la imagen por defecto
                pbxArticulo.Image = imagenPorDefecto;
            }

            // Actualizar el Label
            lblImagen.Text = $"Imagen {indiceImagenActual + 1}/{listaImagenesActual.Count}";

            // Desactivar botones si solo hay 1 imagen, activarlos si hay más de 1
            bool habilitarBotones = listaImagenesActual.Count > 1;
            btnAnterior.Enabled = habilitarBotones;
            btnSiguiente.Enabled = habilitarBotones;
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
    }

}
