namespace AppForSEII2526.API.Models
{
    public class CompraItem
    {

        public int Id { get; set; }
        public int cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede ser mas larga de 200 caracteres", MinimumLength = 5)]
        public string descripcion { get; set; }
        public int idCompra { get; set; }
        public int idHerramienta { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal precio { get; set; }

        public Comprar Compra { get; set; }

        public AppForSEII2526.API.Models.Herramienta herramienta
        {
            get => default;
            set
            {
            }
        }

        public CompraItem()
        {
        }

        public CompraItem(int cantidad, string descripcion, int idCompra,
                         int idHerramienta, decimal precio)
        {
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.idCompra = idCompra;
            this.idHerramienta = idHerramienta;
            this.precio = precio;
        }
    }
}
