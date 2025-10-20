using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.OfertaDTOs;
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
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOferta()
        {
            if (_context.Ofertas == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .Select(o => new OfertaDetailDTO(
                    o.FechaInicio,
                    o.FechaFinal,
                    (TiposMetodoPago)o.MetodoPago,
                    o.OfertaItems.Select(oi => new OfertaItemDTO(
                        oi.IdHerramienta,
                        oi.Porcentaje,
                        oi.PrecioFinal
                    )).ToList<OfertaItemDTO>(),
                    o.Id,
                    (TiposDirigidaOferta)o.DirigidaA

                ))
                .ToListAsync();
            return Ok(oferta);
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOfertaDetalle(int id)
        {
            if (_context.Ofertas == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .Where(o => o.Id == id)
                .Select(o => new OfertaDetailDTO(
                    o.FechaInicio,
                    o.FechaFinal,
                    (TiposMetodoPago)o.MetodoPago,
                    o.Id,
                    (TiposDirigidaOferta)o.DirigidaA
                ))
                .FirstOrDefaultAsync();

            if (oferta == null)
            {
                _logger.LogError($"Error: La oferta {id} no existe");
                return NotFound();
            }

            return Ok(oferta);
        }
        /*
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateOferta(OfertaForCreateDTO ofertaForCreate)
        {

            if (ofertaForCreate.FechaInicio < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy");

            if (ofertaForCreate.FechaFinal <= ofertaForCreate.FechaInicio)
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser posterior a la fecha de inicio");

            if (ofertaForCreate.Items == null || ofertaForCreate.Items.Count == 0)
                ModelState.AddModelError("Items", "Debe incluir al menos una herramienta en la oferta");

            if (ofertaForCreate.Items != null)
            {
                foreach (var item in ofertaForCreate.Items)
                {
                    if (item.PorcentajeRebaja <= 0 || item.PorcentajeRebaja > 100)
                        ModelState.AddModelError("PorcentajeRebaja", $"El porcentaje de rebaja para {item.Herramienta?.Nombre} debe estar entre 1 y 100");
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var herramientaIds = ofertaForCreate.Items.Select(oi => oi.Herramienta.Id).ToList();
            var herramientas = await _context.Herramientas
                .Where(h => herramientaIds.Contains(h.Id))
                .ToDictionaryAsync(h => h.Id);

            foreach (var item in ofertaForCreate.Items)
            {
                if (!herramientas.ContainsKey(item.Herramienta.Id))
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.Herramienta.Id} no existe");
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var oferta = new Oferta
            {
                FechaInicio = ofertaForCreate.FechaInicio,
                FechaFinal = ofertaForCreate.FechaFinal,
                MetodoPago = (Models.TiposMetodoPago)ofertaForCreate.MetodoPago,
                DirigidaA = (Models.TiposDirigidaOferta)ofertaForCreate.DirigidaA,
                OfertaItems = new List<OfertaItem>()
            };

    
            foreach (var item in ofertaForCreate.Items)
            {
                var herramienta = herramientas[item.Herramienta.Id];
                oferta.OfertaItems.Add(new OfertaItem
                {
                    IdHerramienta = herramienta.Id,
                    Porcentaje = item.PorcentajeRebaja,
                    PrecioFinal = (herramienta.Precio * (1.0 - ((double)item.PorcentajeRebaja / 100.0)))
                });
            }

            _context.Ofertas.Add(oferta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la oferta");
                ModelState.AddModelError("Oferta", "Hubo un problema al guardar la oferta, por favor intente más tarde");
                return Conflict($"Error: {ex.Message}");
            }
            var ofertaDetail = new OfertaDetailDTO(
                oferta.FechaInicio,
                oferta.FechaFinal,
                ofertaForCreate.MetodoPago,
                ofertaForCreate.Items,
                oferta.Id
            );

            return CreatedAtAction("GetOferta", new { id = oferta.Id }, ofertaDetail);
        }

        
        */

    }
    }
