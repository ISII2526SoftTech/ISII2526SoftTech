
using SQLitePCL;

namespace AppForSEII2526.API.DTOs.HerramientaDTO;

public class HerramientaOfertaDTO
{
    public HerramientaOfertaDTO()
    {
    }
    public HerramientaOfertaDTO(int id, string nombre, string material, double precio, Fabricante fabricante)
    {
        Nombre = nombre;
        Material = material;
        Precio = precio;
        Fabricante = fabricante;
    }

    [StringLength(20, ErrorMessage = "No puede ser el nombre mayor de 20 caracteres")]
    public string Nombre { get; set; }
    [StringLength(20, ErrorMessage = "No puede ser el material mayor de 20 caracteres")]
    public string Material { get; set; }

    [Required]
    [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
    [Range(1, float.MaxValue, ErrorMessage = "Precio minimo is 1 ")]
    [Display(Name = "Precio de compra")]
    public double Precio { get; set; }
    public Fabricante Fabricante { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        HerramientaOfertaDTO other = (HerramientaOfertaDTO)obj;
        return 
               Nombre == other.Nombre &&
               Material == other.Material &&
               Precio == other.Precio &&
               Fabricante.Id == other.Fabricante.Id &&
               Fabricante.Nombre == other.Fabricante.Nombre;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Nombre?.GetHashCode() ?? 0);
            hash = hash * 23 + (Material?.GetHashCode() ?? 0);
            hash = hash * 23 + Precio.GetHashCode();
            hash = hash * 23 + (Fabricante?.Id.GetHashCode() ?? 0);
            hash = hash * 23 + (Fabricante?.Nombre?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
