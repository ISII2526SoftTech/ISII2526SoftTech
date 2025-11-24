using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarForCreateDTO
    {

        public ComprarForCreateDTO(string nombreCliente, string apellidoCliente, string direccion, TiposMetodoPago metodoPago, string? email, int telefono, IList<ComprarItemDTO> comprarItem, TiposMetodoPago tiposMetodoPago)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            Direccion = direccion;
            TiposMetodoPago = metodoPago;
            Email = email;
            Telefono = telefono;
            ComprarItem = comprarItem;
        }

        public string Direccion { get; set; }
        public TiposMetodoPago TiposMetodoPago { get; set; }
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string ApellidoCliente { get; set; }
        public IList<ComprarItemDTO> ComprarItem { get; set; }
        public string? Email { get; set; }
        public int? Telefono { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ComprarForCreateDTO dTO &&
                   Direccion == dTO.Direccion &&
                   TiposMetodoPago == dTO.TiposMetodoPago &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   EqualityComparer<IList<ComprarItemDTO>>.Default.Equals(ComprarItem, dTO.ComprarItem) &&
                   Email == dTO.Email &&
                   Telefono == dTO.Telefono;
        }
    }
}
