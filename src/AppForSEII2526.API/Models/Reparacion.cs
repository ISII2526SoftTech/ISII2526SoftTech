using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
       

      
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRecogida { get; set; }
        public int Id { get; set; }

        //RELACION
        public virtual List<ReparacionItem> ReparacionItems{ get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float PrecioTotal { get; set; }

        public TiposMetodoPago metodoPago
        {
            get => default;
            set
            {
            }
        }

        public Reparacion()
        {
            ReparacionItems = new List<ReparacionItem>();
        }

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, string numTelefono, float precioTotal)
        {
            
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
        }


    }
}
