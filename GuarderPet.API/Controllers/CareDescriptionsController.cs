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
    public class CareDescriptionsController : Controller
    {
        private readonly DataContext _context;

        public CareDescriptionsController(DataContext context)
        {
            _context = context;
        }

        // GET: CareDescriptions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CareDescriptions.ToListAsync());
        }

        // GET: CareDescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careDescription = await _context.CareDescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careDescription == null)
            {
                return NotFound();
            }

            return View(careDescription);
        }

        // GET: CareDescriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CareDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] CareDescription careDescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(careDescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(careDescription);
        }

        // GET: CareDescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careDescription = await _context.CareDescriptions.FindAsync(id);
            if (careDescription == null)
            {
                return NotFound();
            }
            return View(careDescription);
        }

        // POST: CareDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CareDescription careDescription)
        {
            if (id != careDescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareDescriptionExists(careDescription.Id))
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
            return View(careDescription);
        }

        // GET: CareDescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careDescription = await _context.CareDescriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (careDescription == null)
            {
                return NotFound();
            }

            return View(careDescription);
        }

        // POST: CareDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var careDescription = await _context.CareDescriptions.FindAsync(id);
            _context.CareDescriptions.Remove(careDescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareDescriptionExists(int id)
        {
            return _context.CareDescriptions.Any(e => e.Id == id);
        }
    }
}
