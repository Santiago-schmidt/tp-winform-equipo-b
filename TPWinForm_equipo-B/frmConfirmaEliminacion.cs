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
    public partial class frmConfirmaEliminacion : Form
    {
        private Articulo articuloSeleccionado;
        private string codigoGenerado;
        public frmConfirmaEliminacion()
        {
            InitializeComponent();
        }
        // Constructor sobrecargado para recibir el artículo
        public frmConfirmaEliminacion(Articulo articulo) : this()
        {
            articuloSeleccionado = articulo;
        }
        

        private void frmConfirmaEliminacion_Load(object sender, EventArgs e)
        {
            lblEstaSeguro.Text = $"¿ESTÁ SEGURO DE QUE QUIERE ELIMINAR EL ARTÍCULO CON CÓDIGO {articuloSeleccionado.Codigo}?";

            // Generar un código numérico aleatorio de 4 dígitos
            Random rnd = new Random();
            codigoGenerado = rnd.Next(1000, 9999).ToString();
            lbTextoRandom.Text = codigoGenerado;
        }

        private void btConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            if (tbConfirmacion.Text.Trim() == codigoGenerado)
            {
                ArticuloRepositorio repo = new ArticuloRepositorio();
                repo.Eliminar(articuloSeleccionado.Id);

                MessageBox.Show($"El Artículo con código {articuloSeleccionado.Codigo} ha sido eliminado correctamente", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("El código ingresado no coincide. Intente nuevamente.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btSoloModificar_Click(object sender, EventArgs e)
        {
            // Ocultar la ventana de eliminación
            this.Hide();

            // Instanciar y abrir el formulario de modificación
            FrmArticulo frmModificar = new FrmArticulo(articuloSeleccionado);
            frmModificar.ShowDialog();

            // Devolver OK para que el formulario principal actualice la grilla de todos modos
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
