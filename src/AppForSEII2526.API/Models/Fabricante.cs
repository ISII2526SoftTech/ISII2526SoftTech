namespace AppForSEII2526.API.Models
{
 
    [Index(nameof(Name), IsUnique = true)]
    public class Fabricante
    {
        public Fabricante()
        {
        }

        public Fabricante(string name)
        {
            Name = name;
            
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede ser m�s largo de 50 car�cteres", MinimumLength = 3)]
        public string Name { get; set; }

     
        public IList<Herramienta> Herramientas { get; set; } = new List<Herramienta>();

    }
}