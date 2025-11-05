namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        public int Id { get; set; }
        public int OfertaId { get; set; }

   
        [Required]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public decimal Porcentaje { get; set; }


        //RELACION
        [ForeignKey("OfertaId")]
        public virtual Oferta Oferta { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double PrecioFinal { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double PrecioOriginal { get; set; }

        public virtual Herramienta Herramienta { get; set; }

        public OfertaItem()
        {
        }
        public OfertaItem(int ofertaId, Herramienta herramienta, decimal porcentaje, double precioFinal)
        {
            OfertaId = ofertaId;
            Herramienta = herramienta;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
        }
        public OfertaItem(int ofertaId, Herramienta herramienta, decimal porcentaje, double precioFinal, double precioOriginal)
        {
            OfertaId = ofertaId;
            Herramienta = herramienta;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            PrecioOriginal = precioOriginal;
        }
        public OfertaItem(int ofertaId, decimal porcentaje, double precioFinal,double precioOriginal, Oferta oferta, Herramienta herramienta)
        {
            Herramienta = herramienta;
            OfertaId = ofertaId;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Oferta = oferta;
            Herramienta = herramienta;
            PrecioOriginal = precioOriginal;
        }
    }
}
