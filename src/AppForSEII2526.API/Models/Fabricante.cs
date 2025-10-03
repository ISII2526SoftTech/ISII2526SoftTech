namespace AppForSEII2526.API.Models
{
 
    [Index(nameof(Name), IsUnique = true)]
    public class Fabricante
    {
        public Fabricante()
        {
            Herramientas = new List<Herramienta>();
        }

        public Fabricante(string name)
        {
            Name = name;
            
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede ser más largo de 50 carácteres", MinimumLength = 3)]
        public string Name { get; set; }

        //RELACIONE
        public IList<Herramienta> Herramientas { get; set; } = new List<Herramienta>();

    }
}