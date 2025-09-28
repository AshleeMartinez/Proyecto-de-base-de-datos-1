using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace FarmaciaMia
{

    public class Conexion
    {
        private static Conexion instancia;
        private SqlConnection cnx;

        public Conexion()
        {
            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                cnx.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Conexion CrearInstancia()
        {
            if (instancia == null)
            {
                instancia = new Conexion();
            }
            return instancia;
        }

        public SqlConnection ObtenerConexion()
        {
            return cnx;
        }



    }
}
