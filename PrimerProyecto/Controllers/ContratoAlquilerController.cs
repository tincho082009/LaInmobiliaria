using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    [Authorize]
    public class ContratoAlquilerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioContratoAlquiler rca;
        private readonly RepositorioInmueble rinm;
        private readonly RepositorioInquilino rinq;
        private readonly RepositorioPago rp;

        public ContratoAlquilerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            rca = new RepositorioContratoAlquiler(configuration);
            rinm = new RepositorioInmueble(configuration);
            rinq = new RepositorioInquilino(configuration);
            rp = new RepositorioPago(configuration);
        }

        // GET: ContratoAlquiler
        public ActionResult Index()
        {
            
            var lista = rca.ObtenerTodos();
            return View(lista);
        }

        // GET: ContratoAlquiler/Pagos/5
        public ActionResult Details(int id)
        {

            var sujeto = rp.ObtenerTodos();
            return View(sujeto);
        }

        // GET: ContratoAlquiler/Create
        public ActionResult Create()
        {
            ViewBag.Inmueble = rinm.ObtenerTodos();
            ViewBag.Inquilino = rinq.ObtenerTodos();
            return View();
        }

        // POST: ContratoAlquiler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContratoAlquiler ca)
        {
            try
            {
                var res = rca.Alta(ca);
               /* var contrato = rca.ObtenerPorId(res);
                var fechaInicio = contrato.FechaInicio;
                var fechaFinal = contrato.FechaFinalizacion;

                var mesFinal = Convert.ToInt32(fechaFinal.Month.ToString()); ;
                var diaFinal = Convert.ToInt32(fechaFinal.Day.ToString()); ;
                var añoFinal = Convert.ToInt32(fechaFinal.Year.ToString()); ;

                var mesInicio = Convert.ToInt32(fechaInicio.Month.ToString());
                var diaInicio = Convert.ToInt32(fechaInicio.Day.ToString());
                var añoInicio = Convert.ToInt32(fechaInicio.Year.ToString());

                var cant = 0;

                var año = añoFinal - añoInicio;
                var mes = mesFinal - mesInicio;
                var dia = diaFinal - diaInicio;

                if (año == 0)
                {
                    if (dia == 0 || dia > 0)
                    {
                        cant = mes + 1;

                    } else if (dia < 0)
                    {
                        cant = mes;
                    }
                }
                else if (año > 0)
                {
                    if (mes == 0)
                    {
                        if (dia == 0 || dia > 0)
                        {
                            cant = 12 * año + 1;
                        } else if (dia < 0)
                        {
                            cant = 12 * año ;
                        }
                    }
                    else if (mes < 0 || mes > 0)
                    {
                        if (dia == 0 || dia > 0)
                        {
                            cant = 12 * año + mes + 1;
                        } else if (dia < 0)
                        {
                            cant = 12 * año + mes;
                        }
                    }
                   
                }

                var x = cant;
                */
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContratoAlquiler/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Inquilino = rinq.ObtenerTodos();
            ViewBag.Inmueble = rinm.ObtenerTodos();
          
            var sujeto = rca.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: ContratoAlquiler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContratoAlquiler ca)
        {
            try
            {
                // TODO: Add update logic here
                rca.Modificacion(ca);
                return RedirectToAction(nameof(Index));
            }
             catch
            {
                return View();
            }
        }

        // GET: ContratoAlquiler/Delete/5
        public ActionResult Delete(int id)
        {
            var sujeto = rca.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: ContratoAlquiler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                rca.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}