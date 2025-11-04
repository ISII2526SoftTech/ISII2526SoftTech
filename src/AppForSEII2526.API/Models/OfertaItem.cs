namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        public int Id { get; set; }
        public int OfertaId { get; set; }

        
        public int HerramientaId { get; set; }

   
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


        [ForeignKey("HerramientaId")]
        public virtual Herramienta Herramienta { get; set; }

        public OfertaItem()
        {
        }
        public OfertaItem(int ofertaId, int herramientaId, decimal porcentaje, double precioFinal)
        {
            OfertaId = ofertaId;
            HerramientaId = herramientaId;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
        }

        public OfertaItem(int herramientaId, int ofertaId, decimal porcentaje, double precioFinal,double precioOriginal, Oferta oferta, Herramienta herramienta)
        {
            HerramientaId = herramientaId;
            OfertaId = ofertaId;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Oferta = oferta;
            Herramienta = herramienta;
            PrecioOriginal = precioOriginal;
        }
    }
}
