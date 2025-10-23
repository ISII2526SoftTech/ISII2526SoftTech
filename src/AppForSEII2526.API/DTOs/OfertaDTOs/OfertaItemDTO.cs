namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        public OfertaItemDTO()
        {
        }
        //Capturar el porcentaja de rebaja para cada herramienta
        public OfertaItemDTO(int herramientaId, decimal porcentaje, double precio)
        {
            HerramientaId = herramientaId;
            Porcentaje = porcentaje;
            Precio = precio;
        }

        public int HerramientaId { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Required]
        public decimal Porcentaje { get; set; }



        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double Precio { get; set; }

       

    }
}
