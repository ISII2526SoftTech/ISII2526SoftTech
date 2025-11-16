using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppForSEII2526.API.DTOs.ComprarDTOs
{
    public class ComprarDetailDTO : IEquatable<ComprarDetailDTO>
    {
        public ComprarDetailDTO(string nombreCliente, string apellidoCLiente, string direccion, DateTime fechaCompra, decimal precioTotal, IList<ComprarItemDTO> comprarItem)
        {
            NombreCliente = nombreCliente;
            ApellidoCLiente = apellidoCLiente;
            Direccion = direccion;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
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

        public override bool Equals(object? obj) => Equals(obj as ComprarDetailDTO);

        public bool Equals(ComprarDetailDTO? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            // Comparación por propiedades escalares
            var sameScalars =
                NombreCliente == other.NombreCliente &&
                ApellidoCLiente == other.ApellidoCLiente &&
                Direccion == other.Direccion &&
                PrecioTotal == other.PrecioTotal &&
                FechaCompra == other.FechaCompra;

            if (!sameScalars) return false;

            // Comparación por contenido de la lista (SequenceEqual usa ComprarItemDTO.Equals)
            if (ComprarItem == null && other.ComprarItem == null) return true;
            if (ComprarItem == null || other.ComprarItem == null) return false;

            return ComprarItem.SequenceEqual(other.ComprarItem);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(NombreCliente);
            hash.Add(ApellidoCLiente);
            hash.Add(Direccion);
            hash.Add(PrecioTotal);
            hash.Add(FechaCompra);

            if (ComprarItem != null)
            {
                foreach (var item in ComprarItem)
                    hash.Add(item);
            }

            return hash.ToHashCode();
        }
    }
}