namespace AppForSEII2526.API.Models
{
    public class Comprar
    {
        public int Id { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string correoElectronico { get; set; }
        public string telefono { get; set; }
        public string direccionEnvio { get; set; }
        public DateTime fechaCompra { get; set; }
        public decimal precioTotal { get; set; }


        public TipoMetodoPago metodoPago { get; set; }


        public virtual List<CompraItem> CompraItems { get; set; }


        public Comprar()
        {
            CompraItems = new List<CompraItem>();
        }

        public Comprar(int id, string nombreCliente, string apellidoCliente, string correoElectronico,
                      string telefono, string direccionEnvio, DateTime fechaCompra,
                      decimal precioTotal, TipoMetodoPago metodoPago)
        {
            Id = id;
            this.nombreCliente = nombreCliente;
            this.apellidoCliente = apellidoCliente;
            this.correoElectronico = correoElectronico;
            this.telefono = telefono;
            this.direccionEnvio = direccionEnvio;
            this.fechaCompra = fechaCompra;
            this.precioTotal = precioTotal;
            this.metodoPago = metodoPago;
            CompraItems = new List<CompraItem>();

        }
    }
}
