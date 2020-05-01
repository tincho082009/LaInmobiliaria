using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    [Authorize]
    public class FotoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment environment;
        private readonly IRepositorioFoto rf;
        private readonly IRepositorioInmueble ri;

        public FotoController(IConfiguration configuration, IHostingEnvironment environment, IRepositorioFoto rf, IRepositorioInmueble ri)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.rf = rf;
            this.ri = ri;
        }

        // GET: Foto
        public ActionResult Index()
        {
            var lista = rf.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            var sujeto = rf.ObtenerPorId(id);
            return View(sujeto);
        }

        // GET: Foto/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Inmuebles = ri.ObtenerTodos();
            var sujeto = rf.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(sujeto);
        }

        // POST: Foto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Foto f)
        {
            try
            {
                // TODO: Add update logic here

                rf.Modificacion(f);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(f);
            }
        }

        // GET: Foto/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var sujeto = rf.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(sujeto);
        }

        // POST: Foto/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Foto f)
        {
            try
            {
                // TODO: Add delete logic here

                rf.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index), "Inmuebles");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(f);
            }
        }
    }
}