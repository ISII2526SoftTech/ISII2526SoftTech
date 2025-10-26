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
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMostrarOfertaPorId(int id)
        {
            if (_context.Oferta == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }

            var oferta = await _context.Oferta
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
        public async Task<ActionResult> GetOfertaDetalle() //Devuelve todo lo relativo a Oferta para el paso 7
        {
            if (_context.Oferta == null)
            {
                _logger.LogError("Error: no existen ofertas");
                return NotFound();
            }

            var oferta = await _context.Oferta
                .Select(o => new OfertaDetailDTO(
                    o.FechaInicio,
                    o.FechaFinal,
                    (TiposMetodoPago)o.MetodoPago,
                    o.OfertaItems.Select(oi => new OfertaItemDTO(
                        oi.HerramientaId,
                        oi.Porcentaje,
                        oi.PrecioOriginal,
                        oi.PrecioFinal
                    )).ToList<OfertaItemDTO>(),
                    o.Id,
                    (TiposDirigidaOferta)o.DirigidaA

                ))
                .ToListAsync();
            return Ok(oferta);
        }
       
        [HttpPost]
        [Route("[action]")]//Crear oferta para el paso 5
        [ProducesResponseType(typeof(OfertaDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateOferta(OfertaForCreateDTO ofertaForCreate)
        {

            if (ofertaForCreate.FechaInicio < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "La fecha de inicio no puede ser anterior a hoy");

            if (ofertaForCreate.FechaFinal <= ofertaForCreate.FechaInicio)
                ModelState.AddModelError("FechaFinal", "La fecha de fin debe ser posterior a la fecha de inicio");

            if (ofertaForCreate.OfertaItems == null || ofertaForCreate.OfertaItems.Count == 0)
                ModelState.AddModelError("Items", "Debe incluir al menos una herramienta en la oferta");

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
            var herramientas = await _context.Herramienta
                .Include(h => h.OfertaItems)
                    .ThenInclude(oi => oi.Oferta)
                .Where(h => herramientaIds.Contains(h.Id))
                .ToListAsync();

            Oferta oferta = new Oferta
            {
                FechaInicio = ofertaForCreate.FechaInicio,
                FechaFinal = ofertaForCreate.FechaFinal,
                MetodoPago = (Models.TiposMetodoPago)ofertaForCreate.MetodoPago,
                DirigidaA = (Models.TiposDirigidaOferta)ofertaForCreate.DirigidaA,
                FechaOferta = DateTime.Now,
                OfertaItems = new List<OfertaItem>()
            };
            _context.Oferta.Add(oferta);

            try
            {
                var cambios = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
            foreach (var item in ofertaForCreate.OfertaItems)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Id == item.HerramientaId);
                if (herramienta == null)
                {
                    _logger.LogWarning($"Herramienta {item.HerramientaId} no encontrada");
                    continue;
                }

                bool tieneOfertaActiva = await _context.OfertaItem
                    .Include(oi => oi.Oferta)
                    .Where(oi => oi.HerramientaId == herramienta.Id)
                    .AnyAsync();

                if (tieneOfertaActiva)
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} ya tiene una oferta activa.");
                    continue;
                }

                double precioOriginal = herramienta.Precio;
                double precioFinal = precioOriginal * (1.0 - ((double)item.Porcentaje / 100.0));

                var sql = "INSERT INTO OfertaItem (OfertaId, HerramientaId, Porcentaje, PrecioFinal, PrecioOriginal) VALUES ({0}, {1}, {2}, {3}, {4})";
                var parametros = new object[] { oferta.Id, herramienta.Id, item.Porcentaje, precioFinal, precioOriginal };

                var filasAfectadas = await _context.Database.ExecuteSqlRawAsync(sql, parametros);
            }

            var ofertaConItems = await _context.Oferta
                .Include(o => o.OfertaItems)
                .FirstOrDefaultAsync(o => o.Id == oferta.Id);

            if (ofertaConItems != null)
            {
                var itemsEnBD = await _context.OfertaItem
                    .Where(oi => oi.OfertaId == oferta.Id)
                    .ToListAsync();
            }

            var ofertaItemsDTO = ofertaConItems?.OfertaItems?.Select(oi => new OfertaItemDTO(
                oi.HerramientaId,
                oi.Porcentaje,
                oi.PrecioOriginal,
                oi.PrecioFinal
            )).ToList() ?? new List<OfertaItemDTO>();

            var ofertaDetail = new OfertaDetailDTO(
                oferta.FechaInicio,
                oferta.FechaFinal,
                (TiposMetodoPago)oferta.MetodoPago,
                ofertaItemsDTO,
                oferta.Id,
                (TiposDirigidaOferta)oferta.DirigidaA
            );

            return CreatedAtAction("GetMostrarOfertaPorId", new { id = oferta.Id }, ofertaDetail);
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
               ModelState.AddModelError("FechaFinal", "La fecha de fin debe ser posterior a la fecha de inicio");

           if (ofertaForCreate.OfertaItems == null || ofertaForCreate.OfertaItems.Count == 0)
               ModelState.AddModelError("Items", "Debe incluir al menos una herramienta en la oferta");

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
               OfertaItems = new List<OfertaItem>()
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

               _logger.LogInformation($"Creando oferta para herramienta {herramienta.Id}: " +
                                     $"Precio original: {precioOriginal}, " +
                                     $"Porcentaje: {item.Porcentaje}%, " +
                                     $"Precio final: {precioFinal}");

               oferta.OfertaItems.Add(new OfertaItem
               {
                   HerramientaId = herramienta.Id,
                   Porcentaje = item.Porcentaje,
                   PrecioFinal = precioFinal
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
               var herramienta = herramientas.First(h => h.Id == oi.HerramientaId);
               return new OfertaItemDTO(
                   oi.HerramientaId,
                   oi.Porcentaje,
                   herramienta.Precio,
                   oi.PrecioFinal
               );
           }).ToList();
           foreach (var item in ofertaItemsDTO)
           {
               _logger.LogInformation($"Item DTO - HerramientaId: {item.HerramientaId}, Porcentaje: {item.Porcentaje}, Precio: {item.PrecioFinal}");
           }
           var ofertaDetail = new OfertaDetailDTO(
               oferta.FechaInicio,
               oferta.FechaFinal,
               (TiposMetodoPago)oferta.MetodoPago,
               ofertaItemsDTO,
               oferta.Id,
               (TiposDirigidaOferta)oferta.DirigidaA
           );

           _logger.LogInformation($"Items en OfertaDetailDTO: {ofertaDetail.OfertaItems?.Count}");
           _logger.LogInformation($"PrecioTotalOriginal: {ofertaDetail.PrecioTotalOriginal?.Count}");
           _logger.LogInformation($"PrecioTotalConOferta: {ofertaDetail.PrecioTotalConOferta?.Count}");

           if (ofertaDetail.OfertaItems == null)
           {
               _logger.LogWarning("OfertaItems es NULL en el DTO!");
           }
           return CreatedAtAction("GetMostrarOfertaPorId", new { id = oferta.Id }, ofertaDetail);
       }

       */
    }
}
