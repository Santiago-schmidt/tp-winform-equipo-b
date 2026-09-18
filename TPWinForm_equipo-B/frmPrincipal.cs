using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_B
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            // Establecer el tamaño exacto del formulario
            this.Size = new Size(1856, 812);

            // Centrar la ventana en la pantalla para evitar que los bordes queden fuera del área visible
            this.StartPosition = FormStartPosition.CenterScreen;

            // Bloquear el redimensionamiento mediante el cursor
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Deshabilitar el botón de maximizar en la esquina superior derecha
            this.MaximizeBox = false;
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

            // Ocultar la columna en blanco de la izquierda
            dgvArticulos.RowHeadersVisible = false;

            // Desactivar los temas predeterminados de Windows para habilitar colores personalizados en el encabezado
            dgvArticulos.EnableHeadersVisualStyles = false;

            // Configuración general del control
            dgvArticulos.BackgroundColor = Color.White;
            dgvArticulos.BorderStyle = BorderStyle.None;
            dgvArticulos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Solo líneas divisorias horizontales

            // Configuración visual de los encabezados de columna
            dgvArticulos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvArticulos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 66, 91); // Color azul oscuro formal
            dgvArticulos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvArticulos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvArticulos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvArticulos.ColumnHeadersHeight = 30;

            // Configuración visual de las celdas de datos
            dgvArticulos.DefaultCellStyle.BackColor = Color.White;
            dgvArticulos.DefaultCellStyle.ForeColor = Color.Black;
            dgvArticulos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215); // Color azul estándar de selección
            dgvArticulos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvArticulos.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Configuración de comportamiento y tamaño
            dgvArticulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Expande las columnas para ocupar todo el ancho disponible
            dgvArticulos.AllowUserToResizeRows = false;
        }
    }
}
