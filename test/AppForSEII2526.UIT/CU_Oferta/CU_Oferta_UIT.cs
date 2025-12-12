using AppForMovies.UIT.Shared;
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
        private const string herramientaId1 = "3";
        private const string herramientaNombre1 = "Taladro";
        private const string herramientaMaterial1 = "metal";
        private const string herramientaFabricante1 = "Man";
        private const string herramientaPrecio1 = "100";

        private const string herramientaId2 = "1";
        private const string herramientaNombre2 = "Taladro";
        private const string herramientaMaterial2 = "metal";
        private const string herramientaFabricante2 = "Man";
        private const string herramientaPrecio2 = "100";

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
        private void InitialStepsForSelectOferta()
        {
            getSelectOferta_PO.WaitForBeingVisible(By.Id("CreateRental"));
            _driver.FindElement(By.Id("CreateOferta")).Click();
        }
        [Theory]
        [InlineData(herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaPrecio1, "Taladro", "110")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_FA0_filtering(string idHerramienta, string nombreHerramienta, string materialHerramienta,
            string fabricanteHerramienta, string precioHerramienta, string filtroFabricante, string filtroPrecioMax )
        {
            InitialStepsForSelectOferta();
            var expectedHerramientas = new List<string[]> { new string[] { herramientaId1, herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaPrecio1 }, };

            getSelectOferta_PO.SearchHerramientas(filtroFabricante, filtroPrecioMax);

            Assert.True(getSelectOferta_PO.CheckListOfMovies(expectedHerramientas));

        }

    }
}