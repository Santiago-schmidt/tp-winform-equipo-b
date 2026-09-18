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

            // 2. Ocultar las columnas que no deben verse
            OcultarColumna("Id");
            OcultarColumna("Descripcion");
            OcultarColumna("Imagenes");
            OcultarColumna("Marca");
            OcultarColumna("Categoria");

            if (dgvArticulos.Columns.Contains("MarcaDescripcion"))
            {
                dgvArticulos.Columns["MarcaDescripcion"].HeaderText = "Marca";
            }

            if (dgvArticulos.Columns.Contains("CategoriaDescripcion"))
            {
                dgvArticulos.Columns["CategoriaDescripcion"].HeaderText = "Categoría";
            }
        }

        private void OcultarColumna(string nombreColumna)
        {
            if (dgvArticulos.Columns.Contains(nombreColumna))
            {
                dgvArticulos.Columns[nombreColumna].Visible = false;
            }
        }
    }
}
