using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using AppForSEII2526.API.DTOs.ReparacionDTOs;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionController_test
{
    public class CreateReparacion_test : AppForSEII25264SqliteUT
    {
        public CreateReparacion_test()
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
            var herramienta3 = new Herramienta
            {
                Nombre = "Destornillador",
                Fabricante = fabricantes[2],
                Precio = 80,
                Material = "Hierro",
                TiempoReparacion = "3"
            };
            _context.Herramienta.Add(herramienta1);
            _context.Herramienta.Add(herramienta2);
            _context.Herramienta.Add(herramienta3);
            _context.SaveChanges();
            //Crear usuario                                        id  nombre   apellido        email                telefono
            ApplicationUser applicationUser = new ApplicationUser("1", "Antonio", "Recio", "Mayorista@gmail.com", "+34 676 76 76 76");
            var reparacion = new Reparacion
            {
                ApplicationUser = applicationUser,
                FechaEntrega = DateTime.Now.AddDays(1).AtMidnight(),
                FechaRecogida = DateTime.Now.AddDays(8).AtMidnight(),
                metodoPago = TiposMetodoPago.Efectivo,

            };
            _context.Reparacion.Add(reparacion);
            _context.SaveChanges();
            var reparacionItem1 = new ReparacionItem
            {
                ReparacionId = reparacion.Id,
                Cantidad = 2,
                Descripcion = "Arreglo de la llave inglesa",
                Herramienta = herramienta1,
                PrecioUnitario = 50
            };
            var reparacionItem2 = new ReparacionItem
            {
                ReparacionId = reparacion.Id,
                Cantidad = 1,
                Descripcion = "Arreglo del martillo de uña",
                Herramienta = herramienta2,
                PrecioUnitario = 70
            };
            var reparacionItem3 = new ReparacionItem
            {
                ReparacionId = reparacion.Id,
                Cantidad = 3,
                Descripcion = "Arreglo del destornillador",
                Herramienta = herramienta3,
                PrecioUnitario = 30
            };
            //PRECIO TOTAL DE LAS REPARACIONES
            reparacion.PrecioTotal = (reparacionItem1.PrecioUnitario * reparacionItem1.Cantidad) + (reparacionItem2.PrecioUnitario * reparacionItem2.Cantidad) + (reparacionItem3.PrecioUnitario * reparacionItem3.Cantidad);
            _context.ReparacionItem.AddRange(new List<ReparacionItem> { reparacionItem1, reparacionItem2, reparacionItem3 });
            _context.SaveChanges();


        }

        // CASOS DE ERROR PARA CREAR REPARACION
        public static IEnumerable<object[]> CreateReparacion_TestData()
        {
            var reparacionListPrueba = new List<ReparacionItemDTO>()
            {                   // idHerramienta, precio, descripcion, cantidad
                new ReparacionItemDTO(1, 50, "Arreglo de la llave inglesa", 2),
                new ReparacionItemDTO(2, 70, "Arreglo del martillo de uña", 1)
            };

            var reparacionConUsuarioNoexistente = new ReparacionForCreateDTO("Enrique", "Pastor", DateTime.Now, TiposMetodoPago.Efectivo, null, reparacionListPrueba);

            // EXAMEN IS2
            var reparacionConFormatoNumeroIncorrecto = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(2), TiposMetodoPago.Efectivo, "676 69 67 41", reparacionListPrueba);

            var reparacionConFechaIncorrecta = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(-2), TiposMetodoPago.Efectivo, "+34 676 69 67 41", reparacionListPrueba);

            var reparacionSinItem = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(2), TiposMetodoPago.Efectivo, "+34 676 69 67 41", new List<ReparacionItemDTO>());

            var reparacionConHerramientaNoExistente = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(2), TiposMetodoPago.Efectivo, "+34 676 69 67 41",
                new List<ReparacionItemDTO>()
                {
                    new ReparacionItemDTO(999, 50, "Arreglo de la llave inglesa", 2),
                    new ReparacionItemDTO(89, 70, "Arreglo del martillo de uña", 1)
                });

            var todosLosTest = new List<object[]>
                        {
                            new object[] {reparacionConUsuarioNoexistente, "El Usuario Enrique Pastor no existe." },
                            new object[] {reparacionConFormatoNumeroIncorrecto,"Error! el numero de telefono a de tener el prefijo +34" },
                            new object[] {reparacionConFechaIncorrecta, "La fecha de entrega no puede ser anterior a hoy" },
                            new object[] {reparacionSinItem, "Error! debes reparar al menos una herramienta" },
                            new object[] {reparacionConHerramientaNoExistente, "La herramienta con ID 999 no existe" }
                        };
            return todosLosTest;

        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(CreateReparacion_TestData))]
        public async Task PostParaReparacion_CrearReparacion_Test(ReparacionForCreateDTO reparacionDTO, string mensajeErroneo)
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionController>>();
            var controller = new ReparacionController(_context, mock.Object);

            // Act
            var result = await controller.CreateReparacion(reparacionDTO);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var details = Assert.IsType<ValidationProblemDetails>(badRequest.Value);

            var error = details.Errors.First().Value[0];
            Assert.StartsWith(mensajeErroneo, error);
        }


        // CASOS EXITOSOS PARA CREAR REPARACION
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateReparacion_Exitosos()
        {
            var mock = new Mock<ILogger<ReparacionController>>();
            ILogger<ReparacionController> logger = mock.Object;
            
            var controller = new ReparacionController(_context, logger);

            var reparacionBuenas = new List<ReparacionItemDTO>()
            {                   // idHerramienta, precio, descripcion, cantidad
                new ReparacionItemDTO(1, 50, "Arreglo de la llave inglesa", 2),
                new ReparacionItemDTO(2, 70, "Arreglo del martillo de uña", 1)
            };

            var expectedReparacion = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(3), TiposMetodoPago.Efectivo, "+34 676 69 67 41", reparacionBuenas);

            var reparacionDto = new ReparacionForCreateDTO("Antonio", "Recio", DateTime.Now.AddDays(3), TiposMetodoPago.Efectivo, "+34 676 69 67 41", reparacionBuenas);

            // Act
            var result = await controller.CreateReparacion(reparacionDto);

            // Assert
            var CreateResult = Assert.IsType<CreatedAtActionResult>(result);
            var reparacionCreada = Assert.IsType<ReparacionDetailDTO>(CreateResult.Value);
            Assert.Equal(expectedReparacion.NombreCliente, reparacionCreada.NombreCliente);



        }
    }
}
