using System;

namespace AppForSEII2526.API.DTOs.HerramientaDTO
{
    public class HerramientaComprarDTO : IEquatable<HerramientaComprarDTO>
    {
        public HerramientaComprarDTO(int id, string nombre, string material, decimal precio, string fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Fabricante = fabricante;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public decimal Precio { get; set; }
        public string Fabricante { get; set; }

        public override bool Equals(object? obj) => Equals(obj as HerramientaComprarDTO);

        public bool Equals(HerramientaComprarDTO? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id
                && string.Equals(Nombre, other.Nombre, StringComparison.Ordinal)
                && string.Equals(Material, other.Material, StringComparison.Ordinal)
                && Precio == other.Precio
                && string.Equals(Fabricante, other.Fabricante, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Material, Precio, Fabricante);
        }

        public static bool operator ==(HerramientaComprarDTO? left, HerramientaComprarDTO? right) =>
            EqualityComparer<HerramientaComprarDTO>.Default.Equals(left, right);

        public static bool operator !=(HerramientaComprarDTO? left, HerramientaComprarDTO? right) => !(left == right);
    }
}