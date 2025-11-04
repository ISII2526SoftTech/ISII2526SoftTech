using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AppForSEII2526.API.DTOs.ReparaciónDTO
{
    public class ReparacionForCreateDTO
    {
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string ApellidoCliente { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; }
        [Required]
        public float PrecioTotal { get; set; }
        [Required]
        public DateTime FechaRecogida { get; set; }
        [Required]
        public TiposMetodoPago MetodoPago { get; set; }

        public string? Descripcion { get; set; }
        public string? NºTelefono { get; set; }
        [Required]
        public int Cantidad { get; set; }

        public IList<ReparacionItemDTO> ReparacionItem { get; set; }

        public ReparacionForCreateDTO()
        {
            ReparacionItem = new List<ReparacionItemDTO>();
        }

        public ReparacionForCreateDTO(string nombre, string apellido, DateTime fechaEntrega, float precioTotal,
                                      DateTime fechaRecogida, TiposMetodoPago metodoPago,
                                      int cantidad, IList<ReparacionItemDTO> reparacionItem)
        {
            NombreCliente = nombre;
            ApellidoCliente = apellido;
            FechaEntrega = fechaEntrega;
            PrecioTotal = precioTotal;
            FechaRecogida = fechaRecogida;
            MetodoPago = metodoPago;
            Cantidad = cantidad;
            ReparacionItem = reparacionItem;
        }

        public ReparacionForCreateDTO(string nombre, string apellido, DateTime fechaEntrega, float precioTotal,
                                      DateTime fechaRecogida, TiposMetodoPago metodoPago,
                                      int cantidad, IList<ReparacionItemDTO> reparacionItem, string telefono)
        {
            NombreCliente = nombre;
            ApellidoCliente = apellido;
            FechaEntrega = fechaEntrega;
            PrecioTotal = precioTotal;
            FechaRecogida = fechaRecogida;
            MetodoPago = metodoPago;
            Cantidad = cantidad;
            ReparacionItem = reparacionItem;
            NºTelefono = telefono;
        }

        public ReparacionForCreateDTO(DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal, List<ReparacionItemDTO> herramientasAReparar)
        {
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            ReparacionItem = herramientasAReparar;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionForCreateDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaEntrega == dTO.FechaEntrega &&
                   PrecioTotal == dTO.PrecioTotal &&
                   FechaRecogida == dTO.FechaRecogida &&
                   MetodoPago == dTO.MetodoPago &&
                   Descripcion == dTO.Descripcion &&
                   NºTelefono == dTO.NºTelefono &&
                   Cantidad == dTO.Cantidad &&
                   EqualityComparer<IList<ReparacionItemDTO>>.Default.Equals(ReparacionItem, dTO.ReparacionItem);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(NombreCliente);
            hash.Add(ApellidoCliente);
            hash.Add(FechaEntrega);
            hash.Add(PrecioTotal);
            hash.Add(FechaRecogida);
            hash.Add(MetodoPago);
            hash.Add(Descripcion);
            hash.Add(NºTelefono);
            hash.Add(Cantidad);
            hash.Add(ReparacionItem);
            return hash.ToHashCode();
        }
    }
}