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
        By inputPrecioMax = By.Id("inputPrecioMax");
        By buttonSearchHerramientas = By.Id("searchHerramientas");
        By tableOfHerramientasBy = By.Id("TableOfHerramientas");
        By createOfertaButtonBy = By.Id("createOfertaButton");
        public GetSelectOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string fabricante, double precioMax)
        {
            WaitForBeingClickable(inputFabricante);
            _driver.FindElement(inputFabricante).SendKeys(fabricante);
            WaitForBeingClickable(inputPrecioMax);
            _driver.FindElement(inputPrecioMax).SendKeys(precioMax.ToString());
            _driver.FindElement(buttonSearchHerramientas).Click();


        }
        public void SelectHerramienta(string herramienta)
        {
            By seleeccionarButton = By.Id("herramientaToSelect_" + herramienta);
            WaitForBeingClickable(seleeccionarButton);
            _driver.FindElement(seleeccionarButton).Click();
        }
        public void DeSelectHerramienta(string herramienta)
        {
            By quitarButton = By.Id("removeHerramienta_" + herramienta);
            WaitForBeingClickable(quitarButton);
            _driver.FindElement(quitarButton).Click();
        }
        public void OfertarHerramientas()
        {
            WaitForBeingClickable(createOfertaButtonBy);
            _driver.FindElement(createOfertaButtonBy).Click();
        }
        public void ModifyRentingCart(string herramienta)
        {
            WaitForBeingVisible(By.Id($"removeHerramienta_{herramienta}"));
            _driver.FindElement(By.Id($"removeHerramienta_{herramienta}")).Click();

        }

        public bool OfertarHerramientaNoDisponible()
        {
            try
            {
                return _driver.FindElement(createOfertaButtonBy).Displayed == false;
            }
            catch (Exception ex)
            {
                return true; 
            }


        }

        public void ponerPorcentaje(int id, int porcentaje)
        {
            By porcentajeBy = By.Id("porcentaje_" + id);
            WaitForBeingClickable(porcentajeBy);
            _driver.FindElement(porcentajeBy).Clear();
            _driver.FindElement(porcentajeBy).SendKeys(porcentaje.ToString());
        }
        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

    }
}