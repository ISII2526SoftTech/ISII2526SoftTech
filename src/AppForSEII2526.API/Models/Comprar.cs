namespace AppForSEII2526.API.Models
{
    public class Comprar
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede ser mas largo de 50 caracteres", MinimumLength = 3)]
        public string NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "El apellido no pueder ser mas largo de 50 caracteres", MinimumLength = 3)]
        public string ApellidoCliente { get; set; }

        [StringLength(50, ErrorMessage = "El correo no es valido", MinimumLength = 3)]
        public string CorreoElectronico { get; set; }

        [StringLength(9, ErrorMessage = "El teléfono debe ser de 9 dígitos")]
        public string Telefono { get; set; }

        [StringLength(100, ErrorMessage = "La dirección no puede ser mas larga de 100 caracteres", MinimumLength = 5)]
        public string DireccionEnvio { get; set; }


        public DateTime FechaCompra { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal PrecioTotal { get; set; }
       

        public TiposMetodoPago MetodoPago { get; set; }


        public virtual List<CompraItem> CompraItems { get; set; }


        public Comprar()
        {
            CompraItems = new List<CompraItem>();
        }

        public Comprar(string nombreCliente, string apellidoCliente, string correoElectronico,
                      string telefono, string direccionEnvio, DateTime fechaCompra,
                      decimal precioTotal, TiposMetodoPago metodoPago)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            CompraItems = new List<CompraItem>();

        }
    }
}
