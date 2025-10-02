namespace AppForSEII2526.API.Models
{
    public class OfertaItem
    {
        
        [Key]
        public int idOferta { get; set; }
        public int idHerramienta { get; set; }
        public decimal porcentaje { get; set; }
        public decimal precioFinal { get; set; }

        public OfertaItem()
        {
        }

        // Constructor con parámetros
        public OfertaItem(int idHerramienta, int idOferta, decimal porcentaje, decimal precioFinal)
        {
            this.idHerramienta = idHerramienta;
            this.idOferta = idOferta;
            this.porcentaje = porcentaje;
            this.precioFinal = precioFinal;
        }
    }
}
