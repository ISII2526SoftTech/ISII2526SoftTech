using AppForSEII2526.UIT.Shared;
using AppForSEII2526.UIT.UC_Rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AppForSEII2526.UIT.CU_Oferta
{
    public class CU_Oferta_UIT : UC_UIT
    {
        private GetSelectOferta_PO getSelectOferta_PO;
        private CreateOferta_PO createOferta_PO;
        private GetDetailsOferta_PO getDetailsOferta_PO;
        private const string herramientaId1 = "3";
        private const string herramientaNombre1 = "Taladro";
        private const string herramientaMaterial1 = "metal";
        private const string herramientaFabricante1 = "Man";
        private const string herramientaPrecio1 = "100";

        private const string herramientaId2 = "1";
        private const string herramientaNombre2 = "destornillador";
        private const string herramientaMaterial2 = "hierro";
        private const string herramientaFabricante2 = "FABRICANTE2";
        private const string herramientaPrecio2 = "12";

        public CU_Oferta_UIT(ITestOutputHelper output) : base(output)
        {
            getSelectOferta_PO = new GetSelectOferta_PO(_driver, _output);
            createOferta_PO = new CreateOferta_PO(_driver, _output); 
            getDetailsOferta_PO = new GetDetailsOferta_PO(_driver, _output); 
        }
        private void InitialStepsForOferta()
        {
            Initial_step_opening_the_web_page();

            getSelectOferta_PO.WaitForBeingVisible(By.Id("CreateOferta"));
            Thread.Sleep(500);
            _driver.FindElement(By.Id("CreateOferta")).Click();
        }




        /*
         * CU1_FB_OfertarHerramienta
         * El usuario oferta una herramienta con un porcentaje de descuento válido
         * La herramienta aparece en la lista de herramientas ofertadas con el precio correcto
         */
        [Theory]
        [InlineData("Sergio", 1, 10)]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_FB_OfertarHerramienta(string nombre, int diasInicio, int diasFinal)
        {

            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("", 13);
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] {herramientaId2, "50%", "12,00 €", "6,00 €" }

            };
            DateTime fechaInicio = DateTime.Today.AddDays(1);
            DateTime fechaFinal = DateTime.Today.AddDays(10);
            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre2);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            createOferta_PO?.PonerDatosOferta(nombre, fechaInicio, fechaFinal);
            Thread.Sleep(2500);
            createOferta_PO.SubmitOferta();
            Thread.Sleep(500);
            createOferta_PO.ConfirmarOferta();
            Thread.Sleep(500);

            //Assert
            Assert.True(getDetailsOferta_PO.CheckDetallesOfeta("Sergio", fechaInicio, fechaFinal, "TarjetaCredito", "Socios", "6,00 €"));
            Assert.True(getDetailsOferta_PO.CheckListaHerramientas(expectedHerramientas));

        }




        /*
         * CU3_FA0_Filtro
         * El usuario filtra las herramientas por fabricante
         * El usuario filtra las herramientas por precio máximo
         * Se muestran las herramientas que cumplen los criterios de filtrado
         */
        [Theory]
        [InlineData(herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaPrecio1, "Man", 101)] //filtrado por nombre
        [InlineData(herramientaNombre2, herramientaMaterial2, herramientaFabricante2, herramientaPrecio2, "", 13)] //filtrado por precio maximo
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA0_filro(string nombreHerramienta, string materialHerramienta,
            string fabricanteHerramienta, string precioHerramienta, string filtroFabricante, double filtroPrecioMax )
        {
            InitialStepsForOferta();
            var expectedHerramientas = new List<string[]> { new string[] {nombreHerramienta, materialHerramienta, fabricanteHerramienta, precioHerramienta }, };
            getSelectOferta_PO.SearchHerramientas(filtroFabricante, filtroPrecioMax);

            Thread.Sleep(500);

            Assert.True(getSelectOferta_PO.CheckListOfHerramientas(expectedHerramientas));

        }



        /*
         * FechasErroneas
         * El usuario intenta crear una oferta con fechas incorrectas o el metodo de pago inválido
         * Se muestra un mensaje de error adecuado
         */
        [Theory]
        [InlineData("Sergio", -2, 10, "PayPal", "Socios", "La fecha de inicio no puede ser anterior a hoy")]
        [InlineData("Sergio", 1, -2, "PayPal", "Socios", "La fecha de fin debe ser posterior a la fecha de inicio")]
        [InlineData("Sergio", 1, 3, "PayPal", "Socios", "¡Error!, la oferta debe durar al menos una semana")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA1_FechasInvalidas(string nombre, int diasInicio, int diasFin,
                                    string metodoDePago, string dirigidaA, string error)
        {
            try
            {
                DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
                DateTime fechaFinal = DateTime.Today.AddDays(diasFin);
                InitialStepsForOferta();
                getSelectOferta_PO.SearchHerramientas("", 200000);
                Thread.Sleep(500);
                // Act
                getSelectOferta_PO.SelectHerramienta(herramientaNombre1);
                Thread.Sleep(500);
                getSelectOferta_PO.OfertarHerramientas();
                Thread.Sleep(500);

                if (!_driver.Url.Contains("createoferta"))
                {
     
                    _driver.Navigate().GoToUrl(_URI + "oferta/createoferta");
                    Thread.Sleep(3000);
                }
                createOferta_PO?.PonerDatosOferta(nombre, fechaInicio, fechaFinal);
                Thread.Sleep(500);
                createOferta_PO.SubmitOferta();
                Thread.Sleep(500);
                // Assert
                Assert.True(createOferta_PO.CheckError(error));
            }
            catch (Exception ex)
            {         
            }
        }




        /*
         * CU3_FA2_Carrito
         * El usuario añade dos herramientas al carrito de oferta
         * Elimina una herramienta del carrito de oferta
         * La herramienta eliminada desaparece del carrito de oferta
         */
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA2_Carrito()
        {
            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("", 20000);
            Thread.Sleep(500);
            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre1);
            Thread.Sleep(500);
            getSelectOferta_PO.SelectHerramienta(herramientaNombre2);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            createOferta_PO.ModificarOferta();
            Thread.Sleep(500);

            getSelectOferta_PO.DeSelectHerramienta(herramientaNombre1);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaId2, "50", "12", "6" }

            };
            Thread.Sleep(500);
            //Assert
            Assert.True(createOferta_PO.CheckListaHerramientas(expectedHerramientas));


        }

        /*
         * PorcentajeErroneo
         * El usuario intenta poner un porcentaje de descuento mayor al 100%
         * Se fuerza a que el porcentaje máximo sea 100%
         */

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA3_PorcentajeErroneo()
        {
            int porcentajeIncorrecto = 200;
            int porcentajeEsperado = 100;
            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("", 20000000);
            Thread.Sleep(500);

            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre2);
            Thread.Sleep(500);
            getSelectOferta_PO.ponerPorcentaje(1, porcentajeIncorrecto);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaId2, "100", "12", "0" }

            };
            //Assert
            Assert.True(createOferta_PO.CheckListaHerramientas(expectedHerramientas));



        }



        /*
         * CU3_4_AF4_CarritovacioNoSePuedeOfertar
         * El usuario no ha añadido ninguna herramienta al carrito de oferta
         * El botón de "Ofertar Herramientas" está inactivo
         */
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_4_FA4_CarritovacioNoSePuedeOfertar()
        {
            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("", 200000);
            Thread.Sleep(500);
            //Assert
            Assert.True(getSelectOferta_PO.OfertarHerramientaNoDisponible());
        }

        /*
         * CU3_FA5_DatoObligatorio
         * El usuario intenta crear una oferta sin rellenar metodo de pago
         * Se muestra un mensaje de error adecuado
         */
        [Theory]
        [InlineData("Sergio", 1, 10, "", "Socios", "Falta un metodo de pago válido")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA5_DatoObligatorio(string nombre, int diasInicio, int diasFin,
                                    string metodoDePago, string dirigidaA, string error)
        {
            try
            {
                DateTime fechaInicio = DateTime.Today.AddDays(diasInicio);
                DateTime fechaFinal = DateTime.Today.AddDays(diasFin);
                InitialStepsForOferta();
                getSelectOferta_PO.SearchHerramientas("", 200000);
                Thread.Sleep(500);
                // Act
                getSelectOferta_PO.SelectHerramienta(herramientaNombre1);
                Thread.Sleep(500);
                getSelectOferta_PO.OfertarHerramientas();
                Thread.Sleep(500);

                if (!_driver.Url.Contains("createoferta"))
                {

                    _driver.Navigate().GoToUrl(_URI + "oferta/createoferta");
                    Thread.Sleep(3000);
                }
                createOferta_PO?.PonerDatosOferta(nombre, fechaInicio, fechaFinal);
                Thread.Sleep(500);
                createOferta_PO.SubmitOferta();
                Thread.Sleep(500);
                // Assert
                Assert.True(createOferta_PO.CheckError(error));
            }
            catch (Exception ex)
            {
            }
        }
    }

}