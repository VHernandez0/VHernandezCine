using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var DepList = context.Zonas.FromSqlRaw($"ZonaGetAll");
                    result.Objects = new List<object>();
                    foreach (var row in DepList)
                    {
                        ML.Zona zona = new ML.Zona();
                        zona.IdZona = row.IdZona;
                        zona.Nombre = row.Nombre;
                        result.Objects.Add(zona);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var query = context.Cines.FromSqlRaw($"GetByIdZona {cine.Zona.IdZona}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {


                        ML.Zona zona = new ML.Zona();
                        zona.IdZona = query.IdZona.Value;
                        zona.Nombre = query.Nombre;

                        result.Object = zona;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.ex = ex;
            }




            return result;
        }
    }
}
