using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace VHernandezCine.Controllers
{
    public class LoginController : Controller
    {

        private IHostingEnvironment environment;
        private IConfiguration configuration;

        public LoginController(IHostingEnvironment _environment, IConfiguration _configuration)

        {

            environment = _environment;
            configuration = _configuration;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ML.Usuario usuario, string Password)
        {
            var passencrypt = new Rfc2898DeriveBytes(Password, new byte[0], 1000, HashAlgorithmName.SHA256);
            var passconvertd = passencrypt.GetBytes(20);
            ML.Result result = BL.Usuario.GetUserName(usuario);
            if (result.Correct == false)
            {
                usuario.Password = passconvertd;
                ML.Result result1 = BL.Usuario.AddUsuario(usuario);
                if (result1.Correct)
                {
                    ViewBag.Message = "Se añadio con exito";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error con exito";
                    return View("Modal");
                }
            }
            else
            {
                
                usuario = (ML.Usuario)result.Object;
               
                    return RedirectToAction("Index", "Home");
           
            } 
        }
        [HttpGet]
        public IActionResult RecuperarPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RecuperarPassword(string Correo)
        {
            ML.Result result = BL.Usuario.GetEmail(Correo);
         
            
            if (result.Correct)
            {
                string EmailOrigen = configuration["EmailOrigen"];
                MailMessage mailMessage = new MailMessage(EmailOrigen, Correo, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
                mailMessage.IsBodyHtml = true;
                string ContenidoHTML = System.IO.File.ReadAllText(configuration["ContenidoHTML"]);
                mailMessage.Body = ContenidoHTML;
                string url = configuration["URL"] + HttpUtility.UrlEncode(Correo);
                mailMessage.Body = mailMessage.Body.Replace("{url}", url);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, configuration["Password"]);
               
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
                ViewBag.Message = "Se ha enviado un correo de confirmación a tu correo electronico";
                return View("Modal");
            }
            else
            {
                ViewBag.Message = "No existe el Correo Ingresado Verifique correctamente";
                return View("Modal");
            }
         

        }
        [HttpGet]
        public IActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPassword(string Password1,string Password2, string Correo)
        {
            if (Password1==Password2)
            {
                var passencrypt = new Rfc2898DeriveBytes(Password1, new byte[0], 1000, HashAlgorithmName.SHA256);
                var passconvertd = passencrypt.GetBytes(20);

                ML.Result result = BL.Usuario.UpdatePassword(Correo, passconvertd);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualizado con exito";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error con exito";
                    return View("ModalPassword");
                }
            }
            else
            {
                ViewBag.Message = "Las contraseñas no son iguales";
                return View("ModalPassword");
            }
            
        }
    }
}
