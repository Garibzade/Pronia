using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Pronia.DataAccesLayer;
using Pronia.Views.Categories;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProniaContext _context;

        public  HomeController(ProniaContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var data = await _context.Categories
            .Where(x => x.IsDeleted == false)
                .Select(x => new GetCategoryVM
                 {
                     Id = x.Id,
                     Name = x.Name
                 }).OrderByDescending(x=>x.Name)
                .TakeLast(4)
                .ToListAsync();

            return View(data);
        }
        public async Task<IActionResult> Test(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            _context.Remove(cat);
            return Content(cat.Name);
        }
        public async Task<IActionResult> Contact() 
        {
            return View();

        }
        public async Task<IActionResult> AboutUs()
        {
            return View();

        }
    }
}
