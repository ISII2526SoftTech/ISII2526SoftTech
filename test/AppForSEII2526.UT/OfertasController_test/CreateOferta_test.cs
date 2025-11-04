using AppForSEII2526.API.DTOs.OfertaDTOs;
using System;
using System.Collections.Generic;
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
        
        public static IEnumerable<object[]> GetOferta_TestData()
        {
            var ofertaSinItem = new OfertaDetailDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>(),1, TiposDirigidaOferta.Socios);

            var ofertaConFechaIncorrecta1 = new OfertaForCreateDTO(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(7), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios);

            var ofertaConFechaIncorrecta2 = new OfertaDetailDTO(DateTime.Now.AddDays(7), DateTime.Now.AddDays(2), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(1, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                1, TiposDirigidaOferta.Socios);
            var ofertaConItemInexistente = new OfertaForCreateDTO(DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), TiposMetodoPago.TarjetaCredito,
                new List<OfertaItemDTO>()
                {
                    new OfertaItemDTO(999, 50, 100, 50),
                    new OfertaItemDTO(2, 50, 150, 75)
                },
                TiposDirigidaOferta.Socios);

            var todosLosTest = new List<object[]>
                        {
                            new object[] {  },
                        };

            return todosLosTest;
        }
    }
 
         
    
}
