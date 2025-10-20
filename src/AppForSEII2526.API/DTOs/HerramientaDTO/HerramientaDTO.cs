namespace AppForSEII2526.API.DTOs.HerramientaDTO
{
    public class HerramientaDTO
    {
        public HerramientaDTO()
        {
        }
        public HerramientaDTO(int id, string nombre, string material, double precio, string tiempoReparacion)
        {
            Id = id;
            Nombre = nombre;
            Material = material;

            Precio = precio;
            TiempoReparacion = tiempoReparacion;
        }

        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "No puede ser el nombre mayor de 20 caracteres")]
        public string Nombre { get; set; }
        [StringLength(20, ErrorMessage = "No puede ser el material mayor de 20 caracteres")]
        public string Material { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, float.MaxValue, ErrorMessage = "Precio minimo is 1 ")]
        [Display(Name = "Precio de compra")]
        public double Precio { get; set; }
        public string TiempoReparacion { get; set; }
    }
}
