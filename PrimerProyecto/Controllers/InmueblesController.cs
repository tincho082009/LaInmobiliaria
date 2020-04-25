using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    [Authorize]
    public class InmueblesController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioInmueble ri;
        private readonly IRepositorioPropietario rp;
        private readonly IRepositorioContratoAlquiler rca;

        public InmueblesController(IConfiguration configuration, IRepositorioInmueble ri, IRepositorioPropietario rp, IRepositorioContratoAlquiler ca)
        {
            this.configuration = configuration;
            this.ri = ri;
            this.rp = rp;
            this.rca = ca;
        }

        // GET: Inmuebles
        public ActionResult Index(int id)
        {
            ViewBag.IdSeleccionado = id;
            var lista = ri.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View(lista);
        }

        // GET: Inmuebles/Details/5
        public ActionResult Details(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            return View(sujeto);
        }

        // GET: Inmuebles/Create
        public ActionResult Create()
        {
            ViewBag.Propietario = rp.ObtenerTodos();
            return View();
        }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble i)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int res = ri.Alta(i);
                    TempData["Id"] = i.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(i);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }

        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            ViewBag.Propietario = rp.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View(sujeto);
        }

        // POST: Inmuebles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inmueble i)
        {
            try
            {
                // TODO: Add update logic here
                ri.Modificacion(i);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }

        // GET: Inmuebles/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var sujeto = ri.ObtenerPorId(id); 
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View(sujeto);
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inmueble i)
        {
            try
            {
                // TODO: Add delete logic here
                ri.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(i);
            }
        }
        public ActionResult Buscar(int id)
        {
            var lista = rca.ObtenerPorInmuebleId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(lista);
        }
    }
}