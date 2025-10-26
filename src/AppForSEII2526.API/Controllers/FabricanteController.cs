namespace AppForSEII2526.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class FabricanteController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            private ILogger _logger;

            public FabricanteController(ApplicationDbContext context, ILogger<HerramientasController> logger)
            {
                _context = context;
                _logger = logger;
            }

            // GET: api/Movies/GetMoviesForPurchase
            [HttpGet]
            [Route("[action]")]
            [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
            public async Task<ActionResult> GetFabricante(string? fabricante, int id)
            {

                IList<string> fabricantes = await _context.Fabricante
                    .Where(f => (f.Nombre == null || f.Nombre.Contains(fabricante)) || f.Id == id)             
                    .OrderBy(f => f.Nombre)
                    .Select(f => f.Nombre)
                    .ToListAsync();

                return Ok(fabricantes);
            }
        
    }
}
