using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OfertasController> _logger;

        public OfertasController(ApplicationDbContext context, ILogger<OfertasController> logger)
        {
            _context = context;
            _logger = logger;
            //_logger.LogInformation("TodoService initialized");
            
        }


        

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOfertaDetallePorId(int? id)
        {
            if (_context.Oferta == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }
            

            var oferta = await _context.Oferta
                .Include(o => o.ApplicationUser)
                .Include(o => o.OfertaItems)
                .Where(o => o.Id == id)
                .Select(o => new OfertaDetailDTO(
                o.FechaInicio,
                o.FechaFinal,
                (TiposMetodoPago)o.MetodoPago,
                o.OfertaItems.Select(oi => new OfertaItemDTO(
                    oi.Herramienta.Id,
                    oi.Porcentaje,
                    oi.PrecioOriginal,
                    oi.PrecioFinal
                )
                ).ToList(),
                o.Id,
                (TiposDirigidaOferta)o.DirigidaA,
                o.ApplicationUser.NombreCliente
                ))
                .FirstOrDefaultAsync();

                if (oferta == null)
                {
                    _logger.LogError($"Error: La oferta {id} no existe");
                    return NotFound();
                }

                return Ok(oferta);

        }

        [HttpGet]
        [Route("[action]")]//ESTE METODO NO SE CUENTA PARA EL SPRINT 2, NO ESTA TESTEADO PORQUE ES EXTRA A LA ENTREGA
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetOfertaDetallePorId2(int? id, DateTime? FechaMax)
        {
            if (id == null && FechaMax == null)
            {
                _logger.LogError("Error: Se debe proporcionar al menos un parámetro (id o FechaMax)");
                return BadRequest("Se debe proporcionar al menos un parámetro (id o FechaMax)");
            }

            if (_context.Oferta == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }
            var query = _context.Oferta
                .Include(o => o.ApplicationUser)
                .Include(o => o.OfertaItems)
                    .ThenInclude(oi => oi.Herramienta)
                .AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(o => o.Id == id.Value);
            }

            if (FechaMax.HasValue)
            {
               
                query = query.Where(o => o.FechaFinal <= FechaMax.Value);
            }

            // Ejecutar la consulta
            var oferta = await query
                .Select(o => new OfertaDetailDTO(
                    o.FechaInicio,
                    o.FechaFinal,
                    (TiposMetodoPago)o.MetodoPago,
                    o.OfertaItems.Select(oi => new OfertaItemDTO(
                        oi.Herramienta.Id,
                        oi.Porcentaje,
                        oi.PrecioOriginal,
                        oi.PrecioFinal
                    )).ToList(),
                    o.Id,
                    (TiposDirigidaOferta)o.DirigidaA,
                    o.ApplicationUser.NombreCliente
                ))
                .ToListAsync();

            if (oferta == null)
            {
                string errorMsg = id.HasValue
                    ? $"Error: La oferta {id} no existe"
                    : $"Error: No se encontraron ofertas con fecha máxima {FechaMax}";

                _logger.LogError(errorMsg);
                return NotFound();
            }

            return Ok(oferta);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateOferta(OfertaForCreateDTO ofertaForCreate)
        {

            if (ofertaForCreate.FechaInicio < DateTime.Today)
            {
                _logger.LogError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy");
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy");
            }
            if (ofertaForCreate.FechaFinal <= ofertaForCreate.FechaInicio) { 
                _logger.LogError("FechaFinal", "La fecha de fin debe ser posterior a la fecha de inicio");
                ModelState.AddModelError("FechaFinal", "La fecha de fin debe ser posterior a la fecha de inicio");
            }   
            if (ofertaForCreate.OfertaItems == null || ofertaForCreate.OfertaItems.Count == 0)
            {
                _logger.LogError("Items", "Debe incluir al menos una herramienta en la oferta");
                ModelState.AddModelError("Items", "Debe incluir al menos una herramienta en la oferta");
            }
            if (ofertaForCreate.OfertaItems != null)
            {
                foreach (var item in ofertaForCreate.OfertaItems)
                {
                    if (item.Porcentaje <= 0 || item.Porcentaje > 100)
                        ModelState.AddModelError("Porcentaje", $"El porcentaje de rebaja debe estar entre 1 y 100");
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var herramientaIds = ofertaForCreate.OfertaItems.Select(oi => oi.HerramientaId).ToList();
            var appUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.NombreCliente == ofertaForCreate.NombreUsuario);

            var herramientas = await _context.Herramienta
                .Include(h => h.OfertaItems)
                    .ThenInclude(oi => oi.Oferta)
                .Where(h => herramientaIds.Contains(h.Id))
                .ToListAsync();
            foreach (var item in ofertaForCreate.OfertaItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Id == item.HerramientaId);
                if (herramienta == null)
                {
                    _logger.LogError("Herramientas", $"La herramienta con ID {item.HerramientaId} no existe");
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} no existe");
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            Oferta oferta = new Oferta
            {
                FechaInicio = ofertaForCreate.FechaInicio,
                FechaFinal = ofertaForCreate.FechaFinal,
                MetodoPago = (Models.TiposMetodoPago)ofertaForCreate.MetodoPago,
                DirigidaA = (Models.TiposDirigidaOferta)ofertaForCreate.DirigidaA,
                FechaOferta = DateTime.Now,
                OfertaItems = new List<OfertaItem>(),
                ApplicationUser = appUser
            };

            foreach (var item in ofertaForCreate.OfertaItems)
            {
                var herramienta = herramientas.First(h => h.Id == item.HerramientaId);

                bool tieneOfertaActiva = herramienta.OfertaItems?
                    .Any(oi => oi.Oferta.FechaFinal >= DateTime.Today) ?? false;

                if (tieneOfertaActiva)
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} ya tiene una oferta puesta");
                    continue;
                }

                double precioOriginal = herramienta.Precio;
                double precioFinal = precioOriginal * (1.0 - ((double)item.Porcentaje / 100.0));

                oferta.OfertaItems.Add(new OfertaItem
                {
                    Herramienta = herramienta,
                    Porcentaje = item.Porcentaje,
                    PrecioFinal = precioFinal,
                    PrecioOriginal = precioOriginal
                });
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Oferta.Add(oferta);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Oferta creada exitosamente con ID: {oferta.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la oferta");
                ModelState.AddModelError("Oferta", "Error: Hubo un error guardando tu oferta, prueba más tarde");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var ofertaItemsDTO = oferta.OfertaItems.Select(oi =>
            {
                var herramienta = herramientas.First(h => h.Id == oi.Herramienta.Id);
                return new OfertaItemDTO(
                    oi.Herramienta.Id,
                    oi.Porcentaje,
                    oi.PrecioOriginal,
                    oi.PrecioFinal
                );
            }).ToList();
            var ofertaDetail = new OfertaDetailDTO(
                oferta.FechaInicio,
                oferta.FechaFinal,
                (TiposMetodoPago)oferta.MetodoPago,
                ofertaItemsDTO,
                oferta.Id,
                (TiposDirigidaOferta)oferta.DirigidaA,
                oferta.ApplicationUser.NombreCliente
            );

            return CreatedAtAction("GetOfertaDetallePorId", new { id = oferta.Id }, ofertaDetail);
        }


    }
}
