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
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSelectOferta()//Devuelve solo Id, Nombre, Material y Precio de Herramienta para el paso 2 CU OFERTA
        {

            var herramientas = await _context.Herramienta
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante.Nombre))
                .ToListAsync();

            return Ok(herramientas);
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
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante.Nombre))
                .ToListAsync();

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
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHerramienta1()//Devuelve todo lo relativo a Herramienta
        {

            var herramientas = await _context.Herramienta
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante,
                    h.TiempoReparacion))
                .ToListAsync();

            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHerramientasDisponibles()//Devuelve solo Id, Nombre, Material, Precio y Fabricante de Herramienta para el paso 2 CU Comprar
        {

            var herramientas = await _context.Herramienta
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante))
                .ToListAsync();

            if (!herramientas.Any()) //para el caso de que no haya herramientas disponibles
            {
                return BadRequest("No se encontraron herramientas disponibles");
            }

            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]//Devuelve herramientas filtradas por material y precio 

        public async Task<ActionResult> GetSelectFiltradoCompra(string? material, double? precioMaximo = null)
        {


            var query = _context.Herramienta.AsQueryable();


            if (precioMaximo.HasValue)
                query = query.Where(h => h.Precio <= precioMaximo.Value);
            if (!string.IsNullOrEmpty(material))
                query = query.Where(h => h.Fabricante.Nombre.Contains(material));

            var herramientas = await query
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    (double)h.Precio,
                    h.Fabricante))
                .ToListAsync();

            return Ok(herramientas);
        }







    }


}








