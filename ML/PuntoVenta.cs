using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PuntoVenta
    {
        public int Id { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Descripcion { get; set; }
        public decimal Venta { get; set; }
        public decimal TotalVenta { get; set; }
        public ML.Zona Zona { get; set; }
        public ML.Estadistica Estadistica { get; set; }
        public List<object> PuntoVentas { get; set; }

    }
}
