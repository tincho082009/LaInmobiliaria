using System;
using System.Collections.Generic;
using System.IO;
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
                f = rf.ObtenerPorId(id);
                int InmId = f.InmuebleId;
                string wwwPath = environment.WebRootPath;
                string path = Path.Combine(wwwPath, "Uploads");
                string urlRenovada = f.Url.Replace("/Uploads\\", "");
                string pathCompleto = Path.Combine(path, urlRenovada);
                System.IO.File.Delete(pathCompleto);
                rf.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction("Fotos", "Inmuebles", new { id= InmId});
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