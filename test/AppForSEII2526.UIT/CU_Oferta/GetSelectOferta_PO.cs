using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace AppForSEII2526.UIT.UC_Rental
{
    public class GetSelectOferta_PO : PageObject
    {
        By inputFabricante = By.Id("inputFabricante");
        By inputPrecioMax = By.Id("precioMaxHerramienta");
        By buttonSearchHerramientas = By.Id("searchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        public GetSelectOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string fabricante, string precioMax)
        {
            WaitForBeingClickable(inputFabricante);
            _driver.FindElement(inputFabricante).SendKeys(fabricante);
            _driver.FindElement(inputPrecioMax).SendKeys(precioMax);
            SelectElement selectElement = new SelectElement(_driver.FindElement(inputPrecioMax));
            selectElement.SelectByText(precioMax);
            _driver.FindElement(buttonSearchHerramientas).Click();


        }
        public bool CheckListOfMovies(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }
    }
}