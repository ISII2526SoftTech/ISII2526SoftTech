namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FechaFinal { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        public DateTime FechaOferta { get; set; }
        
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

    
