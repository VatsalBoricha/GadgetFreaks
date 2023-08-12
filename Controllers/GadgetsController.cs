using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GadgetFreaks.Data;
using GadgetFreaks.Models;
using Microsoft.AspNetCore.Authorization;

namespace GadgetFreaks.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class GadgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GadgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gadgets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Gadgets.Include(g => g.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Gadgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gadgets == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.GadgetID == id);
            if (gadget == null)
            {
                return NotFound();
            }

            return View(gadget);
        }

        // GET: Gadgets/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Gadgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GadgetID,CategoryID,Name,Description,Price,ReleaseDate,Manufacturer")] Gadget gadget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gadget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", gadget.CategoryID);
            return View(gadget);
        }

        // GET: Gadgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gadgets == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets.FindAsync(id);
            if (gadget == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", gadget.CategoryID);
            return View(gadget);
        }

        // POST: Gadgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GadgetID,CategoryID,Name,Description,Price,ReleaseDate,Manufacturer")] Gadget gadget)
        {
            if (id != gadget.GadgetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gadget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GadgetExists(gadget.GadgetID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", gadget.CategoryID);
            return View(gadget);
        }

        // GET: Gadgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gadgets == null)
            {
                return NotFound();
            }

            var gadget = await _context.Gadgets
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.GadgetID == id);
            if (gadget == null)
            {
                return NotFound();
            }

            return View(gadget);
        }

        // POST: Gadgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gadgets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Gadgets'  is null.");
            }
            var gadget = await _context.Gadgets.FindAsync(id);
            if (gadget != null)
            {
                _context.Gadgets.Remove(gadget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GadgetExists(int id)
        {
          return (_context.Gadgets?.Any(e => e.GadgetID == id)).GetValueOrDefault();
        }
    }
}
