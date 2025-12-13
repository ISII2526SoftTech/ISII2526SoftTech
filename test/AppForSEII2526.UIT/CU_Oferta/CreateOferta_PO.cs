using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Oferta
{
    public class CreateOferta_PO : PageObject
    {
        public CrearOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }
        By NombreUsuario = By.Id("Name");
        By FechaInicio = By.Id("FechaInicio");
        By FechaFinal = By.Id("FechaFinal");
        By MetodoPago = By.Id("MetodoPago");
        By DirigidaA = By.Id("DirigidaA");

        By OfertarHerramientas = By.Id("Submit");
        By dialogOk = By.Id("Button_DialogOK");
        By modificarOferta = By.Id("ModifyHerramientas");
        By TablaOfertaItemBy = By.Id("TableOfOfertaItems");


        public void PonerDatosOferta(string nombre, DateTime fechaInicio,DateTime fechaFinal, string metodoDePago, string dirigidaA)
        {
            WaitForBeingClickable(NombreUsuario);
            _driver.FindElement(NombreUsuario).SendKeys(nombre);
            WaitForBeingClickable(FechaInicio);
            _driver.FindElement(FechaInicio).SendKeys(fechaInicio.ToString());
            WaitForBeingClickable(FechaFinal);
            _driver.FindElement(FechaFinal).SendKeys(fechaFinal.ToString());
            WaitForBeingClickable(MetodoPago);
            var selectElement = new SelectElement(_driver.FindElement(MetodoPago));
            selectElement.SelectByValue(metodoDePago);
            WaitForBeingClickable(DirigidaA);
            selectElement = new SelectElement(_driver.FindElement(DirigidaA));
            selectElement.SelectByValue(dirigidaA);
        }

        public void SubmitOferta()
        {
            WaitForBeingClickable(OfertarHerramientas);
            _driver.FindElement(OfertarHerramientas).Click();
        }

        public void ConfirmarOferta()
        {
            WaitForBeingClickable(dialogOk);
            _driver.FindElement(dialogOk).Click();


        }

        public void ModificarOferta()
        {
            WaitForBeingClickable(modificarOferta);
            _driver.FindElement(modificarOferta).Click();
        }


        public bool CheckError(string expectedError)
        {

            return _driver.PageSource.Contains(expectedError);

        }

        public bool CheckListaHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, TablaOfertaItemBy);
        }

        


    
}
}
