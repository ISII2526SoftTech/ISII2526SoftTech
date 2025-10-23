namespace AppForSEII2526.API.DTOs.FabricanteDTO
{
    public class FabricanteDTO
    {
        public FabricanteDTO()
        {
        }

        public FabricanteDTO(string nombre)
        {
            Nombre = nombre;

        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El fabricante no puede ser más largo de 50 carácteres", MinimumLength = 3)]
        public string Nombre { get; set; }

    }
}
