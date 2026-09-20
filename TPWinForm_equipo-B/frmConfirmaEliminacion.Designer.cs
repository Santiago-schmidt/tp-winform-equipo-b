
namespace TPWinForm_equipo_B
{
    partial class frmConfirmaEliminacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEstaSeguro = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTextoRandom = new System.Windows.Forms.Label();
            this.tbConfirmacion = new System.Windows.Forms.TextBox();
            this.btConfirmarEliminacion = new System.Windows.Forms.Button();
            this.btSoloModificar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEstaSeguro
            // 
            this.lblEstaSeguro.AutoSize = true;
            this.lblEstaSeguro.Font = new System.Drawing.Font("Impact", 16.8F);
            this.lblEstaSeguro.Location = new System.Drawing.Point(80, 20);
            this.lblEstaSeguro.Name = "lblEstaSeguro";
            this.lblEstaSeguro.Size = new System.Drawing.Size(786, 35);
            this.lblEstaSeguro.TabIndex = 0;
            this.lblEstaSeguro.Text = "¿ESTÁ SEGURO DE QUE QUIERE ELIMINAR EL ARTÍCULO CON CÓDIGO S99?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(170, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(610, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "El artículo será permanentemente eliminado y no podrá recuperar su información";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(90, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(359, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Para eliminar de manera definitiva este artículo,";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(90, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "escriba la siguiente línea:";
            // 
            // lbTextoRandom
            // 
            this.lbTextoRandom.AutoSize = true;
            this.lbTextoRandom.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextoRandom.Location = new System.Drawing.Point(324, 127);
            this.lbTextoRandom.Name = "lbTextoRandom";
            this.lbTextoRandom.Size = new System.Drawing.Size(75, 26);
            this.lbTextoRandom.TabIndex = 0;
            this.lbTextoRandom.Text = "hbn56";
            // 
            // tbConfirmacion
            // 
            this.tbConfirmacion.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConfirmacion.Location = new System.Drawing.Point(500, 118);
            this.tbConfirmacion.Name = "tbConfirmacion";
            this.tbConfirmacion.Size = new System.Drawing.Size(366, 34);
            this.tbConfirmacion.TabIndex = 1;
            // 
            // btConfirmarEliminacion
            // 
            this.btConfirmarEliminacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConfirmarEliminacion.Location = new System.Drawing.Point(86, 203);
            this.btConfirmarEliminacion.Name = "btConfirmarEliminacion";
            this.btConfirmarEliminacion.Size = new System.Drawing.Size(228, 38);
            this.btConfirmarEliminacion.TabIndex = 2;
            this.btConfirmarEliminacion.Text = "Si, Quiero Eliminar";
            this.btConfirmarEliminacion.UseVisualStyleBackColor = true;
            this.btConfirmarEliminacion.Click += new System.EventHandler(this.btConfirmarEliminacion_Click);
            // 
            // btSoloModificar
            // 
            this.btSoloModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSoloModificar.Location = new System.Drawing.Point(362, 203);
            this.btSoloModificar.Name = "btSoloModificar";
            this.btSoloModificar.Size = new System.Drawing.Size(228, 38);
            this.btSoloModificar.TabIndex = 2;
            this.btSoloModificar.Text = "Solo Modificar";
            this.btSoloModificar.UseVisualStyleBackColor = true;
            this.btSoloModificar.Click += new System.EventHandler(this.btSoloModificar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(638, 203);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(228, 38);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmConfirmaEliminacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 267);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btSoloModificar);
            this.Controls.Add(this.btConfirmarEliminacion);
            this.Controls.Add(this.tbConfirmacion);
            this.Controls.Add(this.lbTextoRandom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEstaSeguro);
            this.Name = "frmConfirmaEliminacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmación de Eliminación";
            this.Load += new System.EventHandler(this.frmConfirmaEliminacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstaSeguro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTextoRandom;
        private System.Windows.Forms.TextBox tbConfirmacion;
        private System.Windows.Forms.Button btConfirmarEliminacion;
        private System.Windows.Forms.Button btSoloModificar;
        private System.Windows.Forms.Button btnCancelar;
    }
}