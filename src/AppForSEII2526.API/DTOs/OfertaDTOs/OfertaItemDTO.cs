namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaItemDTO
    {
        //Capturar el porcentaja de rebaja para cada herramienta
        public OfertaItemDTO(int idHerramienta, decimal porcentaje, double precio)
        {
            IdHerramienta = idHerramienta;
            Porcentaje = porcentaje;
            Precio =  precio;

        }
        public int IdHerramienta { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Required]
        public decimal Porcentaje { get; set; }



        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public double Precio { get; set; }

       

    }
}
