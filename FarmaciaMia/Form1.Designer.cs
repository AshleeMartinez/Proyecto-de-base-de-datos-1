namespace FarmaciaMia
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logintxt = new System.Windows.Forms.TextBox();
            this.passwordtxt = new System.Windows.Forms.TextBox();
            this.loginbtn = new System.Windows.Forms.Button();
            this.ojoabierto = new System.Windows.Forms.PictureBox();
            this.ojocerrado = new System.Windows.Forms.PictureBox();
            this.roundedPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ojoabierto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ojocerrado)).BeginInit();
            this.roundedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(42, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(42, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // logintxt
            // 
            this.logintxt.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logintxt.Location = new System.Drawing.Point(139, 110);
            this.logintxt.Name = "logintxt";
            this.logintxt.Size = new System.Drawing.Size(129, 29);
            this.logintxt.TabIndex = 2;
            // 
            // passwordtxt
            // 
            this.passwordtxt.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordtxt.Location = new System.Drawing.Point(139, 159);
            this.passwordtxt.Name = "passwordtxt";
            this.passwordtxt.Size = new System.Drawing.Size(129, 29);
            this.passwordtxt.TabIndex = 3;
            this.passwordtxt.UseSystemPasswordChar = true;
            // 
            // loginbtn
            // 
            this.loginbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.loginbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginbtn.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginbtn.ForeColor = System.Drawing.Color.Black;
            this.loginbtn.Location = new System.Drawing.Point(139, 226);
            this.loginbtn.Name = "loginbtn";
            this.loginbtn.Size = new System.Drawing.Size(100, 39);
            this.loginbtn.TabIndex = 4;
            this.loginbtn.Text = "Login";
            this.loginbtn.UseVisualStyleBackColor = false;
            this.loginbtn.Click += new System.EventHandler(this.loginbtn_Click);
            // 
            // ojoabierto
            // 
            this.ojoabierto.Image = global::FarmaciaMia.Properties.Resources.ojo;
            this.ojoabierto.Location = new System.Drawing.Point(286, 166);
            this.ojoabierto.Name = "ojoabierto";
            this.ojoabierto.Size = new System.Drawing.Size(22, 22);
            this.ojoabierto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ojoabierto.TabIndex = 6;
            this.ojoabierto.TabStop = false;
            this.ojoabierto.Visible = false;
            this.ojoabierto.Click += new System.EventHandler(this.ojoabierto_Click);
            // 
            // ojocerrado
            // 
            this.ojocerrado.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ojocerrado.Image = global::FarmaciaMia.Properties.Resources.cerrar_ojo;
            this.ojocerrado.Location = new System.Drawing.Point(286, 166);
            this.ojocerrado.Name = "ojocerrado";
            this.ojocerrado.Size = new System.Drawing.Size(22, 22);
            this.ojocerrado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ojocerrado.TabIndex = 7;
            this.ojocerrado.TabStop = false;
            this.ojocerrado.Click += new System.EventHandler(this.ojocerrado_Click);
            // 
            // roundedPanel
            // 
            this.roundedPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.roundedPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.roundedPanel.Controls.Add(this.pictureBox1);
            this.roundedPanel.Location = new System.Drawing.Point(34, 24);
            this.roundedPanel.Name = "roundedPanel";
            this.roundedPanel.Size = new System.Drawing.Size(303, 285);
            this.roundedPanel.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FarmaciaMia.Properties.Resources.log;
            this.pictureBox1.Location = new System.Drawing.Point(82, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 66);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(369, 355);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ojocerrado);
            this.Controls.Add(this.ojoabierto);
            this.Controls.Add(this.loginbtn);
            this.Controls.Add(this.passwordtxt);
            this.Controls.Add(this.logintxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.roundedPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ojoabierto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ojocerrado)).EndInit();
            this.roundedPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox logintxt;
        private System.Windows.Forms.TextBox passwordtxt;
        private System.Windows.Forms.Button loginbtn;
        private System.Windows.Forms.PictureBox ojoabierto;
        private System.Windows.Forms.PictureBox ojocerrado;
        private System.Windows.Forms.Panel roundedPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

