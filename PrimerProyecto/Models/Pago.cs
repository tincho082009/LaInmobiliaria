using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int NroPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Importe { get; set; }
        public int ContratoId { get; set; }
        public ContratoAlquiler ContratoAlquiler { get; set; }

    }
}
