namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        
        [Key]
        public int IdOferta { get; set; }
        public int IdHerramienta { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Required]
        public decimal Porcentaje { get; set; }


        //RELACION
        public Oferta Oferta { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal PrecioFinal { get; set; }

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

       
        public OfertaItem(int idHerramienta, int idOferta, decimal porcentaje, decimal precioFinal, Oferta oferta)
        {
            IdHerramienta = idHerramienta;
            IdOferta = idOferta;
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Oferta = oferta;
        }
    }
}
