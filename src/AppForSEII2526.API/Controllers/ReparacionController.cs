using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using AppForSEII2526.API.Models;

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
                  r.PrecioTotal,
                  r.metodoPago,
                  r.ReparacionItems.Select(ri => new ReparacionItemDTO(
                    ri.Herramienta.Id,
                    ri.Precio,
                    ri.Descripcion,
                    ri.Cantidad
                )).ToList()
                ))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogError($"Error: La reparacion {id} no existe");
                return NotFound();
            }

            return Ok(reparacion);

        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateReparacion(ReparacionForCreateDTO reparacionForCreate)
        {

            if (reparacionForCreate.FechaEntrega < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "La fecha de entrega no puede ser anterior a hoy");

            if (reparacionForCreate.reparacionItem == null || !reparacionForCreate.reparacionItem.Any())
                ModelState.AddModelError("CreateReparacion", "Error! debes reparar al menos una herramienta");

            // Buscar usuario
            var applicationUser = await _context.Users.FirstOrDefaultAsync(u => u.NombreCliente == reparacionForCreate.NombreCliente && u.ApellidoCliente == reparacionForCreate.ApellidoCliente);
            if (applicationUser == null)
                ModelState.AddModelError(nameof(reparacionForCreate.NombreCliente), $"El Usuario {reparacionForCreate.NombreCliente} {reparacionForCreate.ApellidoCliente} no existe.");

            //Si hay errores volver atras (Ahora esta comprobación incluirá el error de usuario no encontrado)
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            // Comprobar que las herramientas existen 
            var herramientaIds = reparacionForCreate.reparacionItem.Select(oi => oi.IdHerramienta).ToList();

            var herramientas = await _context.Herramienta
                .Include(h => h.ItemsReparacion)
                    .ThenInclude(oi => oi.Reparacion)
                .Where(h => herramientaIds.Contains(h.Id))
                .ToListAsync();

            foreach (var item in reparacionForCreate.reparacionItem)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Id == item.IdHerramienta);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.IdHerramienta} no existe");
                }
            }

            Reparacion reparacion = new Reparacion
            {
                FechaEntrega = reparacionForCreate.FechaEntrega,
                ReparacionItems = new List<ReparacionItem>(),
                metodoPago = (Models.TiposMetodoPago)reparacionForCreate.MetodoPago,               
                ApplicationUser = applicationUser!

            };

            float precioTotalCalculado = 0;
            //Verificar la existencia de cada herramienta
            foreach (var item in reparacionForCreate.reparacionItem)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Id == item.IdHerramienta);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.IdHerramienta} no existe");
                }
                else
                {
                   

                    double precioHerramienta = herramienta.Precio;
                    double precioFinal = precioHerramienta * item.Cantidad;
                    precioTotalCalculado += (float)precioFinal;

                    //reparacion.ReparacionItems.Add(new ReparacionItem
                    //{
                    //    ReparacionId = reparacion.Id,

                    //    Herramienta = herramienta,

                    //    Descripcion = item.Descripcion,

                    //    Cantidad = item.Cantidad,

                    //    Precio = (float)precioFinal
                    //});
                    // rental.RentalItems.Add(new RentalItem(movie.Id, rental, movie.PriceForRenting, item.Description));

                    reparacion.ReparacionItems.Add(new ReparacionItem(herramienta,herramienta.Id,reparacion, (float)precioFinal, item.Descripcion, item.Cantidad));

                }
            }
           // DateTime fechaRecogidaCalculada = reparacionForCreate.FechaEntrega.AddDays();
            reparacion.PrecioTotal = precioTotalCalculado;
            //Si hay errores volver atras
            if (ModelState.ErrorCount > 0)

                return BadRequest(new ValidationProblemDetails(ModelState));


            _context.Add(reparacion);
            var estado = _context.Entry(reparacion).State;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la nueva reparacion en la base de datos.");
                return Conflict($"Ocurrió un error al guardar la reparacion: {ex.Message}");
            }

            var reparacionItemsDTO = reparacion.ReparacionItems.Select(ri =>
            {
                var herramienta = herramientas.First(h => h.Id == ri.Herramienta.Id);
                return new ReparacionItemDTO(
                    ri.Herramienta.Id,
                    ri.Precio,
                    ri.Descripcion,
                    ri.Cantidad
                );
            }).ToList();

            var reparacionDetail = new ReparacionDetailDTO(
               reparacion.Id,
               reparacion.ApplicationUser.NombreCliente,
               reparacion.ApplicationUser.ApellidoCliente,
               reparacion.ApplicationUser.PhoneNumber,
               reparacion.FechaEntrega,
               reparacion.FechaRecogida,
               reparacion.PrecioTotal,
               reparacion.metodoPago,
               reparacionItemsDTO
            );

            return CreatedAtAction("GetMostrarReparacionPorId", new { id = reparacion.Id }, reparacionDetail);
        }

        


    }
}



