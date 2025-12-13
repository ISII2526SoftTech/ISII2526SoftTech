
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
        private const string herramientaNombre2 = "Destornillador";
        private const string herramientaMaterial2 = "hierro";
        private const string herramientaFabricante2 = "FABRICANTE2";
        private const string herramientaPrecio2 = "12";

        public CU_Oferta_UIT(ITestOutputHelper output) : base(output)
        {
            getSelectOferta_PO = new GetSelectOferta_PO(_driver, _output);
        }
        /*
        private void Precondition_perform_login()
        {
            Perform_login("Sergio", "Password1234%");
        }
        */
        private void InitialStepsForOferta()
        {
            Initial_step_opening_the_web_page();

            getSelectOferta_PO.WaitForBeingVisible(By.Id("CreateOferta"));
            Thread.Sleep(500);
            _driver.FindElement(By.Id("CreateOferta")).Click();
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU1_FB_OfertarHerramienta()
        {

            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("Destornillador", 200000);
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaFabricante2, herramientaPrecio2 }

            };

            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre2);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            createOferta_PO.PonerDatosOferta("Sergio", DateTime.Today, DateTime.Today.AddDays(10), "PayPal", "Socios");
            Thread.Sleep(500);
            createOferta_PO.SubmitOferta();
            Thread.Sleep(500);
            createOferta_PO.ConfirmarOferta();
            Thread.Sleep(500);

            //Assert

            Assert.True(getDetailsOferta_PO.CheckDetallesOfeta("Sergio", DateTime.Today, DateTime.Today.AddDays(10), "PayPal", "Socios", 6));

            Assert.True(getDetailsOferta_PO.CheckListaHerramientas(expectedHerramientas));


        }




        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaPrecio1, "Taladro", null)] //filtrado por nombre
        [InlineData(herramientaId2, herramientaNombre2, herramientaMaterial2, herramientaFabricante2, herramientaPrecio2, "", 13)] //filtrado por precio maximo
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA0_filro(string idHerramienta, string nombreHerramienta, string materialHerramienta,
            string fabricanteHerramienta, string precioHerramienta, string filtroFabricante, double filtroPrecioMax )
        {
            InitialStepsForOferta();
            var expectedHerramientas = new List<string[]> { new string[] { idHerramienta, nombreHerramienta, materialHerramienta, fabricanteHerramienta, precioHerramienta }, };
            getSelectOferta_PO.SearchHerramientas(filtroFabricante, filtroPrecioMax);

            Thread.Sleep(500);

            Assert.True(getSelectOferta_PO.CheckListOfHerramientas(expectedHerramientas));

        }
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
            getSelectOferta_PO.DeSelectHerramienta(herramientaNombre2);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaPrecio1 }

            };
            Thread.Sleep(500);



            //Assert
            Assert.True(createOferta_PO.CheckListaHerramientas(expectedHerramientas));


        }

        [Theory]
        [InlineData("Sergio", "2026-05-12", "2026-06-15", "343", "Socios", "Falta un metodo de pago válido")]
        [InlineData("Sergio", "2024-05-12", "2026-06-15", "PayPal", "Socios", "La fecha de inicio no puede ser anterior a hoy")]
        [InlineData("Sergio", "2026-05-12", "2024-06-15", "PayPal", "Socios", "La fecha de fin debe ser posterior a la fecha de inicio")]
        [InlineData("Sergio", "2026-05-12", "2026-05-13", "PayPal", "Socios", "¡Error!, la oferta debe durar al menos una semana")]

        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA1_FechasErroneas(string nombre, string fechaInicioStr, string fechaFinalStr, string metodoDePago, string dirigidaA, string error)
        {
            DateTime fechaInicio = DateTime.Parse(fechaInicioStr);
            DateTime fechaFinal = DateTime.Parse(fechaFinalStr);
            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("", 200000);
            Thread.Sleep(500);

            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre1);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            createOferta_PO.PonerDatosOferta(nombre, fechaInicio, fechaFinal, metodoDePago, dirigidaA);
            Thread.Sleep(500);
            createOferta_PO.SubmitOferta();
            Thread.Sleep(500);

            //Assert
            Assert.True(createOferta_PO.CheckError(error));

        }
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU3_FA3_PorcentajeErroneo()
        {
            int porcentajeIncorrecto = 200;
            int porcentajeEsperado = 100;
            //Arrange
            InitialStepsForOferta();
            getSelectOferta_PO.SearchHerramientas("",20000000);
            Thread.Sleep(500);

            //Act
            getSelectOferta_PO.SelectHerramienta(herramientaNombre1);
            Thread.Sleep(500);
            getSelectOferta_PO.OfertarHerramientas();
            Thread.Sleep(500);
            createOferta_PO.PonerDatosOferta("Sergio", DateTime.Today, DateTime.Today.AddDays(15), "PayPal", "Socios");
            Thread.Sleep(500);
            getSelectOferta_PO.ponerPorcentaje(3, porcentajeIncorrecto);
            Thread.Sleep(500);
            var porcentajeInput = _driver.FindElement(By.Id($"porcentaje_{herramientaId1}"));
            string valorActualStr = porcentajeInput.GetAttribute("value");
            int valorActual = int.Parse(valorActualStr);
            createOferta_PO.SubmitOferta();
            Thread.Sleep(500);

            createOferta_PO.ConfirmarOferta();
            Thread.Sleep(500);

            //Assert
            Assert.Equal(porcentajeEsperado, valorActual);
            Assert.True(valorActual <= 100, $"El porcentaje {valorActual} debería ser ≤ 100");



        }









    }
}