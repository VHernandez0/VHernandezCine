using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cine
    {
        public int IdCine { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int IdZona { get; set; }
        public decimal Ventas { get; set; }
        public string NombreZona { get; set; }
        public decimal Promedio1 { get; set; }
        public decimal Promedio2 { get; set; }
        public decimal Promedio3 { get; set; }
        public decimal Promedio4 { get; set; }
        public decimal Promedio5 { get; set; }
        public List<object> Cines { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public List<object> PromedioVentas { get; set; }
        public ML.Zona Zona { get; set; }
    }
}
