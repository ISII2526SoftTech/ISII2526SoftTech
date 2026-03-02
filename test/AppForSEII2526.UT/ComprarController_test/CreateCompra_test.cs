using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.ComprarDTOs;
using AppForSEII2526.API.Models;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesCompra_test
{
    public class CreacionCompra_test : AppForSEII25264SqliteUT
    {
        public CreacionCompra_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Arcos"),
                new Fabricante("FABRICANTE2"),
                new Fabricante("Man")
            };

            var herramientas = new List<Herramienta> {
                new Herramienta("Taladro",fabricantes[0] , 100,"metal",null),
                new Herramienta("Sierra", fabricantes[1], 150, "Acero", null),
                new Herramienta("Martillo", fabricantes[2], 15, "Acero", null)

            };

            ApplicationUser usuario = new ApplicationUser("Sergio", "Sanchez", "gambon", "666666666");

            var compra = new Comprar("calle mayor", DateTime.Today, new List<CompraItem>(), 15m, TiposMetodoPago.TarjetaCredito, usuario);
            compra.ComprarItem.Add(new CompraItem(2, "", compra, herramientas[1], (decimal)herramientas[1].Precio));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> CasosPrueba_CreacionCompraDTOs()
        {
            var compra_sin_herramientas = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor",TiposMetodoPago.TarjetaCredito,null,null,new List<ComprarItemDTO>());

            var compra_sin_nombre = new ComprarForCreateDTO("", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_sin_nombre.ComprarItem.Add(new ComprarItemDTO(2,"", "Martillo", "Acero", 15m));

            var compra_sin_apellido = new ComprarForCreateDTO("Sergio", "", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_sin_apellido.ComprarItem.Add(new ComprarItemDTO(2, "", "Martillo", "Acero", 15m));

            var compra_sin_direccion = new ComprarForCreateDTO("Sergio", "Sanchez", "", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_sin_direccion.ComprarItem.Add(new ComprarItemDTO(2, "", "Martillo", "Acero", 15m));

            var compra_usuario_NF = new ComprarForCreateDTO("Billalcico", "Salami", "Calle Samsung", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_usuario_NF.ComprarItem.Add(new ComprarItemDTO(2,"","Taladro", "Metal", 25.50m));

            var compra_cantidad = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_cantidad.ComprarItem.Add(new ComprarItemDTO(0, "", "Martillo", "Acero", 15m));

            var compra_sin_descripcion = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_sin_descripcion.ComprarItem.Add(new ComprarItemDTO(2, "", "Martillo", "Acero", 15m));

            

            var compra_herramienta_erronea = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_herramienta_erronea.ComprarItem.Add(new ComprarItemDTO(2, "una tuerca", "Tuerca", "Hierro", 15m));

            var compra_herramienta_sindescripcion_mayorcantidad = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            compra_herramienta_sindescripcion_mayorcantidad.ComprarItem.Add(new ComprarItemDTO(3, "", "Martillo", "Acero", 15m));


            var allTest = new List<object[]>
            {
                new object[] {compra_sin_herramientas,"Error! debes comprar al menos una herramienta" },
                new object[] {compra_sin_nombre, "El nombre no puede estar vacío"},
                new object[] {compra_sin_apellido, "El apellido no puede estar vacío"},
                new object[] {compra_sin_direccion, "La dirección no puede estar vacía" },
                new object[] {compra_usuario_NF, "El usuario no existe." },
                new object[] {compra_cantidad, "La cantidad debe ser mayor que cero"},
                new object[] {compra_sin_descripcion, "Debe contener descripcion"},
                new object[] {compra_herramienta_erronea, $"'{compra_herramienta_erronea.ComprarItem[0].Nombre}' no existe" },
                new object[] {compra_herramienta_sindescripcion_mayorcantidad, "¡Error! Estás comprando demasiadas herramientas sin descripción" }
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(CasosPrueba_CreacionCompraDTOs))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task CreacionCompra_Test_BadRequest(ComprarForCreateDTO creacioncompra, string erroresperado)
        {
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;
            var controller = new ComprarController(_context, logger);

            var result = await controller.CreacionCompra(creacioncompra);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var detallesProblem = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = detallesProblem.Errors.First().Value[0];
            Assert.StartsWith(erroresperado, errorActual);
        }


        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task CreacionCompra_Test_OK()
        {
            // Arrange (Se define todas las variables que se necesitan)
            var controller = new ComprarController(_context, NullLogger<ComprarController>.Instance);

            var creacioncompra = new ComprarForCreateDTO("Sergio", "Sanchez", "calle mayor", TiposMetodoPago.TarjetaCredito, null, null, new List<ComprarItemDTO>());
            creacioncompra.ComprarItem.Add(new ComprarItemDTO(2, "Martillo calidad", "Martillo", "Acero", 15m));

            var expectedCompra = new ComprarDetailDTO("Sergio", "Sanchez", "calle mayor", DateTime.Today, 30m, new List<ComprarItemDTO>());
            expectedCompra.ComprarItem.Add(new ComprarItemDTO(2, "Martillo calidad", "Martillo", "Acero", 30m));

            //Act (Se ejecuta la acción a testear)
            var result = await controller.CreacionCompra(creacioncompra);

            //Assert (Se comprueba que el resultado es el esperado)
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var compraCreada = Assert.IsType<ComprarDetailDTO>(createdAtActionResult.Value);

            // Comparaciones por campo para evitar fallos por Id y hora
            Assert.Equal(expectedCompra.NombreCliente, compraCreada.NombreCliente);
            Assert.Equal(expectedCompra.ApellidoCLiente, compraCreada.ApellidoCLiente);
            Assert.Equal(expectedCompra.Direccion, compraCreada.Direccion);
            Assert.Equal(expectedCompra.PrecioTotal, compraCreada.PrecioTotal);

            Assert.Equal(expectedCompra.FechaCompra.Date, compraCreada.FechaCompra.Date);

            // Comparar lista de ítems (usar Equals implementado en ComprarItemDTO)
            Assert.Equal(expectedCompra.ComprarItem, compraCreada.ComprarItem);
        }
    }
}