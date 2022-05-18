using System.ComponentModel.DataAnnotations;

namespace AdminLTE.Models
{
    public class BeneficioViewModel
    {
        //Vamos a necesitar un select para tener todas las opciones posibles de los habitantes y apoyos
        //En IdHabitante e IdApoyo se va a tener su seleccion en Id pero no se van a contener sus items
        //Por lo que vamos a necesitar de una colección (un IEnumerable) que nos permita almacenar a los habitantes y apoyos
        //Para mostrarlo en el formulario, para ello se crea otra clase que hereda de esta y va a ser el modelo de la vista

        public int IdBeneficio { get; set; }

        //Aquí se tiene el habitante seleccionado (o sea su ID)
        [Display(Name = "Habitante")]
        public int IdHabitante { get; set; }

        //Aquí se tiene el apoyo seleccionado (o sea su ID)
        [Display(Name = "Apoyo")]
        public int IdApoyo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "La Cantidad debe ser mayor a {1}")]
        public int Cantidad { get; set; }

        //Aquí se tiene estado seleccionado segun el Enum
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
         
        public string Habitante { get; set; }
        public string Apoyo { get; set; }
        public string Estado { get; set; }
    }
}
