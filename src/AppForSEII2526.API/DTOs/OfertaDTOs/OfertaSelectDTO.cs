namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaSelectDTO
    {
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
        public Fabricante Fabricante { get; set; }

        public OfertaSelectDTO(int id, string nombre, string material, double precio, Fabricante fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
        }
    }
}
