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
        [MinLength(8), MaxLength(8)]
        public string Dni { get; set; }
        [MinLength(3), MaxLength(20)]
        public string Nombre { get; set; }
        [MinLength(2), MaxLength(20)]
        public string Apellido { get; set; }
        [MinLength(5), MaxLength(20)]
        public string Trabajo { get; set; }
        [DisplayName("Nombre del garante"), MinLength(3), MaxLength(20)]
        public string NombreGarante { get; set; }
        [DisplayName("Dni del garante"), MinLength(8), MaxLength(8)]
        public string DniGarante { get; set; }
    }
}
