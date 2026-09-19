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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.TextoValido(txtDescripcion.Text))
    {
                MessageBox.Show("La descripción no puede estar vacía.");
                return;
            }

            if (!Validaciones.LongitudValida(txtDescripcion.Text, 50))
            {
                MessageBox.Show("La descripción no puede superar los 50 caracteres.");
                return;
            }

            Marca nuevaMarca = new Marca();
            nuevaMarca.Descripcion = txtDescripcion.Text.Trim();

            MarcaRepositorio repositorio = new MarcaRepositorio();
            repositorio.Agregar(nuevaMarca);

            CargarMarcas();

            txtDescripcion.Clear();

            MessageBox.Show("Marca agregada correctamente.");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una marca.");
                return;
            }

            if (!Validaciones.TextoValido(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.");
                return;
            }

            if (!Validaciones.LongitudValida(txtDescripcion.Text, 50))
            {
                MessageBox.Show("La descripción no puede superar los 50 caracteres.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            seleccionada.Descripcion = txtDescripcion.Text.Trim();

            MarcaRepositorio repositorio = new MarcaRepositorio();
            repositorio.Modificar(seleccionada);

            CargarMarcas();

            MessageBox.Show("Marca modificada correctamente.");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una marca.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar la marca " + seleccionada.Descripcion + "?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            MarcaRepositorio repositorio = new MarcaRepositorio();
            repositorio.Eliminar(seleccionada.Id);

            CargarMarcas();

            txtDescripcion.Clear();

            MessageBox.Show("Marca eliminada correctamente.");
        }

        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow != null)
            {
                Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

                txtDescripcion.Text = seleccionada.Descripcion;
            }
        }
    }
}
