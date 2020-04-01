using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers
{
    public class ContratoAlquilerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioContratoAlquiler rca;
        private readonly RepositorioInmueble rinm;
        private readonly RepositorioInquilino rinq;

        public ContratoAlquilerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            rca = new RepositorioContratoAlquiler(configuration);
            rinm = new RepositorioInmueble(configuration);
            rinq = new RepositorioInquilino(configuration);
        }

        // GET: ContratoAlquiler
        public ActionResult Index()
        {
            
            var lista = rca.ObtenerTodos();
            return View(lista);
        }

        // GET: ContratoAlquiler/Details/5
        public ActionResult Details(int id)
        {
            var sujeto = rca.ObtenerPorId(id);
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
                // TODO: Add insert logic here
                var res = rca.Alta(ca);
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