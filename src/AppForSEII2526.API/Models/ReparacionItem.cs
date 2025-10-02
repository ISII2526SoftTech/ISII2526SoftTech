namespace AppForSEII2526.API.Models
{
    public class ReparacionItem
    {
        // Propiedades/Atributos
        [Key]
        public int IdReparacion { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public string IdHerramienta { get; set; }
        public float Precio { get; set; }

        // Constructor vacío
        public ReparacionItem()
        {
        }

        // Constructor con todos los parámetros
        public ReparacionItem(int cantidad, string descripcion, string idHerramienta,
                             int idReparacion, float precio)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            IdHerramienta = idHerramienta;
            IdReparacion = idReparacion;
            Precio = precio;
        }

        // Constructor sin ID de reparación (para cuando se asigna después)
        public ReparacionItem(int cantidad, string descripcion, string idHerramienta, float precio)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            IdHerramienta = idHerramienta;
            Precio = precio;
        }

        // Método para calcular el subtotal (cantidad * precio)
        public float CalcularSubtotal()
        {
            return Cantidad * Precio;
        }
    }
}
