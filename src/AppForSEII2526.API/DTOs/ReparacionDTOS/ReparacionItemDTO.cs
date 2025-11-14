using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs.ReparaciónDTO
{
    public class ReparacionItemDTO
    {
        //Capturar los datos de los items de la reparacion
       
        public int IdHerramienta { get; set; }
        public string Descripcion { get; set; }
       
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.01, float.MaxValue, ErrorMessage = "Precio mínimo")]
        public float Precio { get; set; }
       
        public int Cantidad { get; set; }

        public ReparacionItemDTO() {
        }
        public ReparacionItemDTO(int idHerramienta, float precio, String descripcion, int cantidad)
        {
            IdHerramienta = idHerramienta;          
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
        }
        public override bool Equals(object? obj)
        {
            return obj is ReparacionItemDTO dTO &&
                   IdHerramienta == dTO.IdHerramienta &&
                   Descripcion == dTO.Descripcion &&
                   Precio == dTO.Precio &&
                   Cantidad == dTO.Cantidad;
        }

    }
}