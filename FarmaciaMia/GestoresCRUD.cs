using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaMia
{
    public class GestoresCRUD
    {
        private static string conexion = "Data Source=DESKTOP-BLRD3AQ;Initial Catalog=FarmaciaMia;Integrated Security=True;TrustServerCertificate=True";

        public static DataTable BuscarMedicamentos(string texto)
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string consulta = "SELECT  id_medicamento, Nombre, descripcion, Precio FROM Medicamentos WHERE Nombre LIKE @texto";
                SqlCommand cmd = new SqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");

              

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static DataTable ObtenerMedicamentos()
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT id_medicamento, Nombre, descripcion, Precio FROM Medicamentos WHERE Stock > 0";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable ObtenerStockTipos()
        {
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                string query = "SELECT Nombre, Stock, TipoConsumo" +
                    "FROM Medicamentos WHERE " +
                    "(TipoConsumo = 'Alta' AND Stock < 40) OR" +
                    "(TipoConsumo = 'Media' AND Stock < 15) OR" +
                    "(TipoConsumo = 'Baja' AND Stock <= 2);" +
                    "";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int InsertarFactura(decimal total)
        {
            int idFactura = -1;

            using (SqlConnection cnx = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Facturas (totalFactura, fecha) OUTPUT INSERTED.id_factura VALUES (@total, GETDATE())", cnx);
                cmd.Parameters.AddWithValue("@total", total);

                cnx.Open();
                idFactura = (int)cmd.ExecuteScalar(); // obtiene el id generado
            }

            return idFactura;
        }

        public static void InsertarDetalleFactura(int idFactura, int idMedicamento, int cantidad, decimal precioUnitario)
        {
            decimal total = cantidad * precioUnitario;

            using (SqlConnection cnx = new SqlConnection(conexion))
            {
                cnx.Open();

                // Validar stock
                SqlCommand cmdStock = new SqlCommand("SELECT stock FROM Medicamentos WHERE id_medicamento = @id", cnx);
                cmdStock.Parameters.AddWithValue("@id", idMedicamento);

                int stockActual = (int)cmdStock.ExecuteScalar();

                if (stockActual < cantidad)
                {
                    throw new Exception($"Stock insuficiente para el medicamento ID {idMedicamento}. Stock actual: {stockActual}");
                }

                // Insertar detalle
                SqlCommand cmdDetalle = new SqlCommand(
                    @"INSERT INTO DetalleFacturas (id_factura, id_medicamento, cantidad, precio_unitario, total)
                      VALUES (@factura, @med, @cant, @precio, @total)", cnx);

                cmdDetalle.Parameters.AddWithValue("@factura", idFactura);
                cmdDetalle.Parameters.AddWithValue("@med", idMedicamento);
                cmdDetalle.Parameters.AddWithValue("@cant", cantidad);
                cmdDetalle.Parameters.AddWithValue("@precio", precioUnitario);
                cmdDetalle.Parameters.AddWithValue("@total", total);
                cmdDetalle.ExecuteNonQuery();

                // Actualizar stock
                SqlCommand cmdUpdateStock = new SqlCommand(
                    "UPDATE Medicamentos SET stock = stock - @cant WHERE id_medicamento = @med", cnx);
                cmdUpdateStock.Parameters.AddWithValue("@cant", cantidad);
                cmdUpdateStock.Parameters.AddWithValue("@med", idMedicamento);
                cmdUpdateStock.ExecuteNonQuery();
            }
        }
    }
}
