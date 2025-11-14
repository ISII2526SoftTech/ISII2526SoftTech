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

        public ReparacionItem(Herramienta herramienta,int herramientaId, Reparacion reparacion, float precio, string? description, int cantidad)
        {
            Herramienta = herramienta;
            HerramientaId = herramientaId;
            Reparacion = reparacion;
            Precio = precio;
            Descripcion = description;
            Cantidad = cantidad;
        }
        public ReparacionItem(int cantidad, string descripcion,
                             int Repairid, float precio, Reparacion reparacion,Herramienta herramienta)
        {   
            ReparacionId = Repairid;
            Cantidad = cantidad;
            Descripcion = descripcion;          
            Precio = precio;
            Reparacion = reparacion;
            Herramienta = herramienta;
        }
        public ReparacionItem(Herramienta herramienta,string descripcion,int cantidad,float precio)
        {
            Herramienta = herramienta;  
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
        }


        public float CalcularSubtotal()
        {
            return Cantidad * Precio;
        }


    }
}
