using Microsoft.AspNetCore.Mvc;
using ML;

namespace VHernandezCine.Controllers
{
    public class MapController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cines.GetAll();
            ML.Result resultVentas = BL.Cines.Promedio();
            
            if (result.Correct)
            {
                cine.PromedioVentas = resultVentas.Objects;
                cine.Cines = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ha ocurrido un error con exito"+result.ErrorMessage;
            }
            return View(cine);
        }
        [HttpGet]
        public ActionResult form(int IdCine)
        {
           
            ML.Result result1 = BL.Zona.GetAll();
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            cine.Zona.Zonas = new List<object>();

            if (IdCine == 0 || IdCine == null)
            {
             
                cine.Zona.Zonas = result1.Objects;
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cines.GetById(IdCine);
              
                if (result.Correct)
                {
                   
                    cine = (ML.Cine)result.Object;
                    cine.IdZona = cine.Zona.IdZona;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error en la consulta" + result.ErrorMessage;
                    return View("Modal");
                }

            }

        }
        [HttpPost]
        public ActionResult form(ML.Cine cine)
        {
            ML.Result area1 = new ML.Result();
            if (cine.IdCine == 0)
            {
                area1 = BL.Cines.Add(cine);
                if (area1.Correct)
                {
                    ViewBag.Message = "Se Inserto Correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error" + area1.ErrorMessage;
                }
            }
            else
            {
                area1 = BL.Cines.Update(cine);
                if (area1.Correct)
                {
                    ViewBag.Message = "Se actualizo con exito";
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar " + area1.ErrorMessage;
                }
            }
            return View("Modal");
        }

        public ActionResult Delete(int IdCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.IdCine = IdCine;
            ML.Result result = BL.Cines.Delete(IdCine);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino con exito";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar" + result.ErrorMessage;
            }
            return View("Modal");
        }

        public IActionResult GetById(int IdCine)
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cines.GetById(IdCine);

            if (result.Correct)
            {

                cine = (ML.Cine)result.Object;
                cine.IdZona = cine.Zona.IdZona;
                return View(cine);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error en la consulta" + result.ErrorMessage;
                return View("Modal");
            }
        }

        public IActionResult Ventas()
        {
            ML.Result result = BL.Cines.GetAll();
            ML.Cine cine = new ML.Cine();
            ML.Result resultVentas = BL.Cines.Promedio();
            cine.PromedioVentas = new List<object>();

            if (result.Correct)
            {
                cine.Cines = result.Objects;
                cine.PromedioVentas = resultVentas.Objects;
               
            }
            else
            {
                ViewBag.Message = "Ha ocurrido un error con exito" + result.ErrorMessage;
            }
            return View(cine);
        }
    }
}
