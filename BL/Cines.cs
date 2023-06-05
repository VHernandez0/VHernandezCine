using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cines
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var DepList = context.Cines.FromSqlRaw($"GetAllCines");
                    result.Objects = new List<object>();
                    foreach (var row in DepList)
                    {
                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = row.IdCine;
                        cine.Nombre = row.Nombre;
                        cine.Direccion = row.Direccion;
                        cine.IdZona = row.IdZona.Value;
                        cine.Ventas = row.Ventas.Value;
                        
                        result.Objects.Add(cine);
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

        public static ML.Result Add(ML.Cine cine)
        {

            ML.Result result = new ML.Result();


            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"AddCine '{cine.Nombre}','{cine.Direccion}',{cine.Zona.IdZona},{cine.Ventas}");


                    if (RowsAffected > 0)
                    {
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
            }

            return result;
        }

        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var query = context.Cines.FromSqlRaw($"GetByIdCine {IdCine}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {


                        ML.Cine cine = new ML.Cine();
                        cine.IdCine = query.IdCine;
                        cine.Nombre = query.Nombre;
                        cine.Direccion = query.Direccion;
                        cine.Ventas = query.Ventas.Value;
                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = query.IdZona.Value;

                        result.Object = cine;

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

        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    int RowsAffected = context.Database.ExecuteSql($"DeleteCine {IdCine}");
                    if (RowsAffected > 0)
                    {
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

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"UpdateCine {cine.IdCine},'{cine.Nombre}','{cine.Direccion}',{cine.Zona.IdZona},{cine.Ventas}");

                    if (RowsAffected > 0)
                    {
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
