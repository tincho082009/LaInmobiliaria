using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInmueble ri;
        private readonly RepositorioPropietario rp;

        public InmueblesController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ri = new RepositorioInmueble(configuration);
            rp = new RepositorioPropietario(configuration);
        }

        // GET: Inmuebles
        public ActionResult Index()
        {
            var lista = ri.ObtenerTodos();
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
                // TODO: Add insert logic here
                int res = ri.Alta(i);
                return RedirectToAction(nameof(Index));
            }
            catch
            {              
                return View();
            }
        }

        // GET: Inmuebles/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            ViewBag.Propietario = rp.ObtenerTodos();
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmuebles/Delete/5
        public ActionResult Delete(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Inmuebles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ri.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}