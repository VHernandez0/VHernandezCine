using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using System.Web;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetUserName(ML.Usuario usuario)
        {
			ML.Result result = new ML.Result(); 
			try
			{
				using(DL.CinesContext context=new DL.CinesContext())
				{
					var query = context.Usuarios.FromSqlRaw($" GetUserName '{usuario.UserName}'").AsEnumerable().FirstOrDefault();
					if (query!=null)
					{
					
						usuario.IdUsuario = query.IdUsuario;
						usuario.Nombre = query.Nombre;
						usuario.ApellidoPaterno = query.ApellidoPaterno;
						usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Password = query.Password;
						usuario.UserName = query.Username;

						result.Object = usuario;
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
        public static ML.Result GetEmail(string Correo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($" GetEmail '{Correo}'").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Correo = query.Correo;
                        usuario.UserName = query.Username;

                        result.Object = usuario;
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

        public static ML.Result AddUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context=new DL.CinesContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"Addusuario '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Correo}', '{usuario.UserName}', @Password", new SqlParameter("@Password", usuario.Password));
                    
                    if (query > 0)
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

        public static ML.Result GetByEmail(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($" GetByEmail '{usuario.Correo}'").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Password = query.Password;
                        usuario.UserName = query.Username;

                        result.Object = usuario;
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
        public static ML.Result UpdatePassword(string Correo, byte[] Password)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CinesContext context = new DL.CinesContext())
                {
                    int RowsAffected = context.Database.ExecuteSqlRaw($"UpdatePassword '{Correo}', @Password", new SqlParameter("@Password", Password));
                    
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
