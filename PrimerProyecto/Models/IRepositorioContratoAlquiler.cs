using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public interface IRepositorioContratoAlquiler : IRepositorio<ContratoAlquiler>
    {
        IList<ContratoAlquiler> ObtenerPorInmuebleId(int id);
        IList<ContratoAlquiler> ObtenerTodosVigentes(DateTime fechaInicio, DateTime fechaFinal);
    }
}
