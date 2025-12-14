using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.ComprarDTOs;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprarController_test
{
    public class GetDetalleCompra_test : AppForSEII25264SqliteUT
    {
        public GetDetalleCompra_test()
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

            var compra = new Comprar("calle mayor", DateTime.Today, new List<CompraItem>(), 31.5m, TiposMetodoPago.TarjetaCredito, usuario);
            compra.ComprarItem.Add(new CompraItem(2, "", compra, herramientas[1], (decimal)herramientas[1].Precio));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();

        }

        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetalleCompra_NotFound_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;

            var controller = new ComprarController(_context, logger);

            //Act (Se ejecuta la acción a testear)
            var result = await controller.GetDetalleCompra(-1);

            //Assert (Se comprueba que el resultado es el esperado)
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetalleCompra_Found_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock = new Mock<ILogger<ComprarController>>();
            ILogger<ComprarController> logger = mock.Object;

            var controller = new ComprarController(_context, logger);

            var expectedCompra = new ComprarDetailDTO("Sergio", "Sanchez", "calle mayor", DateTime.Today, 31.5m, new List<ComprarItemDTO>());
            expectedCompra.ComprarItem.Add(new ComprarItemDTO(2, "", "Sierra", "Acero", 150));

            //Act
            var result = await controller.GetDetalleCompra(1);

            //Assert

            var Okresult = Assert.IsType<OkObjectResult>(result);
            // Ahora esperamos una lista de ComprarDetailDTO
            var detallesList = Assert.IsType<List<ComprarDetailDTO>>(Okresult.Value);

            Assert.Single(detallesList);
            var detallesCompraDTO = detallesList.First();

            Assert.Equal(expectedCompra, detallesCompraDTO);
        }
    }
}