using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Inquilino
    {
        [Key]
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Trabajo { get; set; }
        public string NombreGarante { get; set; }
        public string DniGarante { get; set; }
    }
}
