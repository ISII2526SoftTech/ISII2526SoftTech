using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.HerramientaDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetComprarHerramientas_test : AppForSEII25264SqliteUT
    {
        public GetComprarHerramientas_test()
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


            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> GetHerramientasComprar_TestData()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Arcos"),
                new Fabricante("FABRICANTE2"),
                new Fabricante("Man")
            };
            fabricantes[0].Id = 1;
            fabricantes[1].Id = 2;
            var herramientasDTO = new List<HerramientaComprarDTO>
            {
                new HerramientaComprarDTO (1, "Taladro", "metal", 100, fabricantes[0].Nombre),
                new HerramientaComprarDTO (2, "Sierra", "Acero", 150, fabricantes[1].Nombre),
                new HerramientaComprarDTO (3, "Martillo", "Acero", 15, fabricantes[2].Nombre)
            };

           

            // Para filtro por material "Acero" el controlador devuelve todas las herramientas cuyo Material contiene "Acero"
            // => tanto "Sierra" como "Martillo". Ordenadas por Nombre => "Martillo","Sierra"
            var herramientasDTOsTC3 = new List<HerramientaComprarDTO> { herramientasDTO[1], herramientasDTO[2] }
                .OrderBy(m => m.Nombre).ToList();

            // Para filtro por precio 150 (<= 150) el controlador devuelve todas con precio <= 150 (incluye 100 y 15 y 150)
            var herramientasDTOsTC2 = new List<HerramientaComprarDTO> { herramientasDTO[0], herramientasDTO[1], herramientasDTO[2] }
                .OrderBy(m => m.Nombre).ToList();
          


            var allTest = new List<object[]>
            {
              
                new object[] { "Acero" ,null,herramientasDTOsTC3},
                // pasar literal decimal para evitar error de enlace de tipos en xUnit
                new object[] { null , 150m,herramientasDTOsTC2},
               

            };

            return allTest;
        }

        [Theory] //Comprobar varios casos
        [MemberData(nameof(GetHerramientasComprar_TestData))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetHerramientaComprar_OK_test(string? material, decimal? precio, List<HerramientaComprarDTO> expectedHerramientas)
        {
            // Arrange (Se define todas las variables que se necesitan)
            // pasar un logger no nulo al controlador para evitar ArgumentNullException
            var controlador = new HerramientasController(_context, NullLogger<HerramientasController>.Instance);

            // Act
            var resultado = await controlador.GetHerramientaComprar(material, precio);

            // Assert
            var okResultado = Assert.IsType<OkObjectResult>(resultado);
            var herramientaDTosActual = Assert.IsType<List<HerramientaComprarDTO>>(okResultado.Value);
            Assert.Equal(expectedHerramientas, herramientaDTosActual);


        }
    }
}