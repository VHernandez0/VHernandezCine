using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetUserName(string UserName)
        {
			ML.Result result = new ML.Result(); 
			try
			{
				using(DL.CinesContext context=new DL.CinesContext())
				{
					var query = context.Usuarios.FromSqlRaw($" GetUserName '{UserName}'").AsEnumerable().FirstOrDefault();
					if (query!=null)
					{
						ML.Usuario usuario = new ML.Usuario();
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

    }
}
