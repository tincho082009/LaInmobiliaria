using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Foto
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        [MinLength(1), MaxLength(100)]
        public string Url { get; set; }
        [MinLength(1), MaxLength(50)]
        public string Tipo { get; set; }       
        public int InmuebleId { get; set; }
        public Inmueble inmueble { get; set; }
    }
}
