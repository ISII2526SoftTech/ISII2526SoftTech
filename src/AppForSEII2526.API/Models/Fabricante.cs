
namespace AppForSEII2526.API.Models
{
 
    [Index(nameof(Nombre), IsUnique = true)]
    public class Fabricante
    {
        public Fabricante()
        {
            Herramientas = new List<Herramienta>();
        }

        public Fabricante(string nombre)
        {
            Nombre = nombre;
            
        }
        public Fabricante(int id,string nombre)
        {
            id = Id;
            Nombre = nombre;

        }
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede ser más largo de 50 carácteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        //RELACIONES
        public IList<Herramienta> Herramientas { get; set; } = new List<Herramienta>();

        public override bool Equals(object? obj)
        {
            return obj is Fabricante fabricante &&
                   Id == fabricante.Id &&
                   Nombre == fabricante.Nombre;
                  
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (Nombre?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}