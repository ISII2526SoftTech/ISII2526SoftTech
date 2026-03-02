
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Comprar
{
    internal class DetalleCompra_PO : PageObject
    {
        public DetalleCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }


        public bool CheckDetallesCompra(string nombre, string apellidos, string direccion, string precioTotal, DateTime fecha)
        {
            WaitForBeingClickable(By.Id("RentedMovies"));
            bool result = true;
            var nombreYApellidos = nombre + " " + apellidos;
            result = result && _driver.FindElement(By.Id("NameSurname")).Text.Contains(nombreYApellidos);
            result = result && _driver.FindElement(By.Id("DeliveryAddress")).Text.Contains(direccion);
            result = result && _driver.FindElement(By.Id("PaymentMethod")).Text.Contains(precioTotal);
            result = result && _driver.FindElement(By.Id("RentalDate")).Text.Contains(fecha.ToString("dd/MM/yyyy"));

            return result;
        }

        public bool CheckListaHerramientasCompradas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, By.Id("RentedMovies"));
        }
    }
}
