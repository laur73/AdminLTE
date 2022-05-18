using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminLTE.Controllers
{
    public class BeneficiosController : Controller
    {
        private readonly IRepositorioBeneficios repositorioBeneficios;
        private readonly IRepositorioHabitantes repositorioHabitantes;
        private readonly IRepositorioApoyos repositorioApoyos;

        //En este constructor cargamos los repositorios que vamos a necesitar
        //Para este caso también necesitaremos de habitantes y apoyos para poder cargar
        //el listado de los items de cada coleccion
        public BeneficiosController(IRepositorioBeneficios repositorioBeneficios, IRepositorioHabitantes repositorioHabitantes,
            IRepositorioApoyos repositorioApoyos)
        {
            this.repositorioBeneficios = repositorioBeneficios;
            this.repositorioHabitantes = repositorioHabitantes;
            this.repositorioApoyos = repositorioApoyos;
        }

        //Para mostrar la vista del listado de los apoyos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var beneficios = await repositorioBeneficios.Listar();
            return View(beneficios);
        }

        //Para verificar si ya existe un beneficio de forma remota
        [HttpGet]
        public async Task<IActionResult> VerificarExisteHabitante(int idHabitante, int idApoyo)
        {
            var existeBeneficio = await repositorioBeneficios.Existe(idHabitante, idApoyo);

            if (existeBeneficio)
            {
                return Json($"El beneficio ya existe");
            }

            return Json(true);
        }

        //Para mostrar la vista del formulario crear
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            //El modelo que vamos a usar para cargar el formulario
            var beneficio = new BeneficioCreacionViewModel();
            //Hacemos el mapeo de ambos SelectListItem
            //El metodo esta al final de todo
            beneficio.Habitantes = await ObtenerHabitantes();
            beneficio.Apoyos = await ObtenerApoyos();
            return View(beneficio);
        }

        //Para cuando se envian datos desde el formulario crear
        [HttpPost]
        public async Task<IActionResult> Crear(BeneficioCreacionViewModel beneficio)
        {
            //Validar que el habitante y apoyo que se envía sea valido, existente, accesible
            var habitante = await repositorioHabitantes.ObtenerId(beneficio.IdHabitante);
            var apoyo = await repositorioApoyos.ObtenerId(beneficio.IdApoyo);

            if (habitante is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (apoyo is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            //Valida los campos del modelo
            if (!ModelState.IsValid)
            {
                //Necesitamos obtener a los habitantes y apoyos para poder volver a cargar la vista
                beneficio.Habitantes = await ObtenerHabitantes();
                beneficio.Apoyos = await ObtenerApoyos();
                return View(beneficio);
            }

            //Valida si ya existe el beneficio a insertar
            var existeBeneficio = await repositorioBeneficios.Existe(beneficio.IdHabitante, beneficio.IdApoyo);

            if (existeBeneficio)
            {
                ModelState.AddModelError(nameof(beneficio.IdHabitante), $"El beneficio ya se encuentra registrado");

                //Aquí igual necesitamos obtener a los habitantes y apoyos para poder volver a cargar la vista
                beneficio.Habitantes = await ObtenerHabitantes();
                beneficio.Apoyos = await ObtenerApoyos();
                return View(beneficio);
            }

            await repositorioBeneficios.Crear(beneficio);

            return RedirectToAction("Listar");
        }

        //Para mostrar la vista del Beneficio a editar
        [HttpGet]
        public async Task<IActionResult> Editar(int IdBeneficio)
        {
            var beneficio = await repositorioBeneficios.ObtenerId(IdBeneficio);

            if (beneficio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var modelo = new BeneficioCreacionViewModel()
            {
                IdBeneficio = beneficio.IdBeneficio,
                IdHabitante = beneficio.IdHabitante,
                IdApoyo = beneficio.IdApoyo,
                Cantidad = beneficio.Cantidad,
                IdEstado = beneficio.IdEstado,
                Fecha = beneficio.Fecha
            };

            modelo.Habitantes = await ObtenerHabitantes();
            modelo.Apoyos = await ObtenerApoyos();

            return View(modelo);

        }

        //Para postear el formulario de actualizar
        [HttpPost]
        public async Task<IActionResult> Editar(BeneficioCreacionViewModel beneficio)
        {
            var beneficioExiste = await repositorioBeneficios.ObtenerId(beneficio.IdBeneficio);

            if (beneficioExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioBeneficios.Actualizar(beneficio);

            return RedirectToAction("Listar");
        }

        //Vista de confirmación de borrado de apoyo
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdBeneficio)
        {
            var beneficio = await repositorioBeneficios.ObtenerId(IdBeneficio);

            if (beneficio is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(beneficio);
        }

        //Postear la accion de borrar apoyo
        [HttpPost]
        public async Task<IActionResult> Eliminar(BeneficioViewModel beneficio)
        {
            await repositorioBeneficios.Eliminar(beneficio.IdBeneficio);
            return RedirectToAction("Listar");
        }

        //Métodos para mapear a los SelectListItem y asi poder obtener sus items
        private async Task<IEnumerable<SelectListItem>> ObtenerHabitantes()
        {
            var habitantes = await repositorioHabitantes.Listar();
            //x.Texto (que ve el usuario), x.Valor.ToString() (el valor del id del item)
            return habitantes.Select(x => new SelectListItem(x.Nombre, x.IdHabitante.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerApoyos()
        {
            var apoyos = await repositorioApoyos.Listar();
            return apoyos.Select(x => new SelectListItem(x.Nombre, x.IdApoyo.ToString()));
        }
    }
}
