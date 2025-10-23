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
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOfertaDetalle()
        {

            var herramientas = await _context.Herramientas
                .Select(h => new HerramientaDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    h.Precio
                 ))
                .ToListAsync();

            return Ok(herramientas);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMostrarOferta(int id)
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
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMostrarTodasOfertas()
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
                        oi.HerramientaId,
                        oi.Porcentaje,
                        oi.PrecioFinal
                    )).ToList<OfertaItemDTO>(),
                    o.Id,
                    (TiposDirigidaOferta)o.DirigidaA

                ))
                .ToListAsync();
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
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy");

            if (ofertaForCreate.FechaFinal <= ofertaForCreate.FechaInicio)
                ModelState.AddModelError("FechaFinal", "La fecha de fin debe ser posterior a la fecha de inicio");

            if (ofertaForCreate.Items == null || ofertaForCreate.Items.Count == 0)
                ModelState.AddModelError("Items", "Debe incluir al menos una herramienta en la oferta");

            if (ofertaForCreate.Items != null)
            {
                foreach (var item in ofertaForCreate.Items)
                {
                    if (item.Porcentaje <= 0 || item.Porcentaje > 100)
                        ModelState.AddModelError("Porcentaje", $"El porcentaje de rebaja debe estar entre 1 y 100");
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));
            var herramientaIds = ofertaForCreate.Items.Select(oi => oi.HerramientaId).ToList();
            var herramientas = _context.Herramientas.Include(h => h.OfertaItems)
                .ThenInclude(oi => oi.Oferta)
                .Where(h => herramientaIds.Contains(h.Id))
                .Select(h => new
                {
                    Herramienta = h,
                    OfertasActivas = h.OfertaItems
                        .Where(oi => oi.Oferta.FechaFinal >= DateTime.Today)
                        .Select(oi => oi.Oferta)
                        .ToList()
                })
                .ToList();

            Oferta oferta = new Oferta
            {
                FechaInicio = ofertaForCreate.FechaInicio,
                FechaFinal = ofertaForCreate.FechaFinal,
                MetodoPago = (Models.TiposMetodoPago)ofertaForCreate.MetodoPago,
                DirigidaA = (Models.TiposDirigidaOferta)ofertaForCreate.DirigidaA,
                FechaOferta = DateTime.Now,
                OfertaItems = new List<OfertaItem>()
            };
            foreach (var item in ofertaForCreate.Items)
            {
                var herramientaData = herramientas.FirstOrDefault(h => h.Herramienta.Id.Equals(item.HerramientaId));
                if (herramientaData == null)
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} no existe");
                    continue;
                }
                if (herramientaData.OfertasActivas.Any())
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} ya tiene una oferta activa.");
                    continue;
                }
                
                var herramientaExistente = herramientaData.Herramienta;
                
                oferta.OfertaItems.Add(new OfertaItem
                {
                    HerramientaId = herramientaExistente.Id,
                    Porcentaje = item.Porcentaje,
                    PrecioFinal = herramientaExistente.Precio * (1.0 - ((double)item.Porcentaje / 100.0))
                });
                _logger.LogInformation($"Intentando crear OfertaItem para HerramientaId: {herramientaExistente.Id}");
            }
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Ofertas.Add(oferta);
        
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Oferta", $"Error Hubo un error guardando tu oferta, prueba mas tarde");
            }
            var ofertaItemsDTO = oferta.OfertaItems.Select(oi => new OfertaItemDTO
            {
                HerramientaId = oi.HerramientaId,  
                Porcentaje = oi.Porcentaje,
                Precio = oi.PrecioFinal
            }).ToList();

            var ofertaDetail = new OfertaDetailDTO(
                    oferta.FechaInicio,
                    oferta.FechaFinal,
                    oferta.MetodoPago,
                    ofertaItemsDTO,
                    oferta.Id,
                    (TiposDirigidaOferta)oferta.DirigidaA

                );
            return CreatedAtAction("GetMostrarOferta", new { id = oferta.Id }, ofertaDetail);

            
        }
    }
    }
