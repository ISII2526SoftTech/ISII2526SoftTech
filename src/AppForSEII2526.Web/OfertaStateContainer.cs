using AppForSEII2526.Web.API;
using Humanizer.DateTimeHumanizeStrategy;

namespace AppForSEII2526.Web
{
    public class OfertaStateContainer
    {
        public OfertaForCreateDTO Oferta { get; set; } = new OfertaForCreateDTO()
        {
            OfertaItems = new List<OfertaItemDTO>()
        };

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddOfertaItem(OfertaItemDTO herramienta)
        {
            if(!Oferta.OfertaItems.Any(oi => oi.HerramientaId == herramienta.HerramientaId))
            {
                Oferta.OfertaItems.Add(new OfertaItemDTO()
                {
                    HerramientaId = herramienta.HerramientaId,                    
                    Porcentaje = herramienta.Porcentaje,
                    PrecioOriginal = herramienta.PrecioOriginal,
                    PrecioFinal = herramienta.PrecioFinal
                });
            }           
        }

        public void RemoveOfertaItem(OfertaItemDTO herramienta)
        {
            var itemToRemove = Oferta.OfertaItems.FirstOrDefault(oi => oi.HerramientaId == herramienta.HerramientaId);
            if (itemToRemove != null)
            {
                Oferta.OfertaItems.Remove(itemToRemove);
            }
        }
        public void ClearOfertaItems()
        {
            Oferta.OfertaItems.Clear();
        }
        public void OfertaProcessed()
        {
            Oferta = new OfertaForCreateDTO()
            {
                OfertaItems = new List<OfertaItemDTO>()
            };
        }
    }
}
