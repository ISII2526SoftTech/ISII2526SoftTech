using AppForSEII2526.Web.API;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis;
namespace AppForSEII2526.Web
{
    public class ComprarStateContainer
    {
        //Creamos una instancia de Compra cuando se crea una instancia de CompraStateContainer
        public ComprarForCreateDTO Compra { get; private set; } = new ComprarForCreateDTO()
        {
            ComprarItem = new List<ComprarItemDTO>()
        };
        //Calculamos el Precio Total de las herramientas que hemos seleccionado para comprarlas
        public double PrecioTotal
        {
            get
            {
                return Compra.ComprarItem.Sum(item => item.Precio * item.Cantidad);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramienta(HerramientaComprarDTO herramienta)
        {
            //Antes de agregar una herramienta, verificamos si ya se ha agregado.
            if (!Compra.ComprarItem.Any(h => h.Nombre == herramienta.Nombre))
                //Lo agregamos si no está en la lista.
                Compra.ComprarItem.Add(new ComprarItemDTO()
                {
                    Nombre = herramienta.Nombre,
                    Precio = herramienta.Precio,
                    Material = herramienta.Material,
                    //Cantidad = 1,
                    //Descripcion = ""
                }
            );
        }

        //Eliminar herramienta de la lista de herramientas seleccionadas
        public void EliminarHerramienta(ComprarItemDTO item)
        {
            Compra.ComprarItem.Remove(item);
        }

        //Eliminamos todas las herramientas de la lista
        public void EliminarTodasLasHerramientas()
        {
            Compra.ComprarItem.Clear();
        }

        //Ya hemos finalizado el proceso de compra, por lo tanto, creamos una nueva Compra
        public void FinalizarCompra()
        {
            Compra = new ComprarForCreateDTO()
            {
                ComprarItem = new List<ComprarItemDTO>()
            };
        }
    }
}