using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class ContratoAlquiler
    {
        [Key]
        [DisplayName("Codigo")]
        public int Id { get; set; }
        public decimal Monto { get; set; }
        [DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [DisplayName("Fecha de finalizacion")]
        public DateTime FechaFinalizacion { get; set; }
        public int InquilinoId { get; set; }
        public int InmuebleId { get; set; }
        public Inquilino Inquilino { get; set; }
        public Inmueble Inmueble { get; set; }
    }
}
