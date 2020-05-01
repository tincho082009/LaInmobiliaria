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
        [MinLength(10), MaxLength(50)]
        public string Direccion { get; set; }
        public string Uso { get; set; }
        [MinLength(3), MaxLength(30)]
        public string Tipo { get; set; }
        [DisplayName("Cantidad de ambientes")]
        public int CantAmbientes { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public IFormFileCollection Fotos { get; set; }

        
    }
}
