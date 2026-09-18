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
    public partial class FrmMarcas : Form
    {
        private List<Marca> listaMarcas;
        public FrmMarcas()
        {
            InitializeComponent();
        }
        private void FrmMarcas_Load(object sender, EventArgs e)
        {
            CargarMarcas();
        }
        private void CargarMarcas()
        {
            MarcaRepositorio repositorio = new MarcaRepositorio();
            listaMarcas = repositorio.Listar();
            dgvMarcas.DataSource = listaMarcas;

            dgvMarcas.ReadOnly = true;
            dgvMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMarcas.MultiSelect = false;
            dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
