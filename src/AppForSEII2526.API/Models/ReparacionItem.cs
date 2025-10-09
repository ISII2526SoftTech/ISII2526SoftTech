namespace AppForSEII2526.API.Models
{
    public class ReparacionItem
    {
        [Key]
        public int IdReparacion { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public string? Descripcion { get; set; }
        public string IdHerramienta { get; set; }
        public Reparacion Reparacion { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float Precio { get; set; }

        public Herramienta herramienta
        {
            get => default;
            set
            {
            }
        }

        public ReparacionItem()
        {
        }

        public ReparacionItem(int cantidad, string descripcion, string idHerramienta,
                             int idReparacion, float precio, Reparacion reparacion)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            IdHerramienta = idHerramienta;
            IdReparacion = idReparacion;
            Precio = precio;
            Reparacion = reparacion;
        }


        public float CalcularSubtotal()
        {
            return Cantidad * Precio;
        }


    }
}
