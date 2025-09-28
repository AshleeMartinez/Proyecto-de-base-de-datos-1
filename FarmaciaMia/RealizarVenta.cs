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
    public partial class RealizarVenta : Form
    {
        private DataTable tablaVenta = new DataTable();
        private decimal total = 0;
        public RealizarVenta()
        {
            InitializeComponent();

           
            tablaVenta.Columns.Add("ID");
            tablaVenta.Columns.Add("Nombre");
            tablaVenta.Columns.Add("Precio");
            tablaVenta.Columns.Add("Cantidad");
            tablaVenta.Columns.Add("Subtotal");

            dgvVenta.DataSource = tablaVenta;
            CargarMedicamentos();
            dgvVenta.Columns["ID"].Visible = false;
            dgvMedicamentos.Columns["id_medicamento"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.CurrentRow != null)
            {
                int cantidad = (int)nudCantidad.Value;

                if (cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor que cero.", "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow fila = dgvMedicamentos.CurrentRow;
                int id = Convert.ToInt32(fila.Cells["id_medicamento"].Value);
                string nombre = fila.Cells["Nombre"].Value.ToString();
                decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                decimal subtotal = precio * cantidad;
                
                tablaVenta.Rows.Add(id, nombre, precio, cantidad, subtotal);

                /*total += subtotal;
                lblTotal.Text = $"Total: ${total}";
                */
                
                Obtenertotal();
                textBox1.Clear();
            }
            
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            {
                if (tablaVenta.Rows.Count == 0)
                {
                    MessageBox.Show("Agrega al menos un medicamento.");
                    return;
                }

                try
                {
                    int idFactura = GestoresCRUD.InsertarFactura(total);

                    foreach (DataRow row in tablaVenta.Rows)
                    {
                        int idMed = Convert.ToInt32(row["ID"]);
                        int cantidad = Convert.ToInt32(row["Cantidad"]);
                        decimal precio = Convert.ToDecimal(row["Precio"]);

                        GestoresCRUD.InsertarDetalleFactura(idFactura, idMed, cantidad, precio);
                    }

                    MessageBox.Show("Venta realizada exitosamente.");
                    tablaVenta.Clear();
                    total = 0;
                    lblTotal.Text = "Total: C$0";
                    lblsubtotal.Text = "SubTotal: C$0";
                    lbliva.Text = "Iva 15%: C$0";
                    txtEfectivo.Clear();
                    lblDevolucion.Text = "Devolucion: C$ 0";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar la venta: " + ex.Message);
                }
                
            }

           

        }
        private void CargarMedicamentos()
        {
            DataTable medicamentos = GestoresCRUD.ObtenerMedicamentos();
            dgvMedicamentos.DataSource = medicamentos;
            dgvMedicamentos.Columns[0].Width = 80;
            dgvMedicamentos.Columns[1].Width = 150;
        }
        

        private void RealizarVenta_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();
            Obtenertotal();
            dgvMedicamentos.Columns[0].Width = 250;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                dgvMedicamentos.DataSource = GestoresCRUD.BuscarMedicamentos(texto);
            }
            else
            {
                dgvMedicamentos.DataSource = GestoresCRUD.ObtenerMedicamentos();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            string texto = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                dgvMedicamentos.DataSource = GestoresCRUD.BuscarMedicamentos(texto);
            }
            else
            {
                dgvMedicamentos.DataSource = GestoresCRUD.ObtenerMedicamentos();
            }
            
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
        private void Obtenertotal()
        {

            decimal costot = 0;
            decimal subtotal = 0;
            const double v = 0.15;
            decimal iva = (decimal)v;
            decimal ivatotal;
            foreach (DataRow row in tablaVenta.Rows)
            {
                costot += Convert.ToDecimal(row["Subtotal"]);
            }

            
            ivatotal = costot * iva;
            lbliva.Text = $"Iva 15%: C$    {ivatotal:F2}";
            subtotal = costot;
            costot = costot + ivatotal;
            
            total = costot;
            lblTotal.Text = $"Total:       C$     {costot:F2}";
            lblsubtotal.Text = $"SubTotal: C$    {subtotal:F2}";
            /*
            float costot = 0;
            int contador = 0;

            contador = dgvVenta.RowCount;

            for (int i = 0; i < contador; i++)
            {
                costot += float.Parse(dgvVenta.Rows[i].Cells[3].Value.ToString());
                
            }
            lblTotal.Text = $"Total: ${costot}" ;
            */

        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.CurrentRow != null)
            {
                int index = dgvVenta.CurrentRow.Index;

                if (index >= 0 && index < tablaVenta.Rows.Count)
                {
                    tablaVenta.Rows.RemoveAt(index);
                    Obtenertotal(); // recalcula el total después de eliminar
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEfectivo.Text))
                {
                    decimal efectivo = decimal.Parse(txtEfectivo.Text);

                    if (efectivo >= total)
                    {
                        decimal devolucion = efectivo - total;
                        lblDevolucion.Text = $"Devolución: C${devolucion:F2}";
                    }
                    else
                    {
                        lblDevolucion.Text = "Efectivo insuficiente";
                    }
                }
                else
                {
                    lblDevolucion.Text = "";
                }
            }
            catch
            {
                lblDevolucion.Text = "Entrada inválida";
            }
        }
    }
}
  


