using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models
{
    public class HabitanteViewModel
    {
        public int IdHabitante { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [Display(Name = "Apellido Paterno")]
        public string ApePat { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [Display(Name = "Apellido Materno")]
        public string ApeMat { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.DateTime)]
        public DateTime FechaNac { get; set; }
        
        [StringLength(maximumLength: 100)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:14)]
        [Display(Name ="Teléfono")]
        public string Telefono { get; set; }
    }
}
