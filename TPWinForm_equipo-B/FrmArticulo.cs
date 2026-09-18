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
    public partial class FrmArticulo : Form
    {
        public FrmArticulo()
        {
            InitializeComponent();
            CargarCombos();
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
    }
}
