using System.ComponentModel.DataAnnotations;

namespace AppForSEII2526.API.Models
{
    public class CompraItem
    {
        public CompraItem() { }

        public CompraItem(int cantidad, string descripcion, Comprar comprar,
                          Herramienta herramienta, decimal precio)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            Comprar = comprar;
            Herramienta = herramienta;
            Precio = precio;
            HerramientaId = herramienta.Id;
            CompraId = comprar.Id;
        }

        public int Id { get; set; } // opcional si prefieres PK simple; puedes eliminar si usas PK compuesta

        public int Cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede ser mas larga de 200 caracteres", MinimumLength = 5)]
        public string? Descripcion { get; set; }

        public Comprar Comprar { get; set; }

        // FK hacia Comprar (necesaria para configurar clave compuesta)
        public int CompraId { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public decimal Precio { get; set; }

        public int HerramientaId { get; set; }
        public Herramienta Herramienta { get; set; }
    }
}