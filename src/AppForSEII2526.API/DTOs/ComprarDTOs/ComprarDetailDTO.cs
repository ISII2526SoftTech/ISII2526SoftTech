namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarDetailDTO : ComprarForCreateDTO
    {
        public ComprarDetailDTO(string nombreCliente, string apellidoCliente, object value)
        {
            NombreCliente = nombreCliente;
            ApellidoCliente = apellidoCliente;
        }

        public ComprarDetailDTO(int id, string nombreCliente, string apellidoCliente, string direccion, DateTime fechaCompra, IList<ComprarItemDTO> comprarItem, decimal precioTotal)
                   : base(nombreCliente,
                          apellidoCliente,
                          direccion,
                          fechaCompra,
                          comprarItem,
                          precioTotal
                         )
        {
            Id = id;
        }
        public ComprarDetailDTO( string nombreCliente, string apellidoCliente, string direccion, DateTime fechaCompra, IList<ComprarItemDTO> comprarItem, decimal precioTotal)
                  : base(nombreCliente,
                         apellidoCliente,
                         direccion,
                         fechaCompra,
                         comprarItem,
                         precioTotal
                        )
        { }

        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ComprarDetailDTO dTO &&
                   base.Equals(obj) &&
                   Id == dTO.Id &&
                   Direccion == dTO.Direccion &&
                   NombreCliente == dTO.NombreCliente &&
                   ApellidoCliente == dTO.ApellidoCliente &&
                   FechaCompra == dTO.FechaCompra &&
                   PrecioTotal == dTO.PrecioTotal &&
                   EqualityComparer<IList<ComprarItemDTO>>.Default.Equals(ComprarItem, dTO.ComprarItem);


        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Direccion);
            hash.Add(NombreCliente);
            hash.Add(ApellidoCliente);
            hash.Add(FechaCompra);
            hash.Add(PrecioTotal);
            hash.Add(ComprarItem);
            return hash.ToHashCode();
        }
    }
}
