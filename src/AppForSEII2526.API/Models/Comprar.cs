using AppForSEII2526.API.DTOs.ComprarDTOs;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Comprar
    {
        public Comprar() { }
        public Comprar(string direccionEnvio, DateTime fechaCompra, IList<CompraItem> comprarItem, decimal precioTotal, TiposMetodoPago metodoPago, ApplicationUser applicationUser)
        {
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            ComprarItem = comprarItem;
            ApplicationUser = applicationUser;
        }

        public int Id { get; set; }

        public IList<CompraItem> ComprarItem { get; set; }

        [StringLength(100, ErrorMessage = "La dirección no puede ser mas larga de 100 caracteres", MinimumLength = 5)]
        public string DireccionEnvio { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }


        [DataType(DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo es 0.01")]
        public decimal PrecioTotal { get; set; }

        [Required]
        public TiposMetodoPago MetodoPago { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
