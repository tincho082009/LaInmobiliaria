using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class ContratoAlquiler
    {
        [Key]
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public int InquilinoId { get; set; }
        public int InmuebleId { get; set; }
        public Inquilino Inquilino { get; set; }
        public Inmueble Inmueble { get; set; }
    }
}
