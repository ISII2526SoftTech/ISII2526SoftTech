namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaForCreateDTO
    {
        //Capturar los datos para crear una oferta
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        [Required]
        public TiposMetodoPago MetodoPago { get; set; }
        
        public TiposDirigidaOferta DirigidaA { get; set; } 
        public IList<OfertaItemDTO> Items { get; set; }

        public OfertaForCreateDTO()
        {
            Items = new List<OfertaItemDTO>();
        }

        public OfertaForCreateDTO(DateTime fechainicio, DateTime fechafinal, TiposMetodoPago metodoPago, IList<OfertaItemDTO> items)
        {
            FechaFinal = fechafinal;
            FechaInicio = fechainicio;
            MetodoPago = metodoPago;
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }
        public OfertaForCreateDTO(DateTime fechainicio, DateTime fechafinal, TiposMetodoPago metodoPago, IList<OfertaItemDTO> items, TiposDirigidaOferta dirigidaA)
        {
            FechaFinal = fechafinal;
            FechaInicio = fechainicio;
            MetodoPago = metodoPago;
            Items = items ?? throw new ArgumentNullException(nameof(items));
            DirigidaA = dirigidaA;
        }
        public override bool Equals(object? obj)
        {
            return obj is OfertaForCreateDTO dto &&
                FechaFinal == dto.FechaFinal &&
                FechaInicio == dto.FechaInicio &&
                MetodoPago == dto.MetodoPago &&
                ((DirigidaA == null && dto.DirigidaA == null) || (DirigidaA != null && DirigidaA.Equals(dto.DirigidaA))) &&
                Items.SequenceEqual(dto.Items);
        }

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }
    }
}
