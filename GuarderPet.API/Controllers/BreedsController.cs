using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;
using GuarderPet.API.Helpers;
using GuarderPet.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuarderPet.API.Controllers
{
    [Authorize(Roles = "Carer")]
    public class BreedsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBreedHelper _breedHelper;

        public BreedsController(DataContext context, ICombosHelper combosHelper, IBreedHelper breedHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _breedHelper = breedHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Breeds.Include(x => x.PetType).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Breed breed = await _context.Breeds
                .FirstOrDefaultAsync(m => m.Id == id);

            if (breed == null)
            {
                return NotFound();
            }

            return View(breed);
        }

        public IActionResult Create()
        {
            BreedViewModel model = new BreedViewModel
            {
                PetType = _combosHelper.GetComboPetTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreedViewModel breed)
        {
            if (ModelState.IsValid)
            {
                await _breedHelper.AddBreedAsync(breed);
                return RedirectToAction(nameof(Index));
            }
            return View(breed);
        }

        // GET: Breeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Breed breed = await _context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }
            return View(breed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BreedTittle")] Breed breed)
        {
            if (id != breed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreedExists(breed.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(breed);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Breed breed = await _context.Breeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breed == null)
            {
                return NotFound();
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreedExists(int id)
        {
            return _context.Breeds.Any(e => e.Id == id);
        }
    }
}
