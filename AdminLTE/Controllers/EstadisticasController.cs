using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class EstadisticasController : Controller
    {
        public IActionResult Resumen()
        {
            return View();
        }
    }
}
