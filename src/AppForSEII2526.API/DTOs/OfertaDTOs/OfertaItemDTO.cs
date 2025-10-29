namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        public OfertaItemDTO()
        {
        }
        public OfertaItemDTO(int herramientaId, decimal porcentaje, double precioOriginal, double precioFinal)
        {
            HerramientaId = herramientaId;
            Porcentaje = porcentaje;
            PrecioOriginal = precioOriginal;  
            PrecioFinal = precioFinal;       
        }

        public int HerramientaId { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Required]
        public decimal Porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double PrecioOriginal { get; set; } 

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double PrecioFinal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   Porcentaje == dTO.Porcentaje &&
                   PrecioOriginal == dTO.PrecioOriginal &&
                   PrecioFinal == dTO.PrecioFinal;
        }
    }
}
