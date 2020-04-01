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
    public class PagoController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioPago rp;

        public PagoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            rp = new RepositorioPago(configuration);
        }

        // GET: Pago
        public ActionResult Index()
        {
            var lista = rp.ObtenerTodos();
            return View(lista);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            var sujeto = rp.ObtenerPorId(id);
            return View(sujeto);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago p)
        {
            try
            {
                // TODO: Add insert logic here
                var res = rp.Alta(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = rp.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pago p)
        {
            try
            {
                // TODO: Add update logic here
                rp.Modificacion(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            var sujeto = rp.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                rp.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}