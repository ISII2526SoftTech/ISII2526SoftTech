

using AppForSEII2526.UIT.CU_Comprar;
using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.CU_Comprar
{
    public class CU_Comprar_UIT : UC_UIT
    {
        private GetSelectComprar_PO getSelectComprar_PO;
      
        private DetalleCompra_PO detalleCompra_PO;

        public CU_Comprar_UIT(ITestOutputHelper output) : base(output)
        {
            getSelectComprar_PO = new GetSelectComprar_PO(_driver, _output);
           
            detalleCompra_PO = new DetalleCompra_PO(_driver, _output);
        }
        private const string herramienta1 = "tornillo";
        private const string herramienta2 = "martillo";
        private const string herramienta3 = "Taladro";
        private const string fabricante1 = "juan";
        private const string fabricante2 = "pepe";
        private const string fabricante3 = "billy";
        private const string material1 = "acero";
        private const string material2 = "hierro";
        private const string material3 = "metal";
        private const string precio1 = "12";
        private const string precio3 = "100";
        private const string precio2 = "2";

        private void InitialStepsForComprarHerramientas()
        {
            Initial_step_opening_the_web_page();
            getSelectComprar_PO.WaitForBeingClickable(By.Id("CrearCompra"));
            _driver.FindElement(By.Id("CrearCompra")).Click();
        }

        //Flujo alternativo 1 del paso 2
        [Theory]

        [InlineData(herramienta3, fabricante2, material3, precio3, null, "metal","")]
        [InlineData(herramienta1, fabricante3, material1, precio2, 2, "", "")]
        [InlineData(herramienta1, fabricante3, material1, precio2, 2, "acero", "")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_2_AF1_filteringByPrecioYMaterial(string nombreHerramienta, string fabricanteHerramienta, string materialHerramienta, string precioHerramienta, decimal filtroPrecio, string filtroMaterial,string filtroNombre)
        {
            //Arrange
            Thread.Sleep(1000);
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            var expectedMovies = new List<string[]> { new string[] { nombreHerramienta, fabricanteHerramienta, materialHerramienta, precioHerramienta.ToString() }, };
            Thread.Sleep(500);

            //Act
            getSelectComprar_PO.BuscarHerramientas(filtroMaterial,filtroPrecio, filtroNombre);
            Thread.Sleep(2000);

            //Assert

            Assert.True(getSelectComprar_PO.CheckListOfHerramientas(expectedMovies));

        }


        //Flujo alternativo 3 del paso 4
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_4_AF3_CompraNotavailable()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            //Act
            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);
            getSelectComprar_PO.EliminarHerramientaDeCarrito(herramienta2);
            Thread.Sleep(2000);

            //Assert

            Assert.True(getSelectComprar_PO.CompraNotAvailable());

        }






}
}
