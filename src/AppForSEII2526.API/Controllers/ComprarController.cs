using AppForSEII2526.API.DTOs.ComprarDTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprarController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<ComprarController> _logger;

        public ComprarController(ApplicationDbContext context, ILogger<ComprarController> logger)
        {
            _context = context; //context es la base de datos
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ComprarItemDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompraDetalle(int id) //Devuelve todo lo relativo a Oferta para el paso 7
        {
            if (_context.CompraItem == null)
            {
                _logger.LogError("Error: no hay mas existencias de herramientas");
                return NotFound();
            }

            var compra = await _context.Comprar
            .Where(r => r.Id == id)
                .Include(r => r.CompraItems) //join table RentalItems
                   .ThenInclude(ri => ri.comprar) //then join table Movies
                      // .ThenInclude(movie => movie.Cp) //then join table Genre
            .Select(r => new ComprarDetailDTO(r.ApplicationUser.NombreCliente , r.ApplicationUser.ApellidoCliente, r.DireccionEnvio,
                   r.FechaCompra,
                   r.CompraItems
                       .Select(ri => new ComprarItemDTO(ri.cantidad, ri.descripcion,
                               ri.herramienta.Nombre, ri.herramienta.Material,
                               ri.precio)).ToList<ComprarItemDTO>(),
                r.PrecioTotal))
            .FirstOrDefaultAsync();

            return Ok(compra);
        }

     





    }
}
