using AppForSEII2526.API.DTOs.ReparaciónDTO;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace AppForSEII2526.API.DTOs.ReparacionDTOs
{
    public class ReparacionDetailDTO : ReparacionForCreateDTO
    {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRecogida { get; set; }
        public float PrecioTotal { get; set; }
        public int Cantidad { get; set; }

        public List<ReparacionItemDTO> HerramientasAReparar { get; set; }
        public List<ReparacionItemDTO> ReparacionItemDTOs { get; }

        /* Estas variables son las heredadas de ReparacionForCreateDTO:
         * * public string NombreCliente { get; set; }
         * public string ApellidoCliente { get; set; }
         * public TiposMetodoPago MetodoPago { get; set; }
         * public string? Descripcion { get; set; }
         * public string? NºTelefono { get; set; }
         * public IList<ReparacionItemDTO> ReparacionItem { get; set; }
        */

        public ReparacionDetailDTO()
        {
            new List<ReparacionItemDTO>();
        }

        public ReparacionDetailDTO(int id, DateTime fechaEntrega, DateTime fechaRecogida,
                                   float precioTotal, int cantidad, List<ReparacionItemDTO> herramientasAReparar)
                               : base(fechaEntrega, fechaRecogida, precioTotal, herramientasAReparar)
        {
            Id = id;
            foreach (var item in herramientasAReparar)
            {
                Cantidad = cantidad;
            }
        }

        public ReparacionDetailDTO(DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal, List<ReparacionItemDTO> reparacionItemDTOs)
        {
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            ReparacionItemDTOs = reparacionItemDTOs;
        }

        public ReparacionDetailDTO(int id, string nombreCliente, string apellidoCliente, string? telefono, DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            NºTelefono = telefono;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
        }

        public ReparacionDetailDTO(int id, DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal)
        {
            Id = id;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionDetailDTO dTO &&
                   Id == dTO.Id &&
                   FechaEntrega.Equals(dTO.FechaEntrega) &&
                   FechaRecogida.Equals(dTO.FechaRecogida) &&
                   PrecioTotal == dTO.PrecioTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FechaEntrega, FechaRecogida, PrecioTotal);
        }
    }
}