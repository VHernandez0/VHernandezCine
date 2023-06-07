using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PromedioVentas
    {
        public static ML.Result Promedio(int Idzona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context=new DL.CinesContext())
                {
                    decimal sumatoria = 0;
                    int cont = 0;
                    var DepList = context.Cines.FromSqlRaw($"GetVentas");
                    result.Objects = new List<object>();
                    foreach (var row in DepList)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = row.IdCine;
                        cine.Nombre = row.Nombre;
                        cine.IdZona = row.IdZona.Value;
                        cine.Ventas = row.Ventas.Value;
                        foreach (double item in result.Objects)
                        {
                            switch (cine.IdZona)
                            {
                                case 1:
                                    cont++;
                                    sumatoria = cine.Ventas+cine.Ventas;
                                    decimal promedio1 = sumatoria / cont * 100;
                               break;
                                case 2:
                                    cont++;
                                    sumatoria = cine.Ventas + cine.Ventas;
                                    decimal promedio2 = sumatoria / cont * 100;
                                    break;

                            }
                        }
                     

                        result.Objects.Add(cine);
                        
                    }

                   

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }
    }
}
