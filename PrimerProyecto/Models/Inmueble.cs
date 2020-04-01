using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Inmueble
    {
        [Key]
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public Propietario Propietario { get; set; }
        public string Direccion { get; set; }
        public string Uso { get; set; }
        public string Tipo { get; set; }
        public int CantAmbientes { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }

        
    }
}
