    namespace AppForSEII2526.API.DTOs.HerramientaDTO
{
    public class HerramientaComprarDTO
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

       
    }
}
