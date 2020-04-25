using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public interface IRepositorioInmueble : IRepositorio<Inmueble>
    {
        IList<Inmueble> ObtenerTodosPorPropietarioId(int propietarioId);
    }
}
