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
        private readonly IRepositorioContratoAlquiler rca;
        private readonly IRepositorio<Inmueble> rinm;
        private readonly IRepositorio<Inquilino> rinq;
        private readonly IRepositorioPago rp;

        public ContratoAlquilerController(IRepositorioContratoAlquiler rca, IRepositorio<Inmueble> rinm, IRepositorio<Inquilino> rinq, IRepositorioPago rp, IConfiguration configuration)
        { 
            this.rca = rca;
            this.rinm = rinm;
            this.rinq = rinq;
            this.rp = rp;
            this.configuration = configuration;
        }

        // GET: ContratoAlquiler
        public ActionResult Index(int id)
        {
            ViewBag.IdSeleccionado = id;
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
            var res = 0;
            try
            {
                if (ModelState.IsValid)
                {
                   
                    /*var id = Convert.ToInt32(User.Claims.ToList()[3].Value);
                    var contrato = rca.ObtenerPorId(id);
                    var fechaFinalContrato = contrato.FechaFinalizacion;
                    var fechaIniContrato = contrato.FechaInicio;
                    var fechFinal = ca.FechaFinalizacion;
                    var fechIni = ca.FechaInicio;
                    var inqId = ca.InquilinoId;
                    var inmId = ca.InmuebleId;
                    var listaContratoxInm = rca.ObtenerPorInmuebleId(inmId);

                    foreach (var item in listaContratoxInm)
                    {                       
                        if (item.Estado)
                        {
                            var fechaFinalContrato = item.FechaFinalizacion;
                            var fechaIniContrato = item.FechaInicio;
                            if (fechIni <= fechaFinalContrato)
                            {
                                ViewData["Error"] = "No se puede ahr brusco porque este campo ia esta ocupao";
                                return View(ca);
                            }
                            else
                            {

                                ca.Estado = true;
                                res = rca.Alta(ca);
                                TempData["Id"] = ca.Id;
                                return RedirectToAction(nameof(Index));
                            }                           
                        }                   
                    }
                    */
                    ca.Estado = true;
                    res = rca.Alta(ca);
                    TempData["Id"] = ca.Id;
                    return RedirectToAction(nameof(Index));
                    //Aca iria lo que esta pegado arriba por si llega a fallar

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
            var contrato = rca.ObtenerPorId(id);        
            var lista = rp.ObtenerTodosPorContratoId(contrato.Id);
            var cantidadSupuesta = lista.Count;
            var fechaInicio = contrato.FechaInicio;
            var fechaFinal = contrato.FechaFinalizacion;
            TimeSpan t = fechaFinal - fechaInicio;
            var cantidadCoutas = Math.Round(t.TotalDays / 30);
            
            var ahora = DateTime.Now;
            if (cantidadSupuesta == cantidadCoutas && fechaFinal > ahora)
            {
                TimeSpan mora = fechaFinal - ahora;
                var meses = t.TotalDays / 30;
                var mes = (int)Math.Round(meses);
                if (mes == 1)
                {
                    var importe = lista.First().Importe;
                    ViewData["Error"] = "Si borra este contrato adquirira una multa de: $" + importe;
                }else if(mes >= 2)
                {
                    var importe = lista.First().Importe * 2;
                    ViewData["Error"] = "Si borra este contrato adquirira una multa de: $" + importe;
                }
                return View(contrato);
            }
            else
            {
                ViewData["Error"] = "Tiene que pagar los meses que le faltan MOROSO";
                return View(contrato);
            }           

        }

        // POST: ContratoAlquiler/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ContratoAlquiler ca)
        {
            try
            {
                var lista = rp.ObtenerTodosPorContratoId(id);
                foreach (var item in lista)
                {
                    rp.Baja(item.Id);
                }
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
            TimeSpan t = fechaFinal - fechaInicio;
            var meses = t.TotalDays / 30;
           var mes = (int) Math.Round(meses);
            if(nroCuota >= mes)
            {
                TempData["Error"] = "Ya termino de realizar los pagos";
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
        public ActionResult Buscar(int id)
        {
            var lista = rp.ObtenerTodosPorContratoId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(lista);
        }
        public ActionResult Renovar(int id)
        {
            var ca = rca.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            var fechaFinal = ca.FechaFinalizacion;
            var fechaAhora = DateTime.Now;
            if (fechaFinal > fechaAhora)
            {
                TempData["Error"] = "No puede renovar un contrato que aun no ha terminado!!";
                return RedirectToAction(nameof(Index));

            }            
            ViewBag.Inquilino = rinq.ObtenerTodos();
            ViewBag.Inmueble = rinm.ObtenerTodos();
                   
            return View(ca);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renovar(ContratoAlquiler ca)
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
        [Authorize(Policy = "Administrador")]
        public ActionResult Rescindir(int id)
        {
            var contrato = rca.ObtenerPorId(id);
             
            return View(contrato);
        }

        // POST: ContratoAlquiler/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Rescindir(int id, ContratoAlquiler ca)
        {
            try
            {
                //No se si hacer esto jaja pero bueno ahi esta
                var lista = rp.ObtenerTodosPorContratoId(id);             
                foreach (var item in lista)
                {
                    rp.Baja(item.Id);
                }
                var contrato = rca.ObtenerPorId(id);
                contrato.Estado = false;
                rca.Modificacion(contrato);
                TempData["Mensaje"] = "Rescision realizada correctamente";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(ca);
            }
        }

    }
}