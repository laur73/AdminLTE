using AdminLTE.Models;
using AdminLTE.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly IRepositorioEstadisticas repositorioEstadisticas;

        public EstadisticasController(IRepositorioEstadisticas repositorioEstadisticas)
        {
            this.repositorioEstadisticas = repositorioEstadisticas;
        }

        public async Task <IActionResult> Resumen(EstadisticasViewModel estadisticas)
        {
            var totalHabitantes = await repositorioEstadisticas.ContarHabitantes();
            var totalApoyos = await repositorioEstadisticas.ContarApoyos();
            var totalApoyosAsignados = await repositorioEstadisticas.ContarApoyosAsignados();
            var totalApoyosEntregados = await repositorioEstadisticas.ContarApoyosEntregados();

            var modelo = new EstadisticasViewModel
            {
                Habitantes = totalHabitantes,
                Apoyos = totalApoyos,
                ApoyosA = totalApoyosAsignados,
                ApoyosE = totalApoyosEntregados
            };

            return View(modelo);
        }
    }
}
