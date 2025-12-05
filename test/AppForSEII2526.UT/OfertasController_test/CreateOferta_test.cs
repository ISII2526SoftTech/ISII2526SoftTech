using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using Humanizer;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class CreateOferta_test : AppForSEII25264SqliteUT
    {
        public CreateOferta_test()
        {
            
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Arcos"),
                new Fabricante("FABRICANTE2"),
                new Fabricante("Man")
            };

            _context.Fabricante.AddRange(fabricantes);
            _context.SaveChanges();

            var herramienta1 = new Herramienta
            {
                Nombre = "Taladro",
                Fabricante = fabricantes[0],
                Precio = 100,
                Material = "metal",
                TiempoReparacion = "1 semana"
            };

            var herramienta2 = new Herramienta
            {
                Nombre = "Sierra",
                Fabricante = fabricantes[1],
                Precio = 150,
                Material = "Acero",
                TiempoReparacion = "1 semana"
            };
            var herramienta3 = new Herramienta
            {
                Nombre = "Martillo",
                Fabricante = fabricantes[2],
                Precio = 80,
                Material = "Hierro",
                TiempoReparacion = "1 semana"
            };
            _context.Herramienta.Add(herramienta1);
            _context.Herramienta.Add(herramienta2);
            _context.SaveChanges();
            
            ApplicationUser applicationUser = new ApplicationUser("7", "Pepe", "Villuela", "pepe@gmail.com", "696969696");
            var oferta = new Oferta
            {
                FechaInicio = DateTime.Now.AddDays(7),
                FechaFinal = DateTime.Now.AddDays(2),
                FechaOferta = DateTime.Now,
                MetodoPago = TiposMetodoPago.TarjetaCredito,
                DirigidaA = TiposDirigidaOferta.Socios,
                ApplicationUser = applicationUser
            };

            _context.Oferta.Add(oferta);
            _context.SaveChanges();
            var ofertaItem1 = new OfertaItem
            {
                OfertaId = oferta.Id,
                Herramienta = herramienta1,
                Porcentaje = 50,
                PrecioFinal = 50,
                PrecioOriginal = 100
            };

            var ofertaItem2 = new OfertaItem
            {
                OfertaId = oferta.Id,
                Herramienta = herramienta2,
                Porcentaje = 50,
                PrecioFinal = 75,
                PrecioOriginal = 150
            };
            var ofertaItem3 = new OfertaItem
            {
                OfertaId = oferta.Id,
                Herramienta = herramienta3,
                Porcentaje = 25,
                PrecioFinal = 60,
                PrecioOriginal = 80
            };
            _context.OfertaItem.AddRange(new List<OfertaItem> { ofertaItem3 });
            _context.SaveChanges();
            


        }
        
        public static IEnumerable<object[]> CreateOferta_TestData()
        {
            var ofertaSinItem = new OfertaDetailDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>(), 1, TiposDirigidaOferta.Socios, "Pepe");

            var ofertaConFechaIncorrecta1 = new OfertaForCreateDTO(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");

            var ofertaConFechaIncorrecta2 = new OfertaForCreateDTO(DateTime.Now.AddDays(7), DateTime.Now.AddDays(2), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");
            var ofertaConItemMalPorcentaje = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 1250, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");
            var ofertaConItemConOfertaActiva = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(3, 50, 100, 50),
                },
                TiposDirigidaOferta.Socios, "Pepe");
            var ofertaConItemNoExistente = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(999, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");
            var ofertaConFechaFinalMuyPronta = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");

            var ofertaSinMetodoPago = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), 
                (TiposMetodoPago)243,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios, "Pepe");

            var todosLosTest = new List<object[]>
                        {
                            new object[] {ofertaSinItem, "Debe incluir al menos una herramienta en la oferta" },
                            new object[] {ofertaConFechaIncorrecta1, "La fecha de inicio no puede ser anterior a hoy" },
                            new object[] {ofertaConFechaIncorrecta2, "La fecha de fin debe ser posterior a la fecha de inicio" },
                            new object[] {ofertaConItemMalPorcentaje, "El porcentaje de rebaja debe estar entre 1 y 100" },
                            new object[] {ofertaConItemNoExistente, "La herramienta con ID 999 no existe" },
                            new object[] {ofertaConItemConOfertaActiva, "La herramienta con ID 3 ya tiene una oferta puesta" },
                            new object[] {ofertaConFechaFinalMuyPronta, "¡Error!, la oferta debe durar al menos una semana" },
                            new object[] {ofertaSinMetodoPago, "Falta un metodo de pago válido" },
                        };

            return todosLosTest;
        }


        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(CreateOferta_TestData))]
        public async Task CreateOferta_Error_test(OfertaForCreateDTO ofertaDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);

            // Act
            var result = await controller.CreateOferta(ofertaDTO);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];
            Assert.StartsWith(errorExpected, errorActual);

        }



        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateOferta_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);
            var ofertaItems = new List<OfertaItemDTO>()
            {
                new OfertaItemDTO(1, 50, 100, 50),
                new OfertaItemDTO(2, 50, 150, 75)
            };

            var expectedOfertaDTO = new OfertaForCreateDTO(DateTime.Now.AddDays(2).AtMidnight(), DateTime.Now.AddDays(11).AtMidnight(), TiposMetodoPago.TarjetaCredito, ofertaItems, TiposDirigidaOferta.Socios, "Pepe");

            var ofertaDTO = new OfertaDetailDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(11), TiposMetodoPago.TarjetaCredito, ofertaItems,2,TiposDirigidaOferta.Socios, "Pepe");
            // Act
            var result = await controller.CreateOferta(ofertaDTO);
            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<OfertaDetailDTO>(createdResult.Value);
            
            Assert.Equal(expectedOfertaDTO, actualRentalDetailDTO);

        }

    }

    
}
