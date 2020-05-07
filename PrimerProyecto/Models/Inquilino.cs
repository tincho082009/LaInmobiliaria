using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Inquilino
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        [StringLength(8, MinimumLength = 7, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("[0-9]*$", ErrorMessage = "Solo estan permitidos numeros")]
        public string Dni { get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Nombre { get; set; }
        [StringLength(20, MinimumLength = 2, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Apellido { get; set; }
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Trabajo { get; set; }
        [DisplayName("Nombre del garante"), StringLength(20, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string NombreGarante { get; set; }
        [DisplayName("Dni del garante"), StringLength(8, MinimumLength = 7, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string DniGarante { get; set; }
    }
}
