using AppForSEII2526.API.DTOs.HerramientaDTO;
using AppForSEII2526.API.DTOs.ReparaciónDTO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AppForSEII2526.API.DTOs.ReparaciónDTO
{
    public class ReparacionForCreateDTO
    {
      
        public string NombreCliente { get; set; }
        
        public string ApellidoCliente { get; set; }
        
        public DateTime FechaEntrega { get; set; }
        public TiposMetodoPago MetodoPago { get; set; }
        public string? NºTelefono { get; set; }
        


        public IList<ReparacionItemDTO> reparacionItem { get; set; }

        public ReparacionForCreateDTO()
        {
            reparacionItem = new List<ReparacionItemDTO>();
        }

        public ReparacionForCreateDTO(string nombre, string apellido, DateTime fechaEntrega,
                                       TiposMetodoPago metodoPago,
                                       string? telefono,IList<ReparacionItemDTO> reparacionitem)
        {
            NombreCliente = nombre;
            ApellidoCliente = apellido;
            FechaEntrega = fechaEntrega;
           
            MetodoPago = metodoPago;
            NºTelefono = telefono;
            reparacionItem = reparacionitem;
            
        }

        public ReparacionForCreateDTO(DateTime fechaEntrega, List<ReparacionItemDTO> herramientasAReparar)
        {
            FechaEntrega = fechaEntrega;
           
            
            reparacionItem = herramientasAReparar;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionForCreateDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaEntrega == dTO.FechaEntrega &&
                   
                   
                   MetodoPago == dTO.MetodoPago &&
                   NºTelefono == dTO.NºTelefono &&
                   EqualityComparer<IList<ReparacionItemDTO>>.Default.Equals(reparacionItem, dTO.reparacionItem);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(NombreCliente);
            hash.Add(ApellidoCliente);
            hash.Add(FechaEntrega);
          
            hash.Add(MetodoPago);
            hash.Add(NºTelefono);
            hash.Add(reparacionItem);
            return hash.ToHashCode();
        }
    }
}