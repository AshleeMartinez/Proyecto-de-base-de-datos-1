using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaMia
{
    public  class M_Medicamento
    {
        public int ID_Medicamento {  get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock {  get; set; }
        public string TipoConsumo { get; set; }
        public string Estante { get; set; }

        //DetalleFactura
        public int ID_Venta { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
