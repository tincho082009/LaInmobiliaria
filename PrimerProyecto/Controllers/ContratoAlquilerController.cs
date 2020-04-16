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
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
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
                if (ModelState.IsValid)
                {

                    int res = rca.Alta(ca);
                    TempData["Id"] = ca.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(ca);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(ca);
            }
        }

        // GET: ContratoAlquiler/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Inquilino = rinq.ObtenerTodos();
            ViewBag.Inmueble = rinm.ObtenerTodos();
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
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
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
             catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(ca);
            }
        }

        // GET: ContratoAlquiler/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var sujeto = rca.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(sujeto);
        }

        // POST: ContratoAlquiler/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ContratoAlquiler ca)
        {
            try
            {
                // TODO: Add delete logic here
                rca.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(ca);
            }
        }
        public ActionResult Pagar(int id)
        {           
            var contrato = rca.ObtenerPorId(id);
            var listaVieja = rp.ObtenerTodosPorContratoId(contrato.Id);
            var nroCuota = listaVieja.Count;
            var fechaInicio = contrato.FechaInicio;
            var fechaFinal = contrato.FechaFinalizacion;

            var mesFinal = Convert.ToInt32(fechaFinal.Month.ToString()); ;
            var diaFinal = Convert.ToInt32(fechaFinal.Day.ToString()); ;
            var añoFinal = Convert.ToInt32(fechaFinal.Year.ToString()); ;

            var mesInicio = Convert.ToInt32(fechaInicio.Month.ToString());
            var diaInicio = Convert.ToInt32(fechaInicio.Day.ToString());
            var añoInicio = Convert.ToInt32(fechaInicio.Year.ToString());

            var año = añoFinal - añoInicio;
            var mes = mesFinal - mesInicio;
            var dia = diaFinal - diaInicio;

            if (año > 0)
            {
                if (mes == 0)
                {
                    if (dia == 0 || dia > 0)
                    {
                        mes = 12 * año;
                    }
                    else if (dia < 0)
                    {
                        mes = 12 * año - 1;
                    }
                }
                else if (mes < 0 || mes > 0)
                {
                    if (dia == 0 || dia > 0)
                    {
                        mes += 12 * año;

                    }
                    else if (dia < 0)
                    {
                        mes += 12 * año - 1;
                    }
                }
            }

            if(nroCuota >= mes)
            {
                TempData["Error"] = "Hay que confiar, porque si no confias no hay confianza";
                return RedirectToAction("Index", "Pago");
            }
            else
            {
                var pago = new Pago();
                pago.NroPago = nroCuota + 1;
                pago.FechaPago = DateTime.Now;
                pago.ContratoId = contrato.Id;
                pago.Importe = contrato.Monto / mes;

                rp.Alta(pago);
                TempData["Mensaje"] = "Pago realizado exitosamente!!";
                return RedirectToAction("Index", "Pago");
            }
            
        }
    }
}