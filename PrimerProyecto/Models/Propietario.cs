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
        [StringLength(8, MinimumLength =7, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("[0-9]*$", ErrorMessage = "Solo estan permitidos numeros")]
        public string Dni { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Nombre { get; set; }
        [StringLength(20, MinimumLength = 2, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Apellido { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Clave { get; set; }
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("[0-9]*$", ErrorMessage = "Solo estan permitidos numeros")]
        public string Telefono { get; set; }
    }
    //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    //{

    //}
}
