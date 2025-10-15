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

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede ser más largo de 50 carácteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        //RELACIONES
        public IList<Herramienta> Herramientas { get; set; } = new List<Herramienta>();

    }
}