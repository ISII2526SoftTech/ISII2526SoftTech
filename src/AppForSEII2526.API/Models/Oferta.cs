namespace AppForSEII2526.API.Models
{
    public class Oferta
    {

        public DateTime fechaFinal { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaOferta { get; set; }
        public int Id { get; set; }

        public Oferta()
        {
        }

        // Constructor con parámetros
        public Oferta(DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta, int id)
        {
            this.fechaFinal = fechaFinal;
            this.fechaInicio = fechaInicio;
            this.fechaOferta = fechaOferta;
        }
    }
}

    
