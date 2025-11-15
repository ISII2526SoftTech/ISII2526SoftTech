using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.HerramientaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientaReparacion_test : AppForSEII25264SqliteUT
    {
        public GetHerramientaReparacion_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("BOSCH"),
                new Fabricante("BISERMA"),
                new Fabricante("MANOLO94")
            };

            var herramientas = new List<Herramienta> {
                new Herramienta("Llave Inglesa",fabricantes[0] , 100,"metal","10"),
                new Herramienta("Martillo de uña", fabricantes[1], 150, "Acero", "15"),
                new Herramienta("Sierra de Madera", fabricantes[2], 80, "Madera", "20")
            };

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();
        }
        public static IEnumerable<object[]> GetHerramientaReparacion_data()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("BOSCH"),
                new Fabricante("BISERMA"),
                new Fabricante("MANOLO94")
            };
            fabricantes[0].Id = 1;
            fabricantes[1].Id = 2;
            fabricantes[2].Id = 3;

            var herramientaDTOs = new List<HerramientaDTO> {
                new HerramientaDTO(1,"Llave Inglesa","metal",100,fabricantes[0]),
                new HerramientaDTO(2,"Martillo de uña","Acero",150,fabricantes[1]), 
                new HerramientaDTO(3,"Sierra de Madera","Madera",80,fabricantes[2])
            };

            var herramientaDTOsTC1 = new List<HerramientaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            }.ToList();

            var herramientaDTOsTC2 = new List<HerramientaDTO>() { herramientaDTOs[1] }
               .OrderBy(h => h.Nombre).ToList();
            var herramientaDTOsTC3 = new List<HerramientaDTO>() { herramientaDTOs[0] }
                .OrderBy(h => h.Nombre).ToList();
            var herramientaDTOsTC4 = new List<HerramientaDTO>() { herramientaDTOs[1], herramientaDTOs[0] }
                .OrderBy(h => h.Nombre).ToList();

            var todosLosTest = new List<object[]>
            {
                new object[] { null,"15", herramientaDTOsTC2 },
                new object[] { "Martillo de uña", null, herramientaDTOsTC2, },
                new object[] { "Llave Inglesa", null, herramientaDTOsTC3, },
            };

            return todosLosTest;
        }

        [Theory]
        [MemberData(nameof(GetHerramientaReparacion_data))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaReparar_Ok_test(string? nombrefiltro, string? tiempoReparacionFiltro, IList<HerramientaDTO> expectedHerramientas)
        {
            var controller = new HerramientasController(_context, null);

            // Act
            var result = await controller.GetSelectFiltradoReparacion(nombrefiltro, tiempoReparacionFiltro);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientasDTOsActuales = Assert.IsType<List<HerramientaDTO>>(okResult.Value);

            var expectedOrdenadas = expectedHerramientas.OrderBy(h => h.Id).ToList();
            var actualOrdenadas = herramientasDTOsActuales.OrderBy(h => h.Id).ToList();

            Assert.Equal(expectedOrdenadas, actualOrdenadas);


        }
    }
}