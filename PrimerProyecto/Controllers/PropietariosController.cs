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
	public class PropietariosController : Controller
	{
        private readonly IConfiguration configuration;
        private readonly RepositorioPropietario repositorioPropietario;  

        public PropietariosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            repositorioPropietario = new RepositorioPropietario(configuration);
        }

		// GET: Propietario
		public ActionResult Index()
		{  
            var lista = repositorioPropietario.ObtenerTodos();
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
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                // TODO: Add insert logic here
                int res = repositorioPropietario.Alta(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Edit/5
        public ActionResult Edit(int id)
        {
            var sujeto = repositorioPropietario.ObtenerPorId(id);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Delete/5
        public ActionResult Delete(int id)
        {
            var sujeto = repositorioPropietario.ObtenerPorId(id);
            return View(sujeto);
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                repositorioPropietario.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
    




