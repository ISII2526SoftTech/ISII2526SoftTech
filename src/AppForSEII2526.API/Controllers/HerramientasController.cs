using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHerramientas()
        {

            var herramientas = await _context.Herramientas
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.TiempoReparacion))
                .ToListAsync();
            /*
            var query = _context.Herramientas
                
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(h => h.Nombre.Contains(nombre));

            
            var herramientas = await query
                .OrderBy(h => h.Nombre)
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    h.Fabricante, 
                    (double)h.Precio,
                    h.TiempoReparacion))
                .ToListAsync();
            */
            return Ok(herramientas);
        }
        
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<HerramientaDTO>), (int)HttpStatusCode.OK)]
        
        public async Task<ActionResult> GetHerramientasDetalle(double? precioMaximo = null)
        {
            
            var query = _context.Herramientas.AsQueryable();


            if (precioMaximo.HasValue)
                query = query.Where(h => h.Precio <= precioMaximo.Value);
            

            var herramientas = await query
                .Select(h => new HerramientaDTO
                {
                    Nombre = h.Nombre,
                    Material = h.Material,
                    Precio = h.Precio
                })
                .ToListAsync();
            
            return Ok(herramientas);
        }
        
    }

        
}








