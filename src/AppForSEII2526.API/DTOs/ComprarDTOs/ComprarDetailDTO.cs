namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarDetailDTO
    {
        private string direccionEnvio;
        private IList<CompraItem> comprarItem;
        private List<ComprarItemDTO> comprarItemDTOs;

        public ComprarDetailDTO(string nombreCliente, string apellidoCLiente, string direccion, DateTime fechaCompra, decimal precioTotal, IList<ComprarItemDTO> comprarItem)
        {

            NombreCliente = nombreCliente;
            ApellidoCLiente = apellidoCLiente;
            Direccion = direccion;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
            PrecioTotal = precioTotal;
            ComprarItem = comprarItem;

        }


        [Display(Name = "Nombre")]
        public string NombreCliente { get; set; }

        [Display(Name = "Apellidos")]
        public string ApellidoCLiente { get; set; }

        public string Direccion { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public IList<ComprarItemDTO> ComprarItem { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ComprarDetailDTO dTO &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCLiente == dTO.ApellidoCLiente &&
                   Direccion == dTO.Direccion &&
                   PrecioTotal == dTO.PrecioTotal &&
                   FechaCompra == dTO.FechaCompra &&
                   EqualityComparer<IList<ComprarItemDTO>>.Default.Equals(ComprarItem, dTO.ComprarItem);
        }
    }
}
