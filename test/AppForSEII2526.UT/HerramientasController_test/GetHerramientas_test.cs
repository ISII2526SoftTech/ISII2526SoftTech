using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.Models;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientas_test : AppForSEII25264SqliteUT
    {   
        public GetHerramientas_test()
        {
            var fabricantes = new List<Fabricante>() {
                new Fabricante("Bosh"),
                new Fabricante("Union"),
                new Fabricante("Arcos"),
                new Fabricante("Man")
            };

            var herramientas = new List<Herramienta> {
                new Herramienta("Taladro", fabricantes[0], 100, "metal", "1 semana"),
                new Herramienta("Sierra", fabricantes[1], 150, "Acero", "2 días")
            };
            ApplicationUser user = new ApplicationUser
            {
                
            };
            var oferta = new Oferta(DateTime.Now.AddDays(7), DateTime.Now.AddDays(2), DateTime.Now, 0, new List<OfertaItem>());
            oferta.OfertaItems = new List<OfertaItem>()
            {
                new OfertaItem(oferta.Id, herramientas[0].Id, 50, 50),
                new OfertaItem(oferta.Id, herramientas[1].Id, 50, 75)
            };
            
            _context.Add(herramientas);
            _context.Add(fabricantes);
            _context.Add(oferta);
            _context.SaveChanges();


        }
        /*
        public static IEnumerable<object[]> GetHerramientas_TestData()
        {
            var herramientaDTOs = new List<HerramientaDTO>()
            {   
                new HerramientaDTO(1, "Taladro", "metal", 100, new Fabricante("Bosh"), "1 semana"),
                new HerramientaDTO(2, "Sierra", "Acero", 150, new Fabricante("Union"), "2 días")
            };
    
        }
        [Theory]
        [MemberData(nameof(GetHerramientas_TestData))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientas_OK_test(string? fabricante, IList<HerramientaDTO> expectedHerramientas)
        {
             
            
        }

        */
    }
}
