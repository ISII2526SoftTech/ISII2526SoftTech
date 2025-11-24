    using AppForSEII2526.API.DTOs.ComprarDTOs;
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
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]//Devuelve herramientas filtradas por fabricante y precio maximo

        public async Task<ActionResult> GetSelectFiltradoOferta(string? fabricante, double? precioMaximo = null)
        {

            var query = _context.Herramienta.AsQueryable();

            if (precioMaximo.HasValue)
                query = query.Where(h => h.Precio <= precioMaximo.Value);
            if (!string.IsNullOrEmpty(fabricante))
                query = query.Where(h => h.Fabricante.Nombre.Contains(fabricante));
            var herramientas = await query
                .Select(h => new HerramientaDTO() {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    Material = h.Material,
                    Precio = (double)h.Precio,
                    Fabricante = h.Fabricante
                })
                .ToListAsync();
            //_logger.LogInformation("FiltradoOferta", "Se ha filtrado correctamente");
            return Ok(herramientas);
            /*
            try 
            {
                IList<OfertaSelectDTO> herramientas = await _context.Herramienta


                .Where(h =>
                   (h.Fabricante.Nombre == null || h.Fabricante.Nombre.Contains(fabricante))
                    && (precioMaximo == null || h.Precio <= precioMaximo)
                    )

                .OrderBy(h => h.Nombre)

                .Select(h => new OfertaSelectDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante))
                .ToListAsync();
                return Ok(herramientas);
            }
            catch(System.InvalidOperationException ex)
            {

            }
           */



        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaComprarDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetHerramientaComprar(string? material, decimal? precio, string? nombreHerramienta)//Devuelve todo lo relativo a Herramienta
        {
            if (_context.Herramienta == null)
            {
                _logger.LogWarning("No se encontraron herramientas");
                return NotFound();
            }
            _logger.LogInformation("Se han encontrado herramientas");
         
            var herramientas = await _context.Herramienta
                .Include(h => h.Fabricante)
                .Where(h => (material == null || h.Material.ToLower().Contains(material.ToLower())) &&
                       (precio == null || h.Precio<=(double)precio)&&
                       (nombreHerramienta == null || h.Nombre.ToLower().Contains(nombreHerramienta.ToLower())))
                .OrderBy(h=>h.Nombre)
                .Select(h => new HerramientaComprarDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (decimal)h.Precio,
                    h.Fabricante.Nombre
                    ))
                .ToListAsync();
            _logger.LogInformation("Finalizando la muestra de herramientas");
            return Ok(herramientas);
        }



        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSelectFiltradoReparacion(string? nombreFiltro,string? tiempoReparacionFiltro)//Devuelve herramientas filtradas por nombre y tiempo de reparacion
        {

            var query = _context.Herramienta.AsQueryable();
            if (!string.IsNullOrEmpty(nombreFiltro))
                query = query.Where(h => h.Nombre.Contains(nombreFiltro));
            if (!string.IsNullOrEmpty(tiempoReparacionFiltro))
                query = query.Where(h => h.TiempoReparacion.Contains(tiempoReparacionFiltro));

            var herramientas = await query
                .Select(h => new HerramientaDTO()
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    Material = h.Material,
                    Fabricante = h.Fabricante,
                    Precio = (double)h.Precio,
                    TiempoReparacion= h.TiempoReparacion
                })
                .ToListAsync();
          
            return Ok(herramientas);
           
        }





    }


}








