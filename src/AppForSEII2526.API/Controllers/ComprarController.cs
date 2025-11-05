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
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ComprarItemDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCompraDetalle(int id) //Devuelve todo lo relativo a Oferta para el paso 7
        {
            if (_context.CompraItem == null)
            {
                _logger.LogError("Error: no hay mas existencias de herramientas");
                return NotFound();
            }

            var compra = await _context.Comprar
            .Where(r => r.Id == id)
                .Include(r => r.CompraItems) //join table RentalItems
                   .ThenInclude(ri => ri.comprar) //then join table Movies
                                                  // .ThenInclude(movie => movie.Cp) //then join table Genre
            .Select(r => new ComprarDetailDTO(r.ApplicationUser.NombreCliente, r.ApplicationUser.ApellidoCliente, r.DireccionEnvio,
                   r.FechaCompra,
                   r.CompraItems
                       .Select(ri => new ComprarItemDTO(ri.cantidad, ri.descripcion,
                               ri.herramienta.Nombre, ri.herramienta.Material,
                               ri.precio)).ToList<ComprarItemDTO>(),
                r.PrecioTotal))
            .FirstOrDefaultAsync();

            return Ok(compra);
        }

            [HttpPost]
            [Route("[action]")]
            [ProducesResponseType(typeof(ComprarDetailDTO), (int)HttpStatusCode.Created)]
            [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
            public async Task<ActionResult> CreateCompra(ComprarForCreateDTO compraForCreate)
            {
                if (compraForCreate.ComprarItem == null || !compraForCreate.ComprarItem.Any())
                    ModelState.AddModelError("CreateCompra", "Error! debes comprar al menos una herramienta");

                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(au => au.UserName == compraForCreate.NombreCliente);
                if (user == null)
                    ModelState.AddModelError("ApplicationUser", "Error! UserName is not registered");

                if (!ModelState.IsValid)
                    return ValidationProblem(ModelState);

                // Inicializa la entidad Comprar
                var comprar = new Comprar(compraForCreate.NombreCliente, compraForCreate.ApellidoCliente, compraForCreate.DireccionEnvio,
                                          compraForCreate.MetodoPago, compraForCreate, compraForCreate.Email, compraForCreate.Telefono,
                                          new List<CompraItem>(), compraForCreate.comprarItem?.Descripcion ?? string.Empty,
                                          compraForCreate.comprarItem?.Cantidad ?? 0);

                comprar.PrecioTotal = 0m;
                comprar.ApplicationUser = user;

                // Traer las herramientas desde BD por Ids (precio fiable)
                var ids = compraForCreate.ComprarItem.Select(i => i.Id).ToList();
                var herramientas = await _context.Herramienta
                                                 .Where(h => ids.Contains(h.Id))
                                                 .ToListAsync();

                foreach (var item in compraForCreate.ComprarItem)
                {
                    var herramienta = herramientas.FirstOrDefault(h => h.Id == item.Id);
                    if (herramienta == null)
                    {
                        ModelState.AddModelError("ComprarItem", $"Error! la herramienta '{item.Nombre}' no está disponible");
                        continue;
                    }

                    // usar precio desde BD; Herramienta.Precio es double -> convertir a decimal
                    decimal precioUnitario = Convert.ToDecimal(herramienta.Precio);
                    int cantidad = item.Cantidad;
                    decimal subtotal = precioUnitario * cantidad;

                    // crear CompraItem (almacenar precio unitario en propiedad precio)
                    var compraItem = new CompraItem
                    {
                        cantidad = cantidad,
                        descripcion = item.Descripcion,
                        idHerramienta = herramienta.Id,
                        herramienta = herramienta,
                        precio = precioUnitario
                    };

                    comprar.CompraItems.Add(compraItem);
                    comprar.PrecioTotal += subtotal;
                }


                _context.Comprar.Add(comprar);


                // Mapear a DTO de respuesta
                var comprarDetail = new ComprarDetailDTO(compraForCreate.NombreCliente, compraForCreate.ApellidoCliente,
                                                                comprar.DireccionEnvio, comprar.FechaCompra,
                                                                compraForCreate.ComprarItem, comprar.PrecioTotal);



                return CreatedAtAction("GetCompra", new { id = comprar.Id }, comprarDetail);
            }


    }



}

