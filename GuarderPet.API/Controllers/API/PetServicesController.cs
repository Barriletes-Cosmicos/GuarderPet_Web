using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuarderPet.API.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using GuarderPet.API.Data;
using System.Threading.Tasks;

namespace GuarderPet.API.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PetServicesController : ControllerBase
    {
        private readonly DataContext _context;

        public PetServicesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PetServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetService>>> GetPetServices()
        {
            return await _context.PetServices.ToListAsync();
        }

        // GET: api/PetServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetService>> GetPetService(int id)
        {
            PetService petService = await _context.PetServices.FindAsync(id);

            if (petService == null)
            {
                return NotFound();
            }

            return petService;
        }

        // PUT: api/PetServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetService(int id, PetService petService)
        {
            if (id != petService.Id)
            {
                return BadRequest();
            }

            _context.Entry(petService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PetServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetService>> PostPetService(PetService petService)
        {
            _context.PetServices.Add(petService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPetService", new { id = petService.Id }, petService);
        }

        // DELETE: api/PetServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetService(int id)
        {
            PetService petService = await _context.PetServices.FindAsync(id);
            if (petService == null)
            {
                return NotFound();
            }

            _context.PetServices.Remove(petService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetServiceExists(int id)
        {
            return _context.PetServices.Any(e => e.Id == id);
        }
    }
}
