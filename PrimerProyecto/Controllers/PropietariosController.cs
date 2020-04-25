using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    [Authorize]
    public class PropietariosController : Controller
	{
        private readonly IConfiguration configuration;
        private readonly IRepositorioPropietario repositorioPropietario;
        private readonly IRepositorioInmueble repositorioInmueble;

        public PropietariosController(IRepositorioPropietario repositorioPropietario,  IRepositorioInmueble repositorioInmueble, IConfiguration configuration)
        {          
            this.repositorioPropietario = repositorioPropietario;
            this.repositorioInmueble = repositorioInmueble;
            this.configuration = configuration;
        }

        // GET: Propietario
        public ActionResult Index(int id)
        {
            ViewBag.IdSeleccionado = id;
            var lista = repositorioPropietario.ObtenerTodos();
            if (TempData.ContainsKey("Id"))
                ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            return View(lista);
		}

        // GET: Propietario/Details/5
        public ActionResult Details(int id)
        {
            var sujeto = repositorioPropietario.ObtenerPorId(id);
            return View(sujeto);
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    int res = repositorioPropietario.Alta(p);
                    TempData["Id"] = p.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(p);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(p);
            }
        }

        // GET: Propietario/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = repositorioPropietario.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(sujeto);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Propietario p)
        {
            try
            {
                // TODO: Add update logic here
                repositorioPropietario.Modificacion(p);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(p);
            }
        }

        // GET: Propietario/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            var sujeto = repositorioPropietario.ObtenerPorId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(sujeto);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario p)
        {
            try
            {
                // TODO: Add delete logic here
                repositorioPropietario.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(p);
            }
        }
        public ActionResult Buscar(int id)
        {
            var lista = repositorioInmueble.ObtenerTodosPorPropietarioId(id);
            if (TempData.ContainsKey("Mensaje"))
                ViewBag.Mensaje = TempData["Mensaje"];
            if (TempData.ContainsKey("Error"))
                ViewBag.Error = TempData["Error"];
            return View(lista);
        }

    }
}
    




