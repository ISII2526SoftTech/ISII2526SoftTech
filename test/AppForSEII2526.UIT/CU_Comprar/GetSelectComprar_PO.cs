using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    public class GetSelectComprar_PO : PageObject
    {
        By inputPrecio = By.Id("inputPrecio");
        By inputMaterial = By.Id("inputMaterial");
        By buttonBuscarHerramientas = By.Id("buscarHerramientas");
        By tablaofHerramientas = By.Id("TablaHerramientas");
        By buttonComprarHerramientas = By.Id("purchaseHerraminetaButton");
        public GetSelectComprar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void BuscarHerramientas(decimal precio, string material)
        {
            //wait for the webelement to be clickable
            _driver.FindElement(inputMaterial).Clear();
            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).SendKeys(material);
            _driver.FindElement(inputPrecio).Clear();
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio.ToString());
            _driver.FindElement(buttonBuscarHerramientas).Click();

        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaofHerramientas);
        }

        public void AnadirHerramientaACarrito(string nombreHerramienta)
        {
            By botonAnadir = By.Id("herramientaparacomprar_" + nombreHerramienta);
            WaitForBeingClickable(botonAnadir);
            _driver.FindElement(botonAnadir).Click();
        }

        public void EliminarHerramientaDeCarrito(string nombreHerramienta)
        {
            By botonEliminar = By.Id("eliminarherramientas_" + nombreHerramienta);
            WaitForBeingClickable(botonEliminar);
            var element = _driver.FindElement(botonEliminar);
            var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
            actions.MoveToElement(element).Perform();
            Thread.Sleep(300);
            element.Click();
        
        }

        public bool CompraNotAvailable()
        {
            //the button is not Displayed=hidden
            try
            {
                return _driver.FindElement(buttonComprarHerramientas).Displayed == false;
            }
            catch (Exception ex)
            {
                return true;

            }
        }

        public void PulsarComprarHerramientas()
        {
            /* //POR SI NO HAY PROBLEMAS CON SCROLL
            WaitForBeingClickable(buttonComprarHerramientas);
            _driver.FindElement(buttonComprarHerramientas).Click();
            */
            //POR SI HAY PROBLEMAS CON SCROLL
            WaitForBeingClickable(buttonComprarHerramientas);
            var element = _driver.FindElement(buttonComprarHerramientas);
            var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
            actions.MoveToElement(element).Perform();
            Thread.Sleep(300);
            element.Click();
        }


    }
}