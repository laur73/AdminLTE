using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
