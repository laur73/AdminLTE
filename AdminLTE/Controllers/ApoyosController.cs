using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class ApoyosController : Controller
    {
        //Aqui van los repositorios que se crean y se asignan como campos para poder utilizarlos
        private readonly IRepositorioApoyos repositorioApoyos;

        //Constructor donde inicializo el repositorio de apoyos
        public ApoyosController(IRepositorioApoyos repositorioApoyos)
        {
            this.repositorioApoyos = repositorioApoyos;
        }

        //Como trabajamos con programacion asincrona, los metodos para postear la informacion
        //Tambien debe ser asincrona

        //Para mostrar la vista del listado de los apoyos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var apoyos = await repositorioApoyos.Listar();
            return View(apoyos);
        }

        //Para verificar si ya existe un apoyo de forma remota
        [HttpGet]
        public async Task<IActionResult> VerificarExisteApoyo(string nombre)
        {
            var existeApoyo = await repositorioApoyos.Existe(nombre);

            if (existeApoyo)
            {
                return Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }

        //Para mostrar la vista del formulario crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        //Para cuando se envian datos desde el formulario crear
        [HttpPost]
        public async Task<IActionResult> Crear(ApoyoViewModel apoyo)
        {
            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(apoyo);
            }

            //Valida si ya existe el apoyo a insertar
            var existeApoyo = await repositorioApoyos.Existe(apoyo.Nombre);

            if (existeApoyo)
            {
                ModelState.AddModelError(nameof(apoyo.Nombre), $"El nombre {apoyo.Nombre} ya existe");

                return View(apoyo);
            }

            await repositorioApoyos.Crear(apoyo);

            return RedirectToAction("Listar");
        }

        //Para mostrar la vista del apoyo a editar
        [HttpGet]
        public async Task<IActionResult> Editar(int IdApoyo)
        {
            var apoyo = await repositorioApoyos.ObtenerId(IdApoyo);

            if (apoyo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(apoyo);

        }

        //Para postear el formulario de actualizar
        [HttpPost]
        public async Task<IActionResult> Editar (ApoyoViewModel apoyo)
        {
            var apoyoExiste = await repositorioApoyos.ObtenerId(apoyo.IdApoyo);

            if (apoyoExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioApoyos.Actualizar(apoyo);

            return RedirectToAction("Listar");
        }

        //Vista de confirmación de borrado de apoyo
        [HttpGet]
        public async Task<IActionResult> Eliminar (int IdApoyo)
        {
            var apoyo = await repositorioApoyos.ObtenerId(IdApoyo);

            if (apoyo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(apoyo);
        }

        //Postear la accion de borrar apoyo
        [HttpPost]
        public async Task<IActionResult> Eliminar(ApoyoViewModel apoyo)
        {
            await repositorioApoyos.Eliminar(apoyo.IdApoyo);
            return RedirectToAction("Listar");
        }

    }
}
