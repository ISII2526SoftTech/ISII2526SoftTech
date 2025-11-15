using NuGet.Versioning;

namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(ReparacionId), nameof(HerramientaId))]
    public class ReparacionItem
    {
        public int ReparacionId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public string? Descripcion { get; set; }
        public virtual Reparacion Reparacion { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float Precio { get; set; }

        public Herramienta Herramienta { get; set; }

        public int HerramientaId { get; set; }

        public ReparacionItem()
        {
        }

        public ReparacionItem(Herramienta herramienta, string descripcion, int cantidad, float precio)
        {
            Herramienta = herramienta;
            HerramientaId = herramienta.Id;
            //Reparacion = reparacion;
            //ReparacionId = reparacion.Id;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio; //AMO
        }

        public ReparacionItem(Herramienta herramienta, Reparacion reparacion, string descripcion, int cantidad, float precio)
        //public ReparacionItem(Herramienta herramienta, string descripcion, int cantidad, float precio)
        {
            Herramienta = herramienta;
            HerramientaId = herramienta.Id;
            Reparacion = reparacion;
            ReparacionId = reparacion.Id;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio; //AMO
        }


        public float CalcularSubtotal()
        {
            return Cantidad * Precio;
        }


    }
}
