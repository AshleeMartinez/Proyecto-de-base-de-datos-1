using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmaciaMia
{
    public partial class VentasForm : Form
    {
        public VentasForm()
        {
            InitializeComponent();
            timer1.Start();
            
        }
        

        #region "Variables"
            int iCodigoMedicamento = 0;
        bool bEstadoGuardar = true;
        #endregion


        #region "Metodos"

        private void CargarMedicamentos(string cBusqueda)
        {
            D_Medicamentos Datos = new D_Medicamentos();
            dgvLista.DataSource = Datos.Listar_Medicamentos(cBusqueda);
            FormatoListaMedicamentos();
            dgvLista.Columns["ID"].Visible = false;
        }

        private void CargarVentas(string cBusqueda)
        {
            D_Medicamentos Datos = new D_Medicamentos();
            dgvVentas.DataSource = Datos.Listar_Facturas(cBusqueda);
            FormatoListaVentas();
            dgvVentas.Columns["Id Factura"].Visible = false;
        }
        private void CargarStock(string cBusqueda)
        {
            D_Medicamentos Datos = new D_Medicamentos();
            dgvVentas.DataSource = Datos.Listar_Stock();
            FormatoListaStock();
            
        }

        private void FormatoListaMedicamentos()
        {
            dgvLista.Columns[0].Width = 60;
            dgvLista.Columns[1].Width = 250;
            dgvLista.Columns[2].Width = 350;
        }
        private void FormatoListaVentas()
        {
            dgvVentas.Columns[0].Width = 120;  // ID Factura
            dgvVentas.Columns[1].Width = 160;  // Total
            dgvVentas.Columns[2].Width = 160;  // Fecha
        }
        private void FormatoListaStock()
        {
            dgvVentas.Columns[0].Width = 230;
            dgvVentas.Columns[1].Width = 60;
            dgvVentas.Columns[2].Width = 90;
        }
        private void ActivarTextos(bool bEstado)
        {
            txtNombre.Enabled = bEstado;
            txtDescripcion.Enabled = bEstado;
            txtPrecio.Enabled = bEstado;
            txtStock.Enabled = bEstado;
            txtEstante.Enabled = bEstado;
            txtTipoConsumo.Enabled = bEstado;

            txtBuscarMed.Enabled = !bEstado;
        }
        private void ActivarBotones(bool bEstado)
        {
            btnNuevoMed.Enabled = bEstado;
            btnActualizarMed.Enabled = bEstado;
            btnEliminarMed.Enabled = bEstado;
            btnVenta.Enabled = bEstado;
           
            btnGuardar.Visible = !bEstado;
            btnCancelar.Visible = !bEstado;

        }
        private void activarVentas(bool mostrar)
        {
            dgvVentas.Visible = !mostrar;
            //btnRealizarVenta.Visible = !mostrar;
            //Rango de Fechas
            btnRangoFechas.Visible = !mostrar;
            pnlRangoFechas.Visible=!mostrar;
            dtpInicio.Visible = !mostrar;
            lblFechaInicio.Visible = !mostrar;
            dtpFin.Visible = !mostrar;
            lblFechaFin.Visible = !mostrar;
            //Fecha Unica

            lblFechaUnica.Visible = !mostrar;
            dtpFechaUnica.Visible=!mostrar;
            pnlFechaUnica.Visible=!mostrar;
            btnFechaUnica.Visible=!mostrar;
            //Ver detalles de ventas
            btnVerDetalles.Visible = !mostrar;


            //datos de ingresar med
            lblnombre.Visible = mostrar;
            lbldescr.Visible = mostrar;
            lblprecio.Visible = mostrar;
            lblstock.Visible = mostrar;
            lblEstante.Visible = mostrar;
            lblTipoConsumo.Visible = mostrar;


            txtNombre.Visible = mostrar;
            txtDescripcion.Visible = mostrar;
            txtPrecio.Visible = mostrar;
            txtStock.Visible = mostrar;
            txtEstante.Visible = mostrar;
            txtTipoConsumo.Visible = mostrar;
        }

        private void SeleccionarMedicamento()
        {
            if (dgvLista.CurrentRow != null && dgvLista.CurrentRow.Index >= 0)
            {
                try
                {
                    iCodigoMedicamento = Convert.ToInt32(dgvLista.CurrentRow.Cells[0].Value); // ID en la primera columna
                    txtNombre.Text = dgvLista.CurrentRow.Cells[1].Value.ToString();
                    txtDescripcion.Text = dgvLista.CurrentRow.Cells[2].Value.ToString();
                    txtPrecio.Text = dgvLista.CurrentRow.Cells[3].Value.ToString();
                    txtStock.Text = dgvLista.CurrentRow.Cells[4].Value.ToString();
                    txtEstante.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Estante"].Value);
                    txtTipoConsumo.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Tipo Consumo"].Value);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar medicamento: " + ex.Message);
                }

            }
        }
        private void Limpiar()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtEstante.Clear();
            txtTipoConsumo.Clear();
            iCodigoMedicamento = 0;
        }

        private void AgregarVentas()
        {
            M_Medicamento Ventas = new M_Medicamento();

            

            Ventas.Nombre = txtNombre.Text;
            Ventas.Precio_Unitario = Convert.ToDecimal(txtPrecio.Text);
            Ventas.Total =  Ventas.Precio_Unitario *  Ventas.Cantidad ;
            Ventas.Fecha = Convert.ToDateTime(txtFecha.Text);

           

            CargarVentas("%");
            Limpiar();

        }

        private void GuardarMedicamentos()
        {
            M_Medicamento Medicamento = new M_Medicamento();
            Medicamento.Nombre = txtNombre.Text;
            Medicamento.Descripcion = txtDescripcion.Text;
            Medicamento.Precio = Convert.ToDecimal(txtPrecio.Text);
            Medicamento.Stock = Convert.ToInt32(txtStock.Text);
            Medicamento.Estante = txtEstante.Text;
            Medicamento.TipoConsumo = txtTipoConsumo.Text;

            D_Medicamentos Datos = new D_Medicamentos();
            string respuesta = Datos.Guardar_Medicamentos(Medicamento);

            if(respuesta == "OK")
            {
                CargarMedicamentos("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Datos guardados correctamente", "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else{
                MessageBox.Show(respuesta, "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
               
        }

        private void ActualizarMedicamentos()
        {
            M_Medicamento Medicamento = new M_Medicamento();

            Medicamento.ID_Medicamento = iCodigoMedicamento;
            Medicamento.Nombre = txtNombre.Text;
            Medicamento.Descripcion = txtDescripcion.Text;
            Medicamento.Precio = Convert.ToDecimal(txtPrecio.Text);
            Medicamento.Stock = Convert.ToInt32(txtStock.Text);
            Medicamento.Estante = txtEstante.Text;
            Medicamento.TipoConsumo = txtTipoConsumo.Text;


            D_Medicamentos Datos = new D_Medicamentos();
            string respuesta = Datos.Actualizar_Medicamentos(Medicamento);

            if (respuesta == "OK")
            {
                CargarMedicamentos("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);
                activarVentas(true);
                MessageBox.Show("Datos Actualizados correctamente", "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show(respuesta, "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void EliminarMedicamentos(int iCodigoMedicamento)
        {
            
            D_Medicamentos Datos = new D_Medicamentos();
            string respuesta = Datos.Eliminar_Medicamento(iCodigoMedicamento);

            if (respuesta == "OK")
            {
                CargarMedicamentos("%");
                Limpiar();
                ActivarTextos(false);
                ActivarBotones(true);

                MessageBox.Show("Registro Eliminado Correctamente", "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show(respuesta, "Sistema de gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private bool ValidarTextos()
        {
            bool hayTextosVacios = false;
            if (string.IsNullOrEmpty(txtNombre.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtDescripcion.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtPrecio.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtStock.Text)) hayTextosVacios = true;
            if (string.IsNullOrEmpty(txtEstante.Text)) hayTextosVacios = true;


            return hayTextosVacios;
        }

        #endregion
       // ---------------------------------------------------------------------------
       //----------------------------------------------------------------------------
       //----------------------------------------------------------------------------


        private void VentasForm_Load(object sender, EventArgs e)
        {
            CargarMedicamentos("%");
            
        }

        private void btnBuscarMed_Click(object sender, EventArgs e)
        {
            CargarMedicamentos(txtBuscarMed.Text);
        }

        private void txtBuscarMed_TextChanged(object sender, EventArgs e)
        {
            CargarMedicamentos(txtBuscarMed.Text);
        }
        
        private void btnNuevoMed_Click(object sender, EventArgs e)
        {
            bEstadoGuardar = true;
            iCodigoMedicamento = 0;

            ActivarTextos(true);
            ActivarBotones(false);
            activarVentas(true);
            txtNombre.Select();
            Limpiar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bEstadoGuardar = true;
            iCodigoMedicamento = 0;
            ActivarTextos(false);
            ActivarBotones(true);
            Limpiar();
            activarVentas(true);
        }
        
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarMedicamento();
        }

        private void btnActualizarMed_Click(object sender, EventArgs e)
        {
            if (iCodigoMedicamento == 0)
            {
                MessageBox.Show("Selecciona un registro" , "Sistema de Gestion de Medicamentos", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                bEstadoGuardar = false;
                ActivarTextos(true);
                ActivarBotones(false);
                activarVentas(true);
                txtNombre.Select();
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarTextos ())
            {
                MessageBox.Show("Hay Campos vacios, debes llenar todos los campos (*) obligatorios", 
                    "Sistema Gestion de Medicamentos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (bEstadoGuardar)
                {
                    GuardarMedicamentos();

                }
                else
                {
                    ActualizarMedicamentos();
                }
                
            }
        }

        private void btnEliminarMed_Click(object sender, EventArgs e)
        {
            if (iCodigoMedicamento == 0)
            {
                MessageBox.Show("Selecciona un registro", "Sistema de Gestion de Medicamentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                DialogResult resultado = MessageBox.Show("Estas seguro de eliminar este registro?",
                   "Sistema de Gestion de Medicamentos",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if(resultado == DialogResult.Yes)
                {
                    EliminarMedicamentos(iCodigoMedicamento);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            try
            {
                bool visible = dgvVentas.Visible;

                activarVentas(visible);

                if (!visible)
                {
                    CargarVentas("%");
                    btnVenta.Text = "Ocultar Facturas";
                    txtsuma.Visible = true;
                }
                else
                {
                    btnVenta.Text = "Ver Facturas";
                    txtsuma.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar ventas: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarVentas();

            txtBuscarMed.Clear();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            
                RealizarVenta realizarVenta = new RealizarVenta();
                realizarVenta.ShowDialog();
            


        }

        private void btnRangoFechas_Click(object sender, EventArgs e)
        {
            FormatoListaVentas();
            try
            {
                D_Medicamentos datos = new D_Medicamentos();
                dgvVentas.DataSource = datos.ListarFacturasRangoFechas(dtpInicio.Value, dtpFin.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechaUnica_Click(object sender, EventArgs e)
        {
            FormatoListaVentas();
            try
            {
                DateTime fechaSeleccionada = dtpFechaUnica.Value.Date; // Solo fecha, sin hora
                D_Medicamentos datos = new D_Medicamentos();
                dgvVentas.DataSource = datos.Listar_Facturas_FechaExacta(fechaSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                int idFactura = Convert.ToInt32(dgvVentas.CurrentRow.Cells[0].Value);
                FrmDetallesFacturas detallesForm = new FrmDetallesFacturas(idFactura);
                detallesForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una factura para ver los detalles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool visible = dgvVentas.Visible;

                

                if (visible)
                {
                    float suma = 0;
                    int contador = 0;

                    contador = dgvVentas.RowCount;

                    for (int i = 0; i < contador; i++)
                    {
                        suma += float.Parse(dgvVentas.Rows[i].Cells[1].Value.ToString());
                    }
                    txtsuma.Text = suma.ToString("F2");
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar caja: " + ex.Message);
            }
        }

        private void txtsuma_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnStock_Click(object sender, EventArgs e)
        {

            try
            {
                bool visible = dgvVentas.Visible;

                

                if (!visible)
                {
                    CargarStock("%");
                    dgvVentas.Visible = true; 
                    btnStock.Text = "Ocultar Stock";
                    
                }
                else
                {
                    dgvVentas.Visible = false;
                    btnStock.Text = "Ver Stock";
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar Stock: " + ex.Message);
            }
        }
    }
}
