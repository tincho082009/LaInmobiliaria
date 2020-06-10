using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Inmueble
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public Propietario Propietario { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}.")]
        public string Direccion { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Uso { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "La longitud del {0} deberia ser entre {2} y {1}."), RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo estan permitidas letras")]
        public string Tipo { get; set; }
        [DisplayName("Cantidad de ambientes")]
        public int CantAmbientes { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        //public IFormFileCollection Fotos { get; set; }

        
    }
}
