using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models
{
    public class ApoyoViewModel
    {
        public int IdApoyo { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
