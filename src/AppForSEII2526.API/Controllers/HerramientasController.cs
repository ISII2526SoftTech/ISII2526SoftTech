using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        //Habilitar tu controlador para usar el contexto de la base de datos y el logger
        private readonly ApplicationDbContext _context;
        //Usar el logger para registrar información, advertencias y errores
        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        /*
        public async Task<IActionResult> ComputeDivision(decimal op1, decimal op2)
        {
            if (op2 == 0)
            {
                _logger.LogError($"{DateTime.Now} Exception: op2=0, division by 0");
                return BadRequest("op2 must be different from 0");
            }

            decimal result = decimal.Round(op1 / op2, 2);
            return Ok(result);
        }
        */
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetHerramientas()
        {
            var herramientas = await _context.Herramientas.ToListAsync();
            return Ok(herramientas);
        }




    }
}
