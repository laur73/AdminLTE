using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class ApoyosController : Controller
    {
        RepositorioApoyos repositorioApoyos = new RepositorioApoyos();

        public IActionResult Listar()
        {
            var lista = repositorioApoyos.Listar();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ApoyoViewModel apoyo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = repositorioApoyos.Crear(apoyo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Editar(int IdApoyo)
        {
            var apoyo = repositorioApoyos.ObtenerId(IdApoyo);

            return View(apoyo);
        }

        [HttpPost]
        public IActionResult Editar(ApoyoViewModel apoyo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var respuesta = repositorioApoyos.Editar(apoyo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [HttpGet]
        public IActionResult Eliminar(int IdApoyo)
        {
            var apoyo = repositorioApoyos.ObtenerId(IdApoyo);

            return View(apoyo);
        }

        [HttpPost]
        public IActionResult Eliminar(ApoyoViewModel apoyo)
        {

            var respuesta = repositorioApoyos.Eliminar(apoyo.IdApoyo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
