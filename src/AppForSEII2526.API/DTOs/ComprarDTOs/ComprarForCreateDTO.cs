using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarForCreateDTO
    {

        public ComprarForCreateDTO(string nombreCliente, string apellidoCliente, string direccion, DateTime fechaCompra, IList<ComprarItemDTO> comprarItem, decimal precioTotal)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente ?? throw new ArgumentNullException(nameof(ApellidoCliente)); ;
            Direccion = direccion ?? throw new ArgumentNullException(nameof(Direccion)); ;
            FechaCompra = fechaCompra;
            PrecioTotal = comprarItem.Sum(r => r.Precio * r.Cantidad);

            IList<ComprarItemDTO> ComprarItem = comprarItem ?? throw new ArgumentNullException(nameof(ComprarItem));
        }


        public int Id { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Direccion")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Delivery address must have at least 10 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string Direccion { get; set; }

        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string ApellidoCliente { get; set; }


        public DateTime FechaCompra { get; set; }
        public decimal PrecioTotal { get; set; }
        public IList<ComprarItemDTO> ComprarItem { get; set; }
        public string DireccionEnvio { get; }

        public ComprarForCreateDTO()
        {
            ComprarItem = new List<ComprarItemDTO>();

        }

        public ComprarForCreateDTO(string nombreCliente, string apellidoCliente, string direccionEnvio, DateTime fechaCompra)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
        }

        public override bool Equals(object? obj)
        {
            return obj is ComprarForCreateDTO dTO &&

                   Direccion == dTO.Direccion &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   ComprarItem.SequenceEqual(dTO.ComprarItem) &&
                   PrecioTotal == dTO.PrecioTotal &&
                   FechaCompra == dTO.FechaCompra;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Direccion, NombreCliente, ApellidoCliente, FechaCompra, PrecioTotal, ComprarItem);
        }
    }
}
