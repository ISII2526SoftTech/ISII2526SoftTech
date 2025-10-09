namespace AppForSEII2526.API.Models
{
    public class Comprar
    {
        public int Id { get; set; }

       

        [StringLength(100, ErrorMessage = "La dirección no puede ser mas larga de 100 caracteres", MinimumLength = 5)]
        public string DireccionEnvio { get; set; }


        public DateTime FechaCompra { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal PrecioTotal { get; set; }

        [Required]
        public TiposMetodoPago MetodoPago { get; set; }


        public virtual List<CompraItem> CompraItems { get; set; }


        public Comprar()
        {
            CompraItems = new List<CompraItem>();
        }

        public Comprar(string direccionEnvio, DateTime fechaCompra,
                      decimal precioTotal, TiposMetodoPago metodoPago)
        {
          
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            CompraItems = new List<CompraItem>();

        }
    }
}
