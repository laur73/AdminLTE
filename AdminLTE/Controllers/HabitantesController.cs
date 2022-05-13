using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class HabitantesController : Controller
    {
        //Aqui van los repositorios que se crean y se asignan como campos para poder utilizarlos
        private readonly IRepositorioHabitantes repositorioHabitantes;

        //Constructor donde inicializo el repositorio de apoyos
        public HabitantesController(IRepositorioHabitantes repositorioHabitantes)
        {
            this.repositorioHabitantes = repositorioHabitantes;
        }

        //Como trabajamos con programacion asincrona, los metodos para postear la informacion
        //Tambien debe ser asincrona

        //Para mostrar la vista del listado de los apoyos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var habitantes = await repositorioHabitantes.Listar();
            return View(habitantes);
        }

        //Para verificar si ya existe un habitante de forma remota
        [HttpGet]
        public async Task<IActionResult> VerificarExisteHabitante(string nombre, string apepat, string apemat)
        {
            var existeHabitante = await repositorioHabitantes.Existe(nombre, apepat, apemat);

            if (existeHabitante)
            {
                return Json($"El habitante ya existe");
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
        public async Task<IActionResult> Crear(HabitanteViewModel habitante)
        {
            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                return View(habitante);
            }

            //Valida si ya existe el apoyo a insertar
            var existeApoyo = await repositorioHabitantes.Existe(habitante.Nombre, habitante.ApePat, habitante.ApeMat);

            if (existeApoyo)
            {
                ModelState.AddModelError(nameof(habitante.Nombre), $"El habitante ya se encuentra registrado");

                return View(habitante);
            }

            await repositorioHabitantes.Crear(habitante);

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int IdHabitante)
        {
            var habitante = await repositorioHabitantes.ObtenerId(IdHabitante);

            if (habitante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(habitante);

        }

        //Para mostrar la vista del apoyo a editar
        [HttpGet]
        public async Task<IActionResult> Editar(int IdHabitante)
        {
            var habitante = await repositorioHabitantes.ObtenerId(IdHabitante);

            if (habitante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(habitante);

        }

        //Para postear el formulario de actualizar
        [HttpPost]
        public async Task<IActionResult> Editar(HabitanteViewModel habitante)
        {
            var habitanteExiste = await repositorioHabitantes.ObtenerId(habitante.IdHabitante);

            if (habitanteExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioHabitantes.Actualizar(habitante);

            return RedirectToAction("Listar");
        }

        //Vista de confirmación de borrado de apoyo
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdHabitante)
        {
            var habitante = await repositorioHabitantes.ObtenerId(IdHabitante);

            if (habitante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(habitante);
        }

        //Postear la accion de borrar apoyo
        [HttpPost]
        public async Task<IActionResult> Eliminar(HabitanteViewModel habitante)
        {
            await repositorioHabitantes.Eliminar(habitante.IdHabitante);
            return RedirectToAction("Listar");
        }

    }
}
