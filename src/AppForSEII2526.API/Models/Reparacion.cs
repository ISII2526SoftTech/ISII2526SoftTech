using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.Models
{
    [Index(nameof(NumTelefono), IsUnique = true)]
    public class Reparacion
    {
       

        public string ApellidoCliente { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRecogida { get; set; }
        public int Id { get; set; }
        public string NombreCliente { get; set; }

        //RELACION
        public virtual List<ReparacionItem> ReparacionItems{ get; set; }


        [StringLength(9, ErrorMessage = "El teléfono debe ser de 9 dígitos")]
        public string NumTelefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float PrecioTotal { get; set; }

        public Reparacion()
        {
            ReparacionItems = new List<ReparacionItem>();
        }

        public Reparacion(string apellidoCliente, DateTime fechaEntrega, DateTime fechaRecogida, string nombreCliente, string numTelefono, float precioTotal)
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
