using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Propietario //: IValidatableObject
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        [StringLength(8, MinimumLength =7, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}.")]
        public string Dni { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}.")]
        public string Nombre { get; set; }
        [StringLength(20, MinimumLength = 2, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}.")]
        public string Apellido { get; set; } 
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6), MaxLength(20)]
        public string Telefono { get; set; }
    }
    //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    //{

    //}
}
