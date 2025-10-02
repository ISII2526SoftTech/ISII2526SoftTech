namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        // Propiedades/Atributos
        public string ApellidoCliente { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRecogida { get; set; }
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string NumTelefono { get; set; }
        public float PrecioTotal { get; set; }

        // Constructor vacío
        public Reparacion()
        {
        }

        // Constructor con todos los parámetros
        public Reparacion(string apellidoCliente, DateTime fechaEntrega, DateTime fechaRecogida,
                         int id, string nombreCliente, string numTelefono, float precioTotal)
        {
            ApellidoCliente = apellidoCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            Id = id;
            NombreCliente = nombreCliente;
            NumTelefono = numTelefono;
            PrecioTotal = precioTotal;
        }

        // Constructor sin ID (para cuando se genera automáticamente)
        public Reparacion(string apellidoCliente, DateTime fechaEntrega, DateTime fechaRecogida,
                         string nombreCliente, string numTelefono, float precioTotal)
        {
            ApellidoCliente = apellidoCliente;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            NombreCliente = nombreCliente;
            NumTelefono = numTelefono;
            PrecioTotal = precioTotal;
        }

    }
}
