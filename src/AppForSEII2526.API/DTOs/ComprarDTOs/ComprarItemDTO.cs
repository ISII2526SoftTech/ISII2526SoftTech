using AppForSEII2526.API.Models;
using Humanizer.Localisation;

namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarItemDTO
    {
        public ComprarItemDTO(int cantidad, string descripcion, string nombre, string material, decimal precio)
        {

            Cantidad = cantidad;
            Descripcion = descripcion;
            Nombre = nombre;
            Material = material;
            Precio = precio;



        }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public decimal Precio { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ComprarItemDTO dTO &&
                   Cantidad == dTO.Cantidad &&
                   Descripcion == dTO.Descripcion &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Precio == dTO.Precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cantidad, Descripcion, Material, Precio);
        }
    }
}
