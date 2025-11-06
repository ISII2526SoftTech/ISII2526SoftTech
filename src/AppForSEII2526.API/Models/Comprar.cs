
using AppForSEII2526.API.DTOs.ComprarDTOs;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Comprar
    {
        private string? nombreCliente;
        private string? apellidoCliente;
        private ComprarForCreateDTO compraForCreate;
        private EmailAddressAttribute email;
        private int telefono;
        private string descripcion;
        private int cantidad;

        public int Id { get; set; }

       

        [StringLength(100, ErrorMessage = "La dirección no puede ser mas larga de 100 caracteres", MinimumLength = 5)]
        public string DireccionEnvio { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "FechaCompra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }


        [DataType(DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal PrecioTotal { get; set; }

        [Required]
        public TiposMetodoPago MetodoPago { get; set; }


        public virtual List<CompraItem> CompraItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
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

        public Comprar(int id, string direccionEnvio, DateTime fechaCompra, decimal precioTotal, TiposMetodoPago metodoPago, List<CompraItem> compraItems, ApplicationUser applicationUser)
        {
            Id = id;
            DireccionEnvio = direccionEnvio;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            MetodoPago = metodoPago;
            CompraItems = compraItems;
            ApplicationUser = applicationUser;
        }

        public Comprar(string? nombreCliente, string? apellidoCliente, string? direccionEnvio, TiposMetodoPago metodoPago)
        {
            this.nombreCliente = nombreCliente;
            this.apellidoCliente = apellidoCliente;
            DireccionEnvio = direccionEnvio;
            MetodoPago = metodoPago;
        }

        public Comprar(string? nombreCliente, string? apellidoCliente, string? direccionEnvio, TiposMetodoPago metodoPago, ComprarForCreateDTO compraForCreate, EmailAddressAttribute email, int telefono) : this(nombreCliente, apellidoCliente, direccionEnvio, metodoPago)
        {
            this.compraForCreate = compraForCreate;
            this.email = email;
            this.telefono = telefono;
        }

        public Comprar(string? nombreCliente, string? apellidoCliente, string? direccionEnvio, TiposMetodoPago metodoPago, ComprarForCreateDTO compraForCreate, EmailAddressAttribute email, int telefono, List<CompraItem> compraItems) : this(nombreCliente, apellidoCliente, direccionEnvio, metodoPago, compraForCreate, email, telefono)
        {
        }

        public Comprar(string? nombreCliente, string? apellidoCliente, string? direccionEnvio, TiposMetodoPago metodoPago, ComprarForCreateDTO compraForCreate, EmailAddressAttribute email, int telefono, List<CompraItem> compraItems, string descripcion, int cantidad) : this(nombreCliente, apellidoCliente, direccionEnvio, metodoPago, compraForCreate, email, telefono, compraItems)
        {
            this.descripcion = descripcion;
            this.cantidad = cantidad;
        }
    }
}
