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
            _logger.LogInformation("ComprarController inicializado");
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<ComprarDetailDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetalleCompra(int id) //Devuelve todo lo relativo a Oferta para el paso 7
        {
            if (_context.CompraItem == null)
            {
                _logger.LogError("No se encontraron compras en la base de datos");
                return NotFound();
            }
            if (id != null && id < 0) return NotFound();

            var compra = await _context.Comprar
            .Where(r => r.Id == id)
            .Include(r => r.ApplicationUser)
                .Include(r => r.ComprarItem)
                   .ThenInclude(ri => ri.Herramienta)
                   .ThenInclude(h => h.Fabricante)
            .Select(r => new ComprarDetailDTO(r.ApplicationUser.NombreCliente, r.ApplicationUser.ApellidoCliente, r.DireccionEnvio,
                   r.FechaCompra,
                   r.PrecioTotal, r.ComprarItem
                       .Select(ri => new ComprarItemDTO(ri.Cantidad, ri.Descripcion,
                               ri.Herramienta.Nombre, ri.Herramienta.Material,
                               ri.Precio)).ToList<ComprarItemDTO>()
                ))
            .FirstOrDefaultAsync();
            if (compra == null)
            {
                _logger.LogError("No se encontraron detalles de compra para el ID proporcionado: {Id}", id);
                return NotFound();
            }
            _logger.LogInformation("Detalles de compra recuperados exitosamente para el ID: {Id}", id);
            return Ok(new List<ComprarDetailDTO> { compra });
        }


        [HttpPost]
        [Route("/api/Compra/CrearCompra")]
        [ProducesResponseType(typeof(ComprarDetailDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]

        public async Task<ActionResult> CreacionCompra(ComprarForCreateDTO creaciondecompras)
        {


            if (creaciondecompras.ComprarItem.Count == 0)
            {
                ModelState.AddModelError("No herramienta", "Error! debes comprar al menos una herramienta");
            }

            if (string.IsNullOrEmpty(creaciondecompras.NombreCliente))
            {
                ModelState.AddModelError("Nombre", "El nombre no puede estar vacío");
            }

            if (string.IsNullOrEmpty(creaciondecompras.ApellidoCliente))
            {
                ModelState.AddModelError("Apellido", "El apellido no puede estar vacío");
            }

            if (string.IsNullOrEmpty(creaciondecompras.Direccion))
            {
                ModelState.AddModelError("Dirección de envio", "La dirección no puede estar vacía");
            }



            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            var usuario = _context.ApplicationUsers.FirstOrDefault(u => u.NombreCliente == creaciondecompras.NombreCliente && u.ApellidoCliente == creaciondecompras.ApellidoCliente);
            if (usuario == null)
            {
                ModelState.AddModelError("Usuario", "El usuario no existe.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var herramientasnombres = creaciondecompras.ComprarItem.Select(n => n.Nombre).ToList<string>();

            var herramientas = _context.Herramienta
                .Where(h => herramientasnombres.Contains(h.Nombre))
                .ToList();

            Comprar compra = new Comprar
            {
                DireccionEnvio = creaciondecompras.Direccion,
                FechaCompra = DateTime.Now,
                MetodoPago = creaciondecompras.TiposMetodoPago,
                ApplicationUser = usuario,
                ComprarItem = new List<CompraItem>()
            };

            compra.PrecioTotal = 0;


            foreach (var item in creaciondecompras.ComprarItem)
            {

                if (string.IsNullOrEmpty(item.Descripcion) && item.Cantidad == 3)
                {
                    ModelState.AddModelError("Descripción y Cantidad", "¡Error! Estás comprando demasiadas herramientas sin descripción");
                }

                if (ModelState.ErrorCount > 0)
                    return BadRequest(new ValidationProblemDetails(ModelState));

                if (item.Cantidad <= 0)
                {
                    ModelState.AddModelError("Cantidad", "La cantidad debe ser mayor que cero.");
                }
                if (string.IsNullOrEmpty(item.Descripcion))
                {
                    ModelState.AddModelError("Descripción", "Debe contener descripcion");
                }



                if (ModelState.ErrorCount > 0)
                    return BadRequest(new ValidationProblemDetails(ModelState));

                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramienta", $"'{item.Nombre}' no existe.");

                }
                else
                {
                    compra.ComprarItem.Add(new CompraItem
                    {
                        HerramientaId = herramienta.Id,
                        Cantidad = item.Cantidad,
                        Descripcion = item.Descripcion,
                        Precio = (decimal)(herramienta.Precio * item.Cantidad),
                        Herramienta = herramienta,
                        Comprar = compra
                    });
                }
            }
            compra.PrecioTotal = compra.ComprarItem.Sum(ci => ci.Precio);


            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Comprar.Add(compra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Error al guardar la compra", "Ocurrió un error al guardar la compra en la base de datos.");
                return Conflict("Error" + ex.Message);
            }

            var detallesCompra = new ComprarDetailDTO(
                compra.Id,
                compra.ApplicationUser.NombreCliente,
                compra.ApplicationUser.ApellidoCliente,
                compra.DireccionEnvio,
                compra.FechaCompra,
                compra.PrecioTotal,
                compra.ComprarItem
                    .Select(h => new ComprarItemDTO(
                        h.Cantidad,
                        h.Descripcion,            
                        h.Herramienta.Nombre,
                        h.Herramienta.Material,   
                        h.Precio))
                    .ToList()

            );

            return CreatedAtAction("GetDetalleCompra", new { id = compra.Id }, detallesCompra);

        }

    }

}