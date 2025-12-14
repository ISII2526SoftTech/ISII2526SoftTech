using AppForMovies.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_Reparacion
{
    public class UC_Reparacion_UIT : UC_UIT
    {
        private GetSelectReparacion_PO getSelectReparacion_PO;
        private const string llave = "Llave inglesa";
        private const int llaveid = 1;
        private const string materialLlave = "Acero";
        private const string FabricanteLlave = "BOSCH";
        private const string tiempoReparacionLlave = "30";
        private const float precioLlave = 150;

        private const string destornillador = "Destornillador de estrella";
        private const int destornilladorid = 3;
        private const string materialDestornillador = "Acero y Plástico";
        private const string FabricanteDestornillador = "BOSCH";
        private const string tiempoReparacionDestornillador = "5";
        private const float precioDestornillador = 45;

        public UC_Reparacion_UIT(ITestOutputHelper output) : base(output)
        {
            getSelectReparacion_PO = new GetSelectReparacion_PO(_driver, _output);
        }

        public void Primer_Paso_Seleccionar_Herramienta()
        {
            Initial_step_opening_the_web_page();
            getSelectReparacion_PO.WaitForBeingVisible(By.Id("Reparación"));
            _driver.FindElement(By.Id("Reparación")).Click();
            Thread.Sleep(1000);
        }

        /*
           ============================
                PRUEBAS DEL SELECT 
           ============================
           */

        // PASOS 2 y 3, FLUJO ALTERNATIVO 0 - Filtros
        [Theory]
        [InlineData(llave, tiempoReparacionLlave, llave,FabricanteLlave, materialLlave, precioLlave, tiempoReparacionLlave)] // Filtro por Nombre y Tiempo
        [InlineData(llave, "", llave,FabricanteLlave, materialLlave,  precioLlave, tiempoReparacionLlave)] // Filtro por Nombre
        [InlineData("", tiempoReparacionDestornillador,destornillador, FabricanteDestornillador, materialDestornillador, precioDestornillador, tiempoReparacionDestornillador)] // Filtro por Tiempo

        public void FB_P2_P3_FA0_FiltroNombreYTiempo(
            string nombreHerramientaFiltro,
            string tiempoReparacionFiltro,
            string nombreEsperado,
            string fabricanteEsperado,
            string materialEsperado,
            float precioEsperado,
            string tiempoReparacionEsperado)
        {
            //Arrange
            Primer_Paso_Seleccionar_Herramienta();

            var herramientasEsperadas = new List<string[]>
            {
              new string[] {
              nombreEsperado,
              fabricanteEsperado,
              materialEsperado,
              precioEsperado.ToString() + " €",
              tiempoReparacionEsperado,
              "Añadir"
      }
            }; 

            //Act
            getSelectReparacion_PO.BuscarHerramientas(nombreHerramientaFiltro, tiempoReparacionFiltro);
            Thread.Sleep(1000); // Esperar a que la tabla se refresque

            //Assert
            Assert.True(getSelectReparacion_PO.VerificarTablaDeHerramientas(herramientasEsperadas));

        }

        // PASO 3, FLUJO ALTERNATIVO 2 - Modificar carrito desde Select
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void FB_P3_FA2_ModificarCarritoDesdeSelect()
        {
            // Arrange
            Primer_Paso_Seleccionar_Herramienta();
            getSelectReparacion_PO.BuscarHerramientas("", "");
            Thread.Sleep(1000);

            // Act - 1. Añadimos dos herramientas
            getSelectReparacion_PO.AñadirHerramientaAlCarro(llave); // Precio: 150
            getSelectReparacion_PO.AñadirHerramientaAlCarro(destornillador); // Precio: 45
            //Eliminamos la llave
            Thread.Sleep(1000);
            getSelectReparacion_PO.QuitarDelCarrito(llave);
            Thread.Sleep(1000);

            // Assert
            string precioEsperado = "45,00 €";
            string precioActual = getSelectReparacion_PO.ObtenerTotalEstimado();

            Assert.Equal(precioEsperado, precioActual);

        }

        // PASO 4, FLUJO ALTERNATIVO 3 - Carrito vacío
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void FB_P4_FA3_CarritoVacio()
        {
            //Arrange
            Primer_Paso_Seleccionar_Herramienta();
            getSelectReparacion_PO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            // Sin herramientas en el carrito

            //Assert
            Assert.True(getSelectReparacion_PO.RepararHerramientaNoDisponible());
        }
    }
}
