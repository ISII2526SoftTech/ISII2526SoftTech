using System.Collections.Generic;

namespace AppForSEII2526.API.DTOs.OfertaDTOs
{
    public class OfertaDetailDTO : OfertaForCreateDTO
    {
        //Mostrar los detalle de la oferta YA CREADA
        public int Id { get; set; }
        public List<double> PrecioTotalOriginal { get; set; } = new List<double>();
        public List<double> PrecioTotalConOferta { get; set; } = new List<double>();

        public OfertaDetailDTO(DateTime fechainicio, DateTime fechafin, TiposMetodoPago metodoPago, IList<OfertaItemDTO> ofertaitems,
            int id, TiposDirigidaOferta dirigidaA)
            : base(fechainicio, fechafin, metodoPago, ofertaitems)
            {
                Id = id;
                DirigidaA = dirigidaA;

            foreach (var item in ofertaitems)
            {
                PrecioTotalOriginal.Add(item.PrecioOriginal);  
                PrecioTotalConOferta.Add(item.PrecioFinal);        
            }

        }



        public OfertaDetailDTO(DateTime fechainicio, DateTime fechafin, TiposMetodoPago metodoPago,
            int id, TiposDirigidaOferta dirigidaA)
            : base(fechainicio, fechafin, metodoPago, dirigidaA)
        {
            Id = id;
            DirigidaA = dirigidaA;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            OfertaDetailDTO other = (OfertaDetailDTO)obj;
            bool itemsEqual;
            
            if (OfertaItems == null && other.OfertaItems == null)
            {
                itemsEqual = true;
            }
            else if (OfertaItems == null || other.OfertaItems == null)
            {
                itemsEqual = false;
            }
            else
            {
                itemsEqual = OfertaItems.SequenceEqual(other.OfertaItems);
            }
            
            return Id == other.Id &&
                   CompareDate(FechaInicio, other.FechaInicio) &&
                   CompareDate(FechaFinal, other.FechaFinal) &&
                   MetodoPago == other.MetodoPago &&
                   DirigidaA == other.DirigidaA &&
                   itemsEqual;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + FechaInicio.GetHashCode();
                hash = hash * 23 + FechaFinal.GetHashCode();
                hash = hash * 23 + MetodoPago.GetHashCode();
                hash = hash * 23 + DirigidaA.GetHashCode();
                hash = hash * 23 + (OfertaItems?.GetHashCode() ?? 0);
                return hash;
            }
        }

    }
}
