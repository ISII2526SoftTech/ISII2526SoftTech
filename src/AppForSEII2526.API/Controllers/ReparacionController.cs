
using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionController : ControllerBase
    {
        // Controller para gestionar las reparaciones
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReparacionController> _logger;

        public ReparacionController(ApplicationDbContext context, ILogger<ReparacionController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMostrarReparacionPorId(int id)
        {
            if (_context.Reparacion == null)
            {
                _logger.LogError("Error: no existen reparaciones");
                return NotFound();
            }

            var reparacion = await _context.Reparacion
                .Include(r => r.ApplicationUser)
                .Include(r => r.ReparacionItems)
                .Where(r => r.Id == id)
                
                .Select(r => new ReparacionDetailDTO(
                  r.Id,
                  r.ApplicationUser.NombreCliente,
                  r.ApplicationUser.ApellidoCliente,
                  r.ApplicationUser.Telefono,
                  r.FechaEntrega,
                  r.FechaRecogida,
                  r.PrecioTotal
                ))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogError($"Error: La reparacion {id} no existe");
                return NotFound();
            }

            return Ok(reparacion);

        }

    }

}

