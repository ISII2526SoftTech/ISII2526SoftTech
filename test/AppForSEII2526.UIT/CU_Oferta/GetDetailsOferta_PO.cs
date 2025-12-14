using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Oferta
{
    public class GetDetailsOferta_PO : PageObject
    {
        public GetDetailsOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }
        By tablaOfertaItemBy = By.Id("HerramientasEnOferta");

        public bool CheckDetallesOfeta(string nombre, DateTime fechaInicio, DateTime fechaFinal, string metodoPago, string dirigidaA, string totalAhorro )
        {
            WaitForBeingVisible(tablaOfertaItemBy);
            bool result = true;
          
            result = result && _driver.FindElement(By.Id("Name")).Text.Contains(nombre);
            result = result && _driver.FindElement(By.Id("FechaInicio")).Text.Contains(fechaInicio.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaFinal")).Text.Contains(fechaFinal.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("TotalAhorro")).Text.Contains(totalAhorro);
            result = result && _driver.FindElement(By.Id("MetodoPago")).Text.Contains(metodoPago);
            result = result && _driver.FindElement(By.Id("DirigidaA")).Text.Contains(dirigidaA);
            return result;

        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaOfertaItemBy);
        }
    }
}
