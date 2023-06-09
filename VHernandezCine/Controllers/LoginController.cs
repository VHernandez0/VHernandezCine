using Microsoft.AspNetCore.Mvc;

namespace VHernandezCine.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, byte[] Password)
        {
            ML.Result result = BL.Usuario.GetUserName(UserName);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (UserName==usuario.UserName)
                {
                    return View("views/Map/Index.cshtml");
                }
                else
                {
                    ViewBag.message = "Error de credenciales";
                    return PartialView("Modal");
                }
               
            }
            else
            {
                ViewBag.Message = "error en las credenciales";
                return PartialView("Modal");
            }
        }
    }
}
