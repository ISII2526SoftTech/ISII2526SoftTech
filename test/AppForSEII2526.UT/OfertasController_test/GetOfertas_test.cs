using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.OfertasController_test
{
    public class GetOfertas_test : AppForSEII25264SqliteUT
    {
        public GetOfertas_test()
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

            _context.Herramienta.Add(herramienta1);
            _context.Herramienta.Add(herramienta2);
            _context.SaveChanges();
            var oferta = new Oferta
            {
                FechaInicio = DateTime.Now.AddDays(7),
                FechaFinal = DateTime.Now.AddDays(2),
                FechaOferta = DateTime.Now,
                MetodoPago = TiposMetodoPago.TarjetaCredito,
                DirigidaA = TiposDirigidaOferta.Socios
            };

            _context.Oferta.Add(oferta);
            _context.SaveChanges();
            var ofertaItem1 = new OfertaItem
            {
                OfertaId = oferta.Id,
                HerramientaId = herramienta1.Id,
                Porcentaje = 50,
                PrecioFinal = 50,
                PrecioOriginal = 100
            };

            var ofertaItem2 = new OfertaItem
            {
                OfertaId = oferta.Id,
                HerramientaId = herramienta2.Id,
                Porcentaje = 50,
                PrecioFinal = 75,
                PrecioOriginal = 150
            };
            _context.OfertaItem.AddRange(new List<OfertaItem> { ofertaItem1, ofertaItem2 });
            _context.SaveChanges();   
        }


        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetOfertaDetallePorId_NoEncontrado_test()
        {
            // Arrange
            var mock = new Mock<ILogger<OfertasController>>();
            ILogger<OfertasController> logger = mock.Object;

            var controller = new OfertasController(_context, logger);
            // Act
            var result = await controller.GetOfertaDetallePorId(10);
            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetOfertaDetallePorId_Encontrado_test()
        {
            var fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(7);
            var fechaFinal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddDays(2);
            var controller = new OfertasController(_context, null);
            var ofertaItems = new List<OfertaItemDTO>()
            {
                new OfertaItemDTO(1, 50, 100, 50),
                new OfertaItemDTO(2, 50, 150, 75)
            };
            var expectedOfertas = new OfertaDetailDTO(fechaInicio, fechaFinal, TiposMetodoPago.TarjetaCredito,ofertaItems,1, TiposDirigidaOferta.Socios);
            
            // Act
            var result = await controller.GetOfertaDetallePorId(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualOferta = Assert.IsType<OfertaDetailDTO>(okResult.Value);
            var eq = expectedOfertas.Equals(actualOferta);
            Assert.Equal(expectedOfertas, actualOferta);

        }
    }
    }
