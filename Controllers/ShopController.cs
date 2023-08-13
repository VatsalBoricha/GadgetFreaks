using GadgetFreaks.Data;
using GadgetFreaks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        
        public async Task<IActionResult> GadgetDetails(int? id)
        {
            var gadget = await _context.Gadgets
                .FirstOrDefaultAsync(gadget => gadget.GadgetID == id);

            return View(gadget);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult>AddToCart(int gadgetId, int quantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Carts
                .FirstOrDefaultAsync(cart =>cart.UserId == userId && cart.Active == true);

            if (cart == null)
            {
                cart = new Models.Cart { UserId = userId };
                await _context.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            var gadget = await _context.Gadgets
                .FirstOrDefaultAsync(gadget => gadget.GadgetID  == gadgetId);

            

            return RedirectToAction("GadgetDetails", new {id = gadgetId});
        }
    }
}
