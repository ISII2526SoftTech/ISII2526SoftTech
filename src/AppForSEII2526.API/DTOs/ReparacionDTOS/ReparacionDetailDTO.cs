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

        public ReparacionDetailDTO(int id, DateTime fechaEntrega, DateTime fechaRecogida,string nombreCliente,string apellidoCliente,
                                  TiposMetodoPago metoodopago, float precioTotal,string telefono, List<ReparacionItemDTO> reparacionItem)
                               : base(nombreCliente,apellidoCliente,fechaEntrega,metoodopago,telefono, reparacionItem)
        {
            Id = id;
            FechaEntrega = fechaEntrega;
            precioTotal = precioTotal;


        }

        public ReparacionDetailDTO(int id, string nombreCliente, string apellidoCliente, string? telefono, DateTime fechaEntrega, DateTime fechaRecogida, float precioTotal,TiposMetodoPago metoodopago,List<ReparacionItemDTO> reparaciones)
        {
            Id = id;
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            NºTelefono = telefono;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            MetodoPago = metoodopago;
            reparacionItem = reparaciones;

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