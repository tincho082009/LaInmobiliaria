using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Propietario
    {
        [Key]
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; } 
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
