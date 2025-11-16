using System;
using System.ComponentModel.DataAnnotations;

namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarItemDTO : IEquatable<ComprarItemDTO>
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

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public decimal Precio { get; set; }

        public override bool Equals(object? obj) => Equals(obj as ComprarItemDTO);

        public bool Equals(ComprarItemDTO? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Cantidad == other.Cantidad &&
                   Descripcion == other.Descripcion &&
                   Nombre == other.Nombre &&
                   Material == other.Material &&
                   Precio == other.Precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cantidad, Descripcion, Nombre, Material, Precio);
        }
    }
}