using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FarmaciaMia
{
    public partial class Form1 : Form
    {
        private readonly string connStr =
        "Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia;Integrated Security=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            RedondearBordes(roundedPanel, 80); // 20 = radio de redondeo
            ojoabierto.Visible = false;
            ojocerrado.Visible = true;
        }
        private void RedondearBordes(Panel panel, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(panel.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(panel.Width - radio, panel.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, panel.Height - radio, radio, radio), 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }
        private void loginbtn_Click(object sender, EventArgs e)
        {
            

                var username = logintxt.Text.Trim();
                var passworduser = passwordtxt.Text;  // idealmente comparas hash

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passworduser))
                {
                    MessageBox.Show("Debe ingresar usuario y contraseña.", "Datos faltantes",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (var cnx = new SqlConnection(connStr))
                    using (var cmd = new SqlCommand(
                        "SELECT COUNT(1) FROM userbd WHERE username = @username AND passworduser = @passworduser", cnx))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@passworduser", passworduser);

                        cnx.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 1)
                        {
                            // Credenciales correctas: abrir MainForm
                            this.Hide();
                            VentasForm VForm = new VentasForm();
                            VForm.FormClosed += (s, args) => this.Close();
                            VForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            passwordtxt.Clear();
                            passwordtxt.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión:\n" + ex.Message, "Excepción",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ojocerrado_Click(object sender, EventArgs e)
        {
            if (passwordtxt.UseSystemPasswordChar)
            {
                passwordtxt.UseSystemPasswordChar = false;
                ojoabierto.Visible = true;
                ojocerrado.Visible = false;
            }
            else
            {
                passwordtxt.UseSystemPasswordChar = true;
                ojoabierto.Visible = false;
                ojocerrado.Visible = true;
            }
        }

        private void ojoabierto_Click(object sender, EventArgs e)
        {
            if (passwordtxt.UseSystemPasswordChar)
            {
                passwordtxt.UseSystemPasswordChar = false;
                ojoabierto.Visible = true;
                ojocerrado.Visible = false;
            }
            else
            {
                passwordtxt.UseSystemPasswordChar = true;
                ojoabierto.Visible = false;
                ojocerrado.Visible = true;
            }
        }

        

    }
}
