using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminLTE.Models
{
    //Hereda de BeneficioVM
    public class BeneficioCreacionViewModel : BeneficioViewModel
    {
        //Se agregan las colecciones para almacenar a los habitantes y apoyos
        //para poder seleccionar alguno en la vista de crear
        public IEnumerable<SelectListItem> Habitantes { get; set; }
        public IEnumerable<SelectListItem> Apoyos { get; set; }
        
    }
}
