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
        By createOfertaButtonBy = By.Id("createOfertaButton");
        public GetSelectOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void SearchHerramientas(string fabricante, double precioMax)
        {
            try
            {
                WaitForBeingClickable(inputPrecioMax);
                _driver.FindElement(inputPrecioMax).SendKeys(precioMax.ToString());
                SelectElement selectElement = new SelectElement(_driver.FindElement(inputFabricante));
                selectElement.SelectByText(fabricante);

                _driver.FindElement(buttonSearchHerramientas).Click();
            }
            catch (Exception ex)
            {
            }
        }


        public void SelectHerramienta(string herramienta)
        {
            By seleeccionarButton = By.Id("herramientaToSelect_" + herramienta);
            WaitForBeingClickable(seleeccionarButton);
            _driver.FindElement(seleeccionarButton).Click();
        }
        public void DeSelectHerramienta(string herramienta)
        {
            var quitarButton = By.XPath(
                $"//div[contains(@class,'row mb-2') and .//span[contains(normalize-space(.), \"{herramienta}\")]]" +
                "//button[starts-with(@id, 'removeHerramienta')]");

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
            try
            {
                Thread.Sleep(500);
                var table = _driver.FindElement(tableOfHerramientasBy);
                var rows = table.FindElements(By.TagName("tr"));
                for (int i = 1; i < rows.Count; i++)
                {
                    var cells = rows[i].FindElements(By.TagName("td"));
                    if (cells.Count >= 4)
                    {
                        string nombre = cells[0].Text.Trim();
                        string fabricante = cells[1].Text.Trim();
                        string material = cells[2].Text.Trim();
                        string precio = cells[3].Text.Trim();
                        foreach (var expected in expectedHerramientas)
                        {
                            string expectedNombre = expected[0];
                            string expectedMaterial = expected[1];
                            string expectedFabricante = expected[2];
                            string expectedPrecio = expected[3];
                            string precioNormalizado = NormalizarPrecio(precio);
                            string expectedPrecioNormalizado = NormalizarPrecio(expectedPrecio);

                            bool nombreCoincide = nombre.Equals(expectedNombre, StringComparison.OrdinalIgnoreCase);
                            bool fabricanteCoincide = fabricante.Equals(expectedFabricante, StringComparison.OrdinalIgnoreCase);
                            bool materialCoincide = material.Equals(expectedMaterial, StringComparison.OrdinalIgnoreCase);
                            bool precioCoincide = precioNormalizado == expectedPrecioNormalizado;

                            if (nombreCoincide && fabricanteCoincide && materialCoincide && precioCoincide)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _output.WriteLine($"ERROR en CheckListOfHerramientas: {ex.Message}");
                _output.WriteLine($"Stack: {ex.StackTrace}");
                return false;
            }
        }





        // METODO AUXILIAR PARA NORMALIZAR PRECIOS NO EVALUAR










        private string NormalizarPrecio(string precio)
        {
            try
            {
                _output.WriteLine($"Normalizando precio: '{precio}'");

                if (string.IsNullOrWhiteSpace(precio))
                    return "0.00";
                string normalizado = precio
                    .Replace("€", "")
                    .Replace("$", "")
                    .Trim();
                normalizado = normalizado.Replace(",", ".");
                int lastDotIndex = normalizado.LastIndexOf('.');
                if (lastDotIndex > 0)
                {
                    string beforeLastDot = normalizado.Substring(0, lastDotIndex).Replace(".", "");
                    string afterLastDot = normalizado.Substring(lastDotIndex);
                    normalizado = beforeLastDot + afterLastDot;
                }
                if (decimal.TryParse(normalizado,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal resultado))
                {
                    string final = resultado.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    _output.WriteLine($"Precio normalizado: '{final}'");
                    return final;
                }

                _output.WriteLine($"No se pudo parsear precio, retornando original: '{precio}'");
                return precio;
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error normalizando precio: {ex.Message}");
                return precio;
            }
        }
    }
}