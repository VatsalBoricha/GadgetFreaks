using GadgetFreaks.Data;
using GadgetFreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetFreaks.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext _context;

        //Constructor
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
            .OrderBy(category => category.CategoryName)
            .ToListAsync();


            return View(categories);
        }

        public async Task<IActionResult> Details(int ? id)
        {
            var categoryWithGadgets = await _context.Categories
                .Include(category => category.Gadgets)
                .FirstOrDefaultAsync(category => category.CategoryID == id);

            return View(categoryWithGadgets);
        }
    }
}
