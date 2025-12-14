using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.Models;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AppForSEII2526.API.Controllers;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientas_test : AppForSEII25264SqliteUT
    {   
        public GetHerramientas_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Arcos"),
                new Fabricante("FABRICANTE2"),
                new Fabricante("Man")
            };

            var herramientas = new List<Herramienta> {
                new Herramienta("Taladro",fabricantes[0] , 100,"metal",null),
                new Herramienta("Sierra", fabricantes[1], 150, "Acero", null)
                
            };
            
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();
            
        }

       
        public static IEnumerable<object[]> GetHerramientas_TestData()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Arcos"),
                new Fabricante("FABRICANTE2"),
                new Fabricante("Man")
            };
            fabricantes[0].Id = 1;
            fabricantes[1].Id = 2;
            var herramientaDTOs = new List<HerramientaOfertaDTO>()
            {   
                new HerramientaOfertaDTO(1,"Taladro", "metal", 100, fabricantes[0]),
                new HerramientaOfertaDTO(2,"Sierra", "Acero", 150, fabricantes[1])
            };

            var herramientaDTOsTC2 = new List<HerramientaOfertaDTO>() { herramientaDTOs[1] }
                .OrderBy(h => h.Nombre).ToList();
            var herramientaDTOsTC3 = new List<HerramientaOfertaDTO>() { herramientaDTOs[0] }
                .OrderBy(h => h.Nombre).ToList();
            var herramientaDTOsTC4 = new List<HerramientaOfertaDTO>() { herramientaDTOs[1], herramientaDTOs[0] }
                .OrderBy(h => h.Nombre).ToList();

            var todosLosTest = new List<object[]>
            {             
                new object[] { "FABRICANTE2", 1000.00, herramientaDTOsTC2, },
                new object[] { null, 200.00, herramientaDTOsTC4, },
                new object[] { "Arcos", null, herramientaDTOsTC3, },
            };
            
            return todosLosTest;
        }


        [Theory]
        [MemberData(nameof(GetHerramientas_TestData))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetSelectFiltradoOferta_OK_test(string? fabricante, double? precioMaximo, IList<HerramientaOfertaDTO> expectedHerramientas)
        {
            var controller = new HerramientasController(_context, null);

            // Act
            var result = await controller.GetSelectFiltradoOferta(fabricante, precioMaximo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientasDTOsActual = Assert.IsType<List<HerramientaOfertaDTO>>(okResult.Value);

            var expectedOrdenadas = expectedHerramientas.OrderBy(h => h.Nombre).ToList();
            var actualOrdenadas = herramientasDTOsActual.OrderBy(h => h.Nombre).ToList();

            Assert.Equal(expectedOrdenadas, actualOrdenadas);
        }


      
    }
}
