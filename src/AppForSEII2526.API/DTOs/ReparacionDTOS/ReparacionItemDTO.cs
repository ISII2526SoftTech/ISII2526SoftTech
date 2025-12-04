using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ReparaciónDTO
{
    public class ReparacionItemDTO
    {
        public int IdHerramienta { get; set; }

        // --- NUEVOS CAMPOS AÑADIDOS ---
        public string NombreHerramienta { get; set; }
        public string NombreFabricante { get; set; }
        public string TiempoReparacion { get; set; } 
        // ------------------------------

        public string Descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float PrecioUnitario { get; set; } // Nombre cambiado
        public float PrecioTotal { get; set; }

        public int Cantidad { get; set; }

        public ReparacionItemDTO() { }

        public ReparacionItemDTO(int idHerramienta, float precio, String descripcion, int cantidad)
        {
            IdHerramienta = idHerramienta;
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioUnitario = precio;
        }

        public ReparacionItemDTO(int idHerramienta, string nombreHerramienta, string nombreFabricante, string tiempoReparacion, float precioUnitario, float precioTotal, string descripcion, int cantidad)
        {
            IdHerramienta = idHerramienta;
            NombreHerramienta = nombreHerramienta; // Nuevo
            NombreFabricante = nombreFabricante;   // Nuevo
            TiempoReparacion = tiempoReparacion;   // Nuevo
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            PrecioTotal = precioTotal;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO dTO &&
                   IdHerramienta == dTO.IdHerramienta &&
                   NombreHerramienta == dTO.NombreHerramienta && 
                   Descripcion == dTO.Descripcion &&
                   PrecioUnitario == dTO.PrecioUnitario &&
                   Cantidad == dTO.Cantidad;
        }

        public float CalcularSubtotal()
        {
            return Cantidad * PrecioUnitario;
        }
    }
}