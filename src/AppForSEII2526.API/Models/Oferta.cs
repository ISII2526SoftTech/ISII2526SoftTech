

using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        private DateTime _fechaInicio;
        private DateTime _fechaFinal;
        private DateTime _fechaOferta;

        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date), Display(Name = "FechaFinal")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal
        {
            get => _fechaFinal;
            set => _fechaFinal = value.Date;
        }
        [Required]
        [DataType(DataType.Date), Display(Name = "FechaInicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio 
        {
            get => _fechaInicio;
            set => _fechaInicio = value.Date;
        }
        [Required]
        [DataType(DataType.Date), Display(Name = "FechaOferta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta 
        {
            get => _fechaOferta;
            set => _fechaOferta = value.Date;
        }
        
        [Required]
        public TiposMetodoPago MetodoPago { get; set; }

        //RELACIONES
        public TiposDirigidaOferta? DirigidaA { get; set; }
        public virtual List<OfertaItem> OfertaItems { get; set; }

        public Oferta()
        {
            OfertaItems = new List<OfertaItem>();
        }

        
        public Oferta(DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta, TiposDirigidaOferta dirigidaA)
        {
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            FechaOferta = fechaOferta;
            DirigidaA = dirigidaA;
            OfertaItems = new List<OfertaItem>();

        }

        public Oferta(DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta, int id, TiposMetodoPago metodoPago, TiposDirigidaOferta? dirigidaA, List<OfertaItem> ofertaItems)
        {
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            FechaOferta = fechaOferta;
            Id = id;
            MetodoPago = metodoPago;
            DirigidaA = dirigidaA;
            OfertaItems = ofertaItems;
        }
        public Oferta(DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta, TiposMetodoPago metodoPago, List<OfertaItem> ofertaItems)
        {
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            FechaOferta = fechaOferta;
            MetodoPago = metodoPago;
            OfertaItems = ofertaItems;
        }
    }
}

    
