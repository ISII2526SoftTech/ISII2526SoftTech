using System.Collections.Generic;

namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaDetailDTO : OfertaForCreateDTO
    {
        //Mostrar los detalle de la oferta YA CREADA
        public int Id { get; set; }
        public List<double> PrecioTotalOriginal { get; set; } = new List<double>();
        public List<double> PrecioTotalConOferta { get; set; } = new List<double>();

        public OfertaDetailDTO(DateTime fechainicio, DateTime fechafin, TiposMetodoPago metodoPago, IList<OfertaItemDTO> items,
            int id)
            : base(fechainicio, fechafin, metodoPago, items)
            {
                Id = id;
            foreach (var item in items)
            {
                double precioTotalOriginal = (double)item.Precio;
                double precioTotalOferta = precioTotalOriginal * (1 - (double)item.Porcentaje / 100);
                PrecioTotalOriginal.Add(precioTotalOriginal);
                PrecioTotalConOferta.Add(precioTotalOferta);
            }
        }  
        

        public override bool Equals(object? obj)
        {
            return obj is OfertaDetailDTO dtO &&
                base.Equals(obj) &&
                PrecioTotalConOferta == dtO.PrecioTotalConOferta &&
                PrecioTotalOriginal == dtO.PrecioTotalOriginal &&
                Id == dtO.Id &&
                CompareDate(FechaInicio, dtO.FechaInicio) &&
                CompareDate(FechaFinal, dtO.FechaFinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id, PrecioTotalOriginal, PrecioTotalConOferta);
        }

    }
}
