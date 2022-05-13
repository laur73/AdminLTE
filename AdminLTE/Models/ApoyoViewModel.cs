using AdminLTE.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models
{
    public class ApoyoViewModel
    {
        public int IdApoyo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage ="La longitud debe estar entre {2} y {1}")]
        [Remote(action: "VerificarExisteApoyo", controller:"Apoyos")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range (minimum:0, maximum:1000, ErrorMessage ="El valor debe estar entre {1} y {2}")]
        public int Cantidad { get; set; }
    }
}
