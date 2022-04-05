using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Chartjs()
        {
            return View();
        }

        public IActionResult Flot()
        {
            return View();
        }
    }
}
