using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class HabitantesController : Controller
    {
        RepositorioHabitantes repositorioHabitantes = new RepositorioHabitantes();
        public IActionResult Listar()
        {
            var lista = repositorioHabitantes.Listar();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(HabitanteViewModel habitante)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = repositorioHabitantes.Crear(habitante);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Editar(int IdHabitante)
        {
            var habitante = repositorioHabitantes.ObtenerId(IdHabitante);

            return View(habitante);
        }

        [HttpPost]
        public IActionResult Editar(HabitanteViewModel habitante)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = repositorioHabitantes.Editar(habitante);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int IdHabitante)
        {
            var apoyo = repositorioHabitantes.ObtenerId(IdHabitante);

            return View(apoyo);
        }

        [HttpPost]
        public IActionResult Eliminar(HabitanteViewModel habitante)
        {

            var respuesta = repositorioHabitantes.Eliminar(habitante.IdHabitante);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
