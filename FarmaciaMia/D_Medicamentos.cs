using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmaciaMia
{
    public class D_Medicamentos
    {

        public DataTable Listar_Medicamentos(string cBusqueda)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                SqlCommand comando = new SqlCommand("sp_listar_medicamentos", cnx);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@cBusqueda", SqlDbType.VarChar).Value = cBusqueda;
                cnx.Open();

                resultado = comando.ExecuteReader();
                tabla.Load(resultado);

                return tabla;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                if(cnx.State == ConnectionState.Open)cnx.Close();
                
            }
        }
        public DataTable Listar_Facturas(string cBusqueda)
        {
            DataTable tabla = new DataTable();
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");

                string consulta = @"
            SELECT 
                id_factura AS [ID Factura],
                totalFactura AS [Total],
                fecha AS [Fecha]
            FROM Facturas
            WHERE CAST(id_factura AS VARCHAR) LIKE '%' + @busqueda + '%'
            ORDER BY id_factura DESC";

                SqlCommand comando = new SqlCommand(consulta, cnx);
                comando.Parameters.AddWithValue("@busqueda", cBusqueda);

                cnx.Open();
                SqlDataReader lector = comando.ExecuteReader();
                tabla.Load(lector);
                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar facturas: " + ex.Message);
                throw;
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }
        }

        public DataTable Listar_Stock()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia;Integrated Security=True;TrustServerCertificate=True"))
            {
                try
                {
                    string consulta = @"
                SELECT 
                    Nombre AS [Nombre],
                    Stock AS [Stock],
                    TipoConsumo AS [Tipo Consumo]
                FROM Medicamentos
                WHERE 
                    (TipoConsumo = 'Alta' AND Stock < 40) OR
                    (TipoConsumo = 'Media' AND Stock < 15) OR
                    (TipoConsumo = 'Baja' AND Stock <= 2);";

                    SqlCommand comando = new SqlCommand(consulta, cnx);

                    cnx.Open();
                    SqlDataReader lector = comando.ExecuteReader();
                    tabla.Load(lector);
                    return tabla;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al listar stock: " + ex.Message);
                    throw;
                }
            }
        }


        public string Guardar_Medicamentos(M_Medicamento Medicamento)
        {
            string respuesta = "";
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                SqlCommand comando = new SqlCommand("sp_Guardar_Medicamentos", cnx);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@cNombre", SqlDbType.VarChar).Value = Medicamento.Nombre;
                comando.Parameters.Add("@cDescripcion", SqlDbType.VarChar).Value = Medicamento.Descripcion;
                comando.Parameters.Add("@cPrecio", SqlDbType.Decimal).Value = Medicamento.Precio;
                comando.Parameters.Add("@cStock", SqlDbType.Int).Value = Medicamento.Stock;
                comando.Parameters.Add("@cEstante", SqlDbType.VarChar).Value = Medicamento.Estante;
                comando.Parameters.Add("@cTipoConsumo", SqlDbType.VarChar).Value = Medicamento.TipoConsumo;
                cnx.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos no se pudieron registrar";

            }catch(Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }
            return respuesta;

        }

        public string Actualizar_Medicamentos(M_Medicamento Medicamento)
        {
            string respuesta = "";
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                SqlCommand comando = new SqlCommand("sp_Actualizar_Medicamentos", cnx);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@nIdMedicamento" ,  SqlDbType.Int).Value  = Medicamento.ID_Medicamento;
                comando.Parameters.Add("@cNombre", SqlDbType.VarChar).Value = Medicamento.Nombre;
                comando.Parameters.Add("@cDescripcion", SqlDbType.VarChar).Value = Medicamento.Descripcion;
                comando.Parameters.Add("@cPrecio", SqlDbType.Decimal).Value = Medicamento.Precio;
                comando.Parameters.Add("@cStock", SqlDbType.Int).Value = Medicamento.Stock;
                comando.Parameters.Add("@cEstante", SqlDbType.VarChar).Value = Medicamento.Estante;
                comando.Parameters.Add("@cTipoConsumo", SqlDbType.VarChar).Value = Medicamento.TipoConsumo;

                cnx.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos no se pudieron registrar";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }
            return respuesta;

        }

        public string Eliminar_Medicamento(int iCodigoMedicamento)
        {
            string respuesta = "";
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                SqlCommand comando = new SqlCommand("sp_Eliminar_Medicamento", cnx);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@nIdMedicamento", SqlDbType.Int).Value = iCodigoMedicamento;
                
                cnx.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "Los Datos no se pudieron Eliminar" : "OK";        }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }
            return respuesta;

        }

        public string Agregar_Ventas(M_Medicamento Ventas)
        {
            string respuesta = "";
            SqlConnection cnx = new SqlConnection();

            try
            {
                cnx = new SqlConnection("Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia; Integrated Security=True;TrustServerCertificate=True");
                SqlCommand comando = new SqlCommand("sp_Listar_Ventas", cnx);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@cNombre", SqlDbType.VarChar).Value = Ventas.Nombre;
                comando.Parameters.Add("@cCantidad", SqlDbType.Int).Value = Ventas.Cantidad;
                comando.Parameters.Add("@cPrecio_Unitario", SqlDbType.Decimal).Value = Ventas.Precio_Unitario;
                comando.Parameters.Add("@cTotal", SqlDbType.Decimal).Value = Ventas.Total;
                comando.Parameters.Add("@cFecha", SqlDbType.DateTime).Value = Ventas.Fecha;
                cnx.Open();

                respuesta = comando.ExecuteNonQuery() >= 1 ? "OK" : "Los Datos no se pudieron registrar";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (cnx.State == ConnectionState.Open) cnx.Close();
            }
            return respuesta;

        }

        public DataTable ListarFacturasRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();
            
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Facturas WHERE CAST(fecha AS DATE) BETWEEN @inicio AND @fin ORDER BY fecha DESC",
                    Conexion.CrearInstancia().ObtenerConexion()))
                {
                    
                    cmd.Parameters.AddWithValue("@inicio",  fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@fin", fechaFin.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar factura por rango: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabla;
        }

        public DataTable Listar_Facturas_FechaExacta(DateTime fecha)
        {
            DataTable tabla = new DataTable();
            SqlConnection conexion = Conexion.CrearInstancia().ObtenerConexion();

            try
            {
                using (SqlCommand comando = new SqlCommand(
                    "SELECT * FROM Facturas WHERE CAST(fecha AS DATE) = @fecha", conexion))
                {
                    comando.Parameters.AddWithValue("@fecha", fecha.Date);

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar facturas por fecha: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabla;
        }

        public DataTable Listar_DetallesVenta(int idFactura)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT m.nombre AS Medicamento,df.cantidad AS Cantidad,df.precio_unitario AS Precio, df.total AS Subtotal " +
                    "FROM DetalleFacturas df " +
                    "INNER JOIN Medicamentos m ON df.id_medicamento = m.id_medicamento " +
                    "WHERE df.id_factura = @idFactura", Conexion.CrearInstancia().ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idFactura", idFactura);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(tabla);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles de venta: " + ex.Message);
            }
            return tabla;
        }

    }


}
