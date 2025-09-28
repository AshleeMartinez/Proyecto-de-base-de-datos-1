using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmaciaMia
{
    public partial class FrmDetallesFacturas : Form
    {
        public FrmDetallesFacturas(int idFactura)
        {
            InitializeComponent();
            CargarDetallesVenta(idFactura);
        }

        private void CargarDetallesVenta(int idFactura)
        {
            D_Medicamentos datos = new D_Medicamentos();
            dgvDetalles.DataSource = datos.Listar_DetallesVenta(idFactura);
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmDetallesFacturas_Load(object sender, EventArgs e)
        {
            dgvDetalles.Columns[0].Width = 250;
        }
    }
}
