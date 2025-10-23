namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        [Key]
        public int IdOferta { get; set; }
        public int HerramientaId { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Required]
        public decimal Porcentaje { get; set; }


        //RELACION
        public Oferta Oferta { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double PrecioFinal { get; set; }

        public Herramienta Herramienta
        {
            get => default;
            set
            {
            }
        }

        public OfertaItem()
        {
        }

       
        public OfertaItem(int herramientaId, int idOferta, decimal porcentaje, double precioFinal, Oferta oferta, Herramienta herramienta)
        {
            HerramientaId = herramientaId;
            IdOferta = idOferta;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Oferta = oferta;
            Herramienta = herramienta;
        }
    }
}
