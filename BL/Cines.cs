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



        public static ML.Result Promedio()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    decimal sumatoria = 0;
                    int cont1 = 0,cont2=0,cont3=0,cont4=0,cont5=0;

                    var DepList = context.Cines.FromSqlRaw($"GetVentas");
                    result.Objects = new List<object>();
                    foreach (var row in DepList)
                    {
                        ML.Cine cine = new ML.Cine();
                     
                        cine.Nombre = row.Nombre;
                        cine.Direccion = row.Direccion;
                        cine.IdZona = row.IdZona.Value;
                        cine.Ventas = row.Ventas.Value;
                        result.Objects.Add(cine);
                        foreach (var item in result.Objects)
                        {
                            switch (cine.IdZona)
                            {
                                case 1:
                                    cont1++;
                                    sumatoria += cine.Ventas;
                                    decimal promedio1 = (sumatoria / cont1) * 100;
                                    cine.Promedio1 = promedio1;
                                  
                                    break;
                                case 2:
                                    cont2++;
                                    sumatoria += cine.Ventas;
                                    decimal promedio2 = (sumatoria / cont2) * 100;
                                    cine.Promedio2 = promedio2;
                                    
                                    break;
                                case 3:
                                    cont3++;
                                    sumatoria += cine.Ventas;
                                    decimal promedio3 = (sumatoria / cont3) * 100;
                                    cine.Promedio3 = promedio3;
                                   
                                    break;
                                case 4:
                                    cont4++;
                                    sumatoria += cine.Ventas;
                                    decimal promedio4 = (sumatoria / cont4) * 100;
                                    cine.Promedio4 = promedio4;
                                   
                                    break;
                                case 5:
                                    cont5++;
                                    sumatoria += cine.Ventas;
                                    decimal promedio5 = (sumatoria / cont5) *100;
                                    cine.Promedio5 = promedio5;
                                   
                                    break;
                            }
                            
                            
                        }
                        result.Correct = true;

                        

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
    }
}
