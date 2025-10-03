namespace AppForSEII2526.API.Models
{
    public class CompraItem
    {
        // Propiedades básicas
        public int Id { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }
        public int idCompra { get; set; }
        public int idHerramienta { get; set; }
        public decimal precio { get; set; }

        // Relación: Un CompraItem pertenece a una Compra
        public Comprar Compra { get; set; }

        // Constructores
        public CompraItem()
        {
        }

        public CompraItem(int id, int cantidad, string descripcion, int idCompra,
                         int idHerramienta, decimal precio)
        {
            Id = id;
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.idCompra = idCompra;
            this.idHerramienta = idHerramienta;
            this.precio = precio;
        }
    }
}
