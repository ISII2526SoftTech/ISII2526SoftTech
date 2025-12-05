using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReparacionStateContainer
    {
        public ReparacionForCreateDTO Reparacion { get; private set; } = new ReparacionForCreateDTO()
        {
            ReparacionItem = new List<ReparacionItemDTO>()
        };

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddReparacionItem(ReparacionItemDTO item)
        {
            if (!Reparacion.ReparacionItem.Any(ri => ri.IdHerramienta == item.IdHerramienta))
            {
                Reparacion.ReparacionItem.Add(new ReparacionItemDTO(){
                    IdHerramienta = item.IdHerramienta,
                    Descripcion = item.Descripcion,
                    PrecioUnitario = item.PrecioUnitario,
                    Cantidad = item.Cantidad
                });
            }
            ;
                
        }

        public void RemoveReparacionItem(ReparacionItemDTO item)
        {
            var itemToRemove = Reparacion.ReparacionItem.FirstOrDefault(ri => ri.IdHerramienta == item.IdHerramienta);
            if (itemToRemove != null)
            {
                Reparacion.ReparacionItem.Remove(itemToRemove);
            }
        }

        public void ClearReparacionItems()
        {
            Reparacion.ReparacionItem.Clear();  
        }       

        public void ReparacionProcessed()
        {
            Reparacion = new ReparacionForCreateDTO()
            {
                ReparacionItem = new List<ReparacionItemDTO>()
            };
        }

    }
}
