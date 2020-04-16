using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Usuario
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Rol { get; set; }
        [Required, DataType(DataType.Password)]
        public string Clave { get; set; }

    }
}
