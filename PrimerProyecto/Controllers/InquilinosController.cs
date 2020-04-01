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
    public class InquilinosController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioInquilino ri;

        public InquilinosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ri = new RepositorioInquilino(configuration);
        }

        // GET: Inquilinos
        public ActionResult Index()
        {
            var lista = ri.ObtenerTodos();
            return View(lista);
        }

        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            return View(sujeto);
        }

        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                // TODO: Add insert logic here
                int res = ri.Alta(inquilino);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inquilino inquilino)
        {
            try
            {
                // TODO: Add update logic here
                ri.Modificacion(inquilino);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            var sujeto = ri.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Inquilinos/Delete/5
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