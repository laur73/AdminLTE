using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Display(Name = "Recuérdame")]
        public bool Recuerdame { get; set; }
    }
}
