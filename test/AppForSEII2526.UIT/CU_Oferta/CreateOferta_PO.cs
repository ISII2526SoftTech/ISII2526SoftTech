using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Oferta
{
    public class CreateOferta_PO : PageObject
    {
        public CreateOferta_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
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


        public void PonerDatosOferta(string nombre, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {

                WaitForElementVisible(NombreUsuario, 10);
                var nombreElement = _driver.FindElement(NombreUsuario);
                nombreElement.Clear();
                Thread.Sleep(200);
                nombreElement.SendKeys(nombre);
                Thread.Sleep(500);
                SetBlazorInputDate(FechaInicio, fechaInicio);
                Thread.Sleep(500);
                SetBlazorInputDate(FechaFinal, fechaFinal);
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                throw;
            }
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


        private void WaitForElementVisible(By locator, int timeoutSeconds)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.Until(driver => driver.FindElement(locator).Displayed);
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error esperando elemento {locator}: {ex.Message}");
                throw;
            }
        }






        private void SetBlazorInputDate(By locator, DateTime date)
        {
            try
            {
                WaitForElementVisible(locator, 5);
                var element = _driver.FindElement(locator);

                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

                // Formato ISO para fechas: yyyy-MM-dd
                string dateString = date.ToString("yyyy-MM-dd");

                // **SOLUCIÓN PARA InputDate DE BLAZOR**
                string script = @"
                var input = arguments[0];
                var dateValue = arguments[1];
                
                // Establecer el valor directamente
                input.value = dateValue;
                
                // Disparar TODOS los eventos que Blazor necesita
                var events = ['input', 'change', 'blur', 'keydown', 'keyup', 'keypress'];
                
                events.forEach(function(eventName) {
                    input.dispatchEvent(new Event(eventName, {
                        bubbles: true,
                        cancelable: true
                    }));
                });
                
                // También disparar eventos específicos de fecha
                input.dispatchEvent(new Event('datechange', { bubbles: true }));
                
                // Forzar actualización del estado
                if (window.Blazor) {
                    // Esto es para versiones más recientes de Blazor
                    var dotNetHelper = input.blazor__instance || input._blazorInstance;
                    if (dotNetHelper) {
                        dotNetHelper.invokeMethodAsync('SetValueAsync', dateValue);
                    }
                }
                
                return input.value;
            ";

                string result = (string)js.ExecuteScript(script, element, dateString);
                _output.WriteLine($"Fecha establecida. Valor actual: {result}");

                // Verificar que se estableció
                Thread.Sleep(500);
                string currentValue = element.GetAttribute("value");
                _output.WriteLine($"Valor después de establecer: {currentValue}");

                if (string.IsNullOrEmpty(currentValue))
                {
                    _output.WriteLine("ADVERTENCIA: El valor no se estableció. Intentando método alternativo...");
                    SetBlazorInputDateAlternative(element, date);
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error en SetBlazorInputDate: {ex.Message}");
                throw;
            }
        }

        private void SetBlazorInputDateAlternative(IWebElement element, DateTime date)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            // Método alternativo: simular entrada de teclas
            string dateString = date.ToString("ddMMyyyy"); // Formato sin separadores

            js.ExecuteScript(@"
            var input = arguments[0];
            var dateStr = arguments[1];
            
            // Hacer clic para enfocar
            input.click();
            input.focus();
            
            // Simular entrada de teclado
            for (var i = 0; i < dateStr.length; i++) {
                var char = dateStr.charAt(i);
                var keyEvent = new KeyboardEvent('keydown', { key: char, bubbles: true });
                input.dispatchEvent(keyEvent);
                
                var inputEvent = new InputEvent('input', { 
                    data: char,
                    bubbles: true,
                    inputType: 'insertText'
                });
                input.dispatchEvent(inputEvent);
                
                var keyUpEvent = new KeyboardEvent('keyup', { key: char, bubbles: true });
                input.dispatchEvent(keyUpEvent);
            }
            
            // Disparar cambio final
            input.dispatchEvent(new Event('change', { bubbles: true }));
            input.dispatchEvent(new Event('blur', { bubbles: true }));
        ", element, dateString);
        }


    }
}
