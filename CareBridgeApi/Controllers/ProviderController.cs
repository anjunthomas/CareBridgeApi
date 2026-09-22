using CareBridgeApi.Data;
using CareBridgeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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


        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetProviders()
        {
            var providers = await _context.Providers.ToListAsync();
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProviderByID(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if(provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }

        [HttpPost]
        public async Task<ActionResult<Provider>> CreateProvider(Provider newProvider)
        {
            _context.Providers.Add(newProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProviderByID),
                new { id = newProvider.Id },
                newProvider
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> UpdateProvider(int id, Provider updatedProvider)
        {
            var provider = await _context.Providers.FindAsync(id);

            if(provider == null)
            {
                return NotFound();
            }

            provider.FirstName = updatedProvider.FirstName;
            provider.LastName = updatedProvider.LastName;
            provider.Specialty = updatedProvider.Specialty;
            provider.Email = updatedProvider.Email;
            provider.PhoneNumber = updatedProvider.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(provider);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var deletedProvider = await _context.Providers.FindAsync(id);
            if(deletedProvider == null)
            {
                return NotFound();
            }
            _context.Providers.Remove(deletedProvider);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}