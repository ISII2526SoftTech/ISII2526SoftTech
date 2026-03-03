
using AppForSEII2526.UIT.CU_Comprar;
using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    public class CUComprarHerramientas_UIT : UC_UIT
    {
        private GetSelectComprar_PO getSelectComprar_PO;
        private CrearCompra_PO crearCompra_PO;
        private DetalleCompra_PO detalleCompra_PO;

        public CUComprarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            getSelectComprar_PO = new GetSelectComprar_PO(_driver, _output);
            crearCompra_PO = new CrearCompra_PO(_driver, _output);
            detalleCompra_PO = new DetalleCompra_PO(_driver, _output);
        }
  
        private const string herramienta2 = "Sierra de mano";
        private const string herramienta3 = "Destornillador de estrella";
        private const string fabricante3 = "juan";
        private const string material1 = "Acero y Plástico";
        private const string material2 = "Acero y Madera";
        private const string precio3 = "45";
        private const string precio2 = "110";

        private void InitialStepsForComprarHerramientas()
        {
            Initial_step_opening_the_web_page();
            getSelectComprar_PO.WaitForBeingClickable(By.Id("CrearCompra"));
            _driver.FindElement(By.Id("CrearCompra")).Click();
        }

        //Flujo alternativo 1 del paso 2
        [Theory]
        [InlineData(herramienta3, fabricante3, material1, precio3, 45, "")]
        [InlineData(herramienta2, fabricante3, material2, precio2, null, "Madera")]
        [InlineData(herramienta2, fabricante3, material2, precio2, 110, "Acero y Madera")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_2_AF1_filteringByPrecioYMaterial(string nombreHerramienta, string fabricanteHerramienta, string materialHerramienta, string precioHerramienta, decimal filtroPrecio, string filtroMaterial)
        {
            //Arrange
            Thread.Sleep(1000);
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            var expectedMovies = new List<string[]> { new string[] { nombreHerramienta, fabricanteHerramienta, materialHerramienta, precioHerramienta.ToString() }, };
            Thread.Sleep(500);

            //Act
            getSelectComprar_PO.BuscarHerramientas(filtroPrecio, filtroMaterial);
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


        //Flujo básico
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_FlujoBasico()
        {
            //Arrange

            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);

            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);
            crearCompra_PO.RellenarFormularioCompra("Sergio", "Sanchez", "Calle Mayor");
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas("Tuerca de Acero", herramienta2);
            Thread.Sleep(500);

            crearCompra_PO.rellenarCantidad(1, herramienta2);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(500);

            //Assert

            Assert.True(detalleCompra_PO.CheckDetallesCompra("Sergio", "Sanchez", "Calle Mayor", precio2, DateTime.Today));

            var expectedDetallesHerramienta = new List<string[]> { new string[] { herramienta2, material2, "1", "Tuerca de Acero", precio2 }, };
            Assert.True(detalleCompra_PO.CheckListaHerramientasCompradas(expectedDetallesHerramienta));

        }

        //PRUEBAS FUNCIONALES DEL POST
        //Flujo alternativo 4 del paso 6
        [Theory]
        [InlineData("", "Sanchez", "Calle Mayor", "Sierra de mano Acero y Madera", "The NombreCliente field is required.")]
        [InlineData("Sergio", "", "Calle Mayor", "Sierra de mano Acero y Madera", "The ApellidoCliente field is required.")]
        [InlineData("Sergio", "Sanchez", "", "Sierra de mano Acero y Madera", "The Direccion field is required.")]
        [InlineData("Sergio", "Sanchez", "Calle Mayor", "", "The Descripcion field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_6_AF4_datosErroneos(string nombre, string apellidos, string direccion, string descripcion, string error)
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);




            //Act

            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);  
            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);

            crearCompra_PO.RellenarFormularioCompra(nombre, apellidos, direccion);
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas(descripcion, herramienta2);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(1000);

            //Assert

            Assert.True(crearCompra_PO.ValidarError(error));

        }

        //Flujo alternativo 2 del paso 5
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_5_AF2_ModificarCarrito()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta3);
            Thread.Sleep(2000);
            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);
            crearCompra_PO.modificarCarrito();
            Thread.Sleep(2000);
            getSelectComprar_PO.EliminarHerramientaDeCarrito(herramienta3);
            Thread.Sleep(500);
            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(500);


            var expectedHerramientas = new List<string[]> { new string[] { herramienta2, material2 }, };
            Thread.Sleep(500);


            //Assert
            Assert.True(crearCompra_PO.comprobarListaHerramientasItems(expectedHerramientas));

        }

        //Flujo alternativo 5 del paso 6
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_6_AF5_CantidadErronea()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);
            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);

            crearCompra_PO.RellenarFormularioCompra("Sergio", "Sanchez", "Calle Mayor");
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas("Sierra de mano", herramienta2);
            Thread.Sleep(500);
            crearCompra_PO.rellenarCantidad(0, herramienta2);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(1000);

            //Assert

            Assert.True(crearCompra_PO.ValidarError("La cantidad debe ser mayor que cero."));
        }

        //prueba examen recuperacion
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CasoExamen()
        {
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            getSelectComprar_PO.BuscarHerramientas(45,"");
            Thread.Sleep(2000);
            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta3);
            Thread.Sleep(2000);
            getSelectComprar_PO.BuscarHerramientas(0, "Acero y Madera");
            Thread.Sleep(2000);
            getSelectComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);
            getSelectComprar_PO.EliminarHerramientaDeCarrito(herramienta3);
            Thread.Sleep(2000);
            getSelectComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);
            crearCompra_PO.RellenarFormularioCompra("Sergio", "Sanchez", "Calle Mayor");
            Thread.Sleep(2000);
            crearCompra_PO.rellenarCantidad(2, herramienta2);
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas("sierra de mano to guapa", herramienta2);
            Thread.Sleep(2000);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(2000);

            Assert.True(detalleCompra_PO.CheckDetallesCompra("Sergio", "Sanchez", "Calle Mayor", precio2, DateTime.Today));

            var expectedDetallesHerramienta = new List<string[]> { new string[] { herramienta2, material2, "2", "sierra de mano to guapa", precio2 }, };
            Assert.True(detalleCompra_PO.CheckListaHerramientasCompradas(expectedDetallesHerramienta));


        }








    }
    }