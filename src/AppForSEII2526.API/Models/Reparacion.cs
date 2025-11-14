using AppForSEII2526.API.DTOs.ReparaciónDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {


        [Required]
        [DataType(DataType.Date), Display(Name = "FechaEntrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [DataType(DataType.Date), Display(Name = "FechaRecogida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecogida { get; set; }
        public int Id { get; set; }


        //RELACION
        public virtual List<ReparacionItem> ReparacionItems{ get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float PrecioTotal { get; set; }

        public TiposMetodoPago metodoPago { get; set; }

        public Reparacion()
        {
            ReparacionItems = new List<ReparacionItem>();
        }
        

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, List<ReparacionItem> reparacionItems, TiposMetodoPago MetodoPago,ApplicationUser applicationUser)
        {

            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            ReparacionItems = reparacionItems ;
            metodoPago = MetodoPago;
            ApplicationUser = applicationUser;
        }

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, int id, List<ReparacionItem> reparacionItems, ApplicationUser applicationUser, float precioTotal, TiposMetodoPago metodopago)
        {
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            Id = id;
            ReparacionItems = reparacionItems;
            ApplicationUser = applicationUser;
            PrecioTotal = precioTotal;
            metodoPago = metodopago;
        }
    }
}
