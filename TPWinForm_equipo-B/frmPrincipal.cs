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



        }

        private void button5_Click(object sender, EventArgs e)
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
    }
}
