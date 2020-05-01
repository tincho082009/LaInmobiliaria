using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerProyecto.Models
{
    public interface IRepositorioFoto : IRepositorio<Foto>
    {
        IList<Foto> ObtenerTodosPorInmuebleId(int inmuebleId);
    }
}
