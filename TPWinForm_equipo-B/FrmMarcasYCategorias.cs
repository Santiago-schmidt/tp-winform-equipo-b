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
    // Enumerador para definir el comportamiento del formulario
    public enum TipoAdministracion
    {
        Marcas,
        Categorias
    }

    public partial class frmMarcasYCategorias : Form
    {
        private TipoAdministracion tipoActual;

        // El constructor ahora lleva el nuevo nombre del formulario
        public frmMarcasYCategorias(TipoAdministracion tipo)
        {
            InitializeComponent();
            this.tipoActual = tipo;
        }

        private void frmMarcasYCategorias_Load(object sender, EventArgs e)
        {
            // Adaptar el título de la ventana
            if (tipoActual == TipoAdministracion.Marcas)
                this.Text = "Administrar Marcas";
            else
                this.Text = "Administrar Categorías";

            CargarDatos();
        }

        private void CargarDatos()
        {
            // Cargar el repositorio correspondiente al origen de datos
            if (tipoActual == TipoAdministracion.Marcas)
            {
                MarcaRepositorio repositorio = new MarcaRepositorio();
                dgvMarcas.DataSource = repositorio.Listar();
            }
            else
            {
                CategoriaRepositorio repositorio = new CategoriaRepositorio();
                dgvMarcas.DataSource = repositorio.Listar();
            }

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
                MessageBox.Show("La descripción no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.LongitudValida(txtDescripcion.Text, 50))
            {
                MessageBox.Show("La descripción no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tipoActual == TipoAdministracion.Marcas)
            {
                Marca nuevaMarca = new Marca();
                nuevaMarca.Descripcion = txtDescripcion.Text.Trim();
                MarcaRepositorio repositorio = new MarcaRepositorio();
                repositorio.Agregar(nuevaMarca);
            }
            else
            {
                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.Descripcion = txtDescripcion.Text.Trim();
                CategoriaRepositorio repositorio = new CategoriaRepositorio();
                repositorio.Agregar(nuevaCategoria);
            }

            CargarDatos();
            txtDescripcion.Clear();
            MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.TextoValido(txtDescripcion.Text))
            {
                MessageBox.Show("La descripción no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Validaciones.LongitudValida(txtDescripcion.Text, 50))
            {
                MessageBox.Show("La descripción no puede superar los 50 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tipoActual == TipoAdministracion.Marcas)
            {
                Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                seleccionada.Descripcion = txtDescripcion.Text.Trim();
                MarcaRepositorio repositorio = new MarcaRepositorio();
                repositorio.Modificar(seleccionada);
            }
            else
            {
                Categoria seleccionada = (Categoria)dgvMarcas.CurrentRow.DataBoundItem;
                seleccionada.Descripcion = txtDescripcion.Text.Trim();
                CategoriaRepositorio repositorio = new CategoriaRepositorio();
                repositorio.Modificar(seleccionada);
            }

            CargarDatos();
            MessageBox.Show("Registro modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = 0;
            string descripcion = "";

            if (tipoActual == TipoAdministracion.Marcas)
            {
                Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                id = seleccionada.Id;
                descripcion = seleccionada.Descripcion;
            }
            else
            {
                Categoria seleccionada = (Categoria)dgvMarcas.CurrentRow.DataBoundItem;
                id = seleccionada.Id;
                descripcion = seleccionada.Descripcion;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar el registro '" + descripcion + "'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            if (tipoActual == TipoAdministracion.Marcas)
            {
                MarcaRepositorio repositorio = new MarcaRepositorio();
                repositorio.Eliminar(id);
            }
            else
            {
                CategoriaRepositorio repositorio = new CategoriaRepositorio();
                repositorio.Eliminar(id);
            }

            CargarDatos();
            txtDescripcion.Clear();
            MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow != null)
            {
                if (tipoActual == TipoAdministracion.Marcas)
                {
                    Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    txtDescripcion.Text = seleccionada.Descripcion;
                }
                else
                {
                    Categoria seleccionada = (Categoria)dgvMarcas.CurrentRow.DataBoundItem;
                    txtDescripcion.Text = seleccionada.Descripcion;
                }
            }
        }
    }
}
