using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrimerProyecto.Models;

namespace PrimerProyecto.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ContratoAlquilerController : Controller
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public ContratoAlquilerController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: api/ContratoAlquiler
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(contexto.ContratoAlquiler.Include(e=>e.Inmueble).ThenInclude(e=>e.Propietario).Where(e=>e.Inmueble.Propietario.Email == usuario));             
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/ContratoAlquiler/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorInmuebleId(int id)
        {
            try
            {
                var usuario = User.Identity.Name;              
                return Ok(contexto.ContratoAlquiler.Include(e => e.Inmueble).ThenInclude(e => e.Propietario).Where(e => e.Inmueble.Propietario.Email == usuario && e.Inmueble.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/ContratoAlquiler
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ContratoAlquiler/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
