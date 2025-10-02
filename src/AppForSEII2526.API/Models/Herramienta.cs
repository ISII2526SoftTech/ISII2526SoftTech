namespace AppForSEII2526.API.Models
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Herramienta
    {
        public Herramienta()
        {
        }

        public Herramienta(string nombre, Fabricante fabricante, double precio, string material)
        {
            Nombre = nombre;
            Precio = (decimal)precio;
            Fabricante = fabricante;
            Material = material;
        }

        public int Id { get; set; }


        [StringLength(50, ErrorMessage = "El nombre de la herramienta no puede ser mas largo de 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(20, ErrorMessage = "El nombre del material no puede ser mas largo de 20 caracteres")]
        public string Material { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        [Display(Name = "Precio de compra")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        private Fabricante Fabricante;
    }
}
