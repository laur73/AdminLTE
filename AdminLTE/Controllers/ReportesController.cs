using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Excel()
        {
            return View();
        }
    }
}
