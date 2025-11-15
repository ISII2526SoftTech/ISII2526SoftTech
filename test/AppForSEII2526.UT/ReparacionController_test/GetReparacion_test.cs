using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.OfertaDTOs;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionController_test
{
    public class GetReparacion_test : AppForSEII25264SqliteUT{
        public GetReparacion_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("BOSCH"),
                new Fabricante("BISERMA"),
                new Fabricante("MANOLO94")
            };
            _context.Fabricante.AddRange(fabricantes);
            _context.SaveChanges();
            var herramienta1 = new Herramienta
            {
                Nombre = "Llave inglesa",
                Fabricante = fabricantes[0],
                Precio = 100,
                Material = "metal",
                TiempoReparacion = "7"
            };
            var herramienta2 = new Herramienta
            {
                Nombre = "Martillo de uña",
                Fabricante = fabricantes[1],
                Precio = 150,
                Material = "Acero",
                TiempoReparacion = "5"
            };
            _context.Herramienta.Add(herramienta1);
            _context.Herramienta.Add(herramienta2);
            _context.SaveChanges();

            //Crear usuario                                        id  nombre   apellido        email              telefono
            ApplicationUser applicationUser = new ApplicationUser("1", "Antonio", "Recio", "Mayorista@gmail.com", "676 76 76 76");
            var reparacion = new Reparacion
            {
                ApplicationUser = applicationUser,
                FechaEntrega = DateTime.Now.AddDays(1).AtMidnight(),
                FechaRecogida = DateTime.Now.AddDays(8).AtMidnight(),
                metodoPago = TiposMetodoPago.Efectivo,

            };
            _context.Reparacion.Add(reparacion);


            var reparacionItem1 = new ReparacionItem
            {
                ReparacionId = reparacion.Id,
                Cantidad = 2,
                Descripcion = "Reparacion de llave inglesa",
                Precio = (float)herramienta1.Precio,
                Reparacion = reparacion,
                Herramienta = herramienta1

            };
            var reparacionItem2 = new ReparacionItem
            {
                ReparacionId = reparacion.Id,
                Cantidad = 4,
                Descripcion = "Reparacion de Martillo de uña",
                Precio = (float)herramienta2.Precio,
                Reparacion = reparacion,
                Herramienta = herramienta2
            };

            //PRECIO TOTAL DE LAS REPARACIONES
            reparacion.PrecioTotal = (reparacionItem1.Precio * reparacionItem1.Cantidad) + (reparacionItem2.Precio * reparacionItem2.Cantidad);         
            _context.ReparacionItem.AddRange(new List<ReparacionItem> {reparacionItem1,reparacionItem2 });
            _context.SaveChanges();

        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReparacionDetallePorId_NoEncontrado_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;

            var controller = new ReparacionController(_context, logger);
            // Act
            var result = await controller.GetMostrarReparacionPorId(10);
            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetReparacionDetallePorId_Encontrado_test()
        {
            // Arrange
            var controller = new ReparacionController(_context,null);
            var reparacionitems = new List<ReparacionItemDTO>
            {
                new ReparacionItemDTO(1,100,"Reparacion de llave inglesa",2),
                new ReparacionItemDTO(2,150,"Reparacion de Martillo de uña",4)
            };
            ApplicationUser applicationUser = new ApplicationUser("1", "Antonio", "Recio", "antoniorecio@mayorista.com", "699 67 41 89");
            var expectedReparacion = new ReparacionDetailDTO(
                1,
                applicationUser.NombreCliente,
                applicationUser.ApellidoCliente,
                applicationUser.Telefono,
                DateTime.Now.AddDays(1).AtMidnight(),
                DateTime.Now.AddDays(8).AtMidnight(),       
                800,
                TiposMetodoPago.Efectivo,
                reparacionitems
                );

            // Act
            var result = await controller.GetMostrarReparacionPorId(1);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualReparacion = Assert.IsType<ReparacionDetailDTO>(okResult.Value);
            var eq = expectedReparacion.Equals(actualReparacion);
            Assert.Equal(expectedReparacion, actualReparacion);


        }
    }
}
