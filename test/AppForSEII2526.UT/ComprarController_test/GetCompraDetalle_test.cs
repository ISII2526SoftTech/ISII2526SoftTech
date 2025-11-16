/* using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ComprarController_test
{
    public class GetCompraDetalle_test : AppForSEII25264SqliteUT
    {
        public GetCompraDetalle_test()
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
            //string nombreCliente, string apellidoCliente, string? correoElectronico, string? telefono
            ApplicationUser user = new ApplicationUser("Manuel", "Castano", "manuel@uclm.es", "666666666");
            //int id, string direccionEnvio, DateTime fechaCompra, decimal precioTotal, TiposMetodoPago metodoPago, List<CompraItem> compraItems, ApplicationUser applicationUser
            var compra = new Comprar
            {
                Id = 1,
                DireccionEnvio = "Calle Falsa 123",
                FechaCompra = DateTime.Now,
                PrecioTotal = 125,
                MetodoPago = TiposMetodoPago.TarjetaCredito,
                CompraItems = new List<CompraItem>(),
                ApplicationUser = user
            };
            compra.CompraItems.Add(new CompraItem(1,"herramienta1",1,compra,1,herramienta1,100));
            compra.CompraItems.Add(new CompraItem(2, "herramienta2", 2, compra, 2, herramienta2, 150));
            //int cantidad, string descripcion, int idCompra, Comprar comprar,  int idHerramienta, Herramienta herramienta, decimal precio

            _context.Comprar.Add(compra);
            _context.SaveChanges();
            var compraItem1 = new CompraItem
            {
                cantidad=compra.CompraItems[0].cantidad,
                descripcion=compra.CompraItems[0].descripcion,
                idCompra=compra.CompraItems[0].idCompra,
                comprar=compra.CompraItems[0].comprar,
                idHerramienta=compra.CompraItems[0].idHerramienta,
                herramienta=compra.CompraItems[0].herramienta,
                precio=compra.CompraItems[0].precio
            };

            var compraItem2 = new CompraItem
            {
                cantidad = compra.CompraItems[1].cantidad,
                descripcion = compra.CompraItems[1].descripcion,
                idCompra = compra.CompraItems[1].idCompra,
                comprar = compra.CompraItems[1].comprar,
                idHerramienta = compra.CompraItems[1].idHerramienta,
                herramienta = compra.CompraItems[1].herramienta,
                precio = compra.CompraItems[1].precio
            };
            _context.CompraItem.AddRange(new List<CompraItem> { compraItem1, compraItem2 });
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
            }
        }
    }
*/
