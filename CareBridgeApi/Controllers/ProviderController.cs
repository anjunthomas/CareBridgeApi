using CareBridgeApi.Data;
using CareBridgeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareBridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly CareBridgeDBContext _context;

        public ProvidersController(CareBridgeDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Provider>> CreateProvider(Provider newProvider)
        {
            _context.Providers.Add(newProvider);
            await _context.SaveChangesAsync();

            return Ok(newProvider);
        }
    }
}