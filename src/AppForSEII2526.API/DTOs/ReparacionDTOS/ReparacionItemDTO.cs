namespace AppForSEII2526.API.DTOs.ReparaciónDTO
{
    public class ReparacionItemDTO
    {
        //Capturar los datos de los items de la reparacion
        [Required]
        public int HerramientaId { get; set; }
        public string Descripcion { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public int Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public ReparacionItemDTO(int HerramientaId, float Precio, String descripcion, int cantidad)
        {
        }
        public ReparacionItemDTO(int herramientaId, string descripcion, int precio, int cantidad)
        {
            HerramientaId = herramientaId;
            Descripcion = descripcion;
            Precio = precio;
            Cantidad = cantidad;
        }

        public ReparacionItemDTO(int herramientaId, int precio, int cantidad)
        {
            HerramientaId = herramientaId;
            Precio = precio;
            Cantidad = cantidad;
        }


        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO dTO &&
                   HerramientaId == dTO.HerramientaId &&
                   Descripcion == dTO.Descripcion &&
                   Precio == dTO.Precio &&
                   Cantidad == dTO.Cantidad;
        }

    }
}