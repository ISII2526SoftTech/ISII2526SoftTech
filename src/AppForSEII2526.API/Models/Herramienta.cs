namespace AppForSEII2526.API.Models
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Herramienta
    {
        public Herramienta()
        {
            CompraItems = new List<CompraItem>();
            OfertaItems = new List<OfertaItem>();
        }

        public Herramienta(string nombre, Fabricante fabricante, double precio, string material, string tiempoReparacion)
        {
            Nombre = nombre;
            Precio = precio;
            Fabricante = fabricante;
            Material = material;
            CompraItems = new List<CompraItem>();
            OfertaItems = new List<OfertaItem>();
            TiempoReparacion = tiempoReparacion;
        }

        public Herramienta(int id, string tiempoReparacion, string nombre, string material, double precio, List<CompraItem> compraItems, List<OfertaItem> ofertaItems, List<ReparacionItem> itemsReparacion, Fabricante fabricante)
        {
            Id = id;
            TiempoReparacion = tiempoReparacion;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            CompraItems = compraItems;
            OfertaItems = ofertaItems;
            ItemsReparacion = itemsReparacion;
            Fabricante = fabricante;
        }

        public int Id { get; set; }
        public string TiempoReparacion { get; set; }

        [StringLength(50, ErrorMessage = "El nombre de la herramienta no puede ser mas largo de 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(20, ErrorMessage = "El nombre del material no puede ser mas largo de 20 caracteres")]
        public string Material { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        [Display(Name = "Precio de compra")]
        [Precision(10, 2)]
        public double Precio { get; set; }



        //RELACIONES
        public virtual List<CompraItem> CompraItems { get; set; }
        public virtual List<OfertaItem> OfertaItems { get; set; }

        public List<ReparacionItem> ItemsReparacion
        {
            get => default;
            set
            {
            }
        }

        public Fabricante Fabricante { get; set; }
    }
}
