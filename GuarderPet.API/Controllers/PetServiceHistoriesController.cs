using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuarderPet.API.Data;
using GuarderPet.API.Data.Entities;

namespace GuarderPet.API.Controllers
{
    public class PetServiceHistoriesController : Controller
    {
        private readonly DataContext _context;

        public PetServiceHistoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: PetServiceHistories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetServiceHistories.ToListAsync());
        }

        // GET: PetServiceHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petServiceHistory = await _context.PetServiceHistories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petServiceHistory == null)
            {
                return NotFound();
            }

            return View(petServiceHistory);
        }

        // GET: PetServiceHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetServiceHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InitDate,EndDate,Total")] PetServiceHistory petServiceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petServiceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petServiceHistory);
        }

        // GET: PetServiceHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petServiceHistory = await _context.PetServiceHistories.FindAsync(id);
            if (petServiceHistory == null)
            {
                return NotFound();
            }
            return View(petServiceHistory);
        }

        // POST: PetServiceHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InitDate,EndDate,Total")] PetServiceHistory petServiceHistory)
        {
            if (id != petServiceHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petServiceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetServiceHistoryExists(petServiceHistory.Id))
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
            return View(petServiceHistory);
        }

        // GET: PetServiceHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petServiceHistory = await _context.PetServiceHistories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petServiceHistory == null)
            {
                return NotFound();
            }

            return View(petServiceHistory);
        }

        // POST: PetServiceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petServiceHistory = await _context.PetServiceHistories.FindAsync(id);
            _context.PetServiceHistories.Remove(petServiceHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetServiceHistoryExists(int id)
        {
            return _context.PetServiceHistories.Any(e => e.Id == id);
        }
    }
}
