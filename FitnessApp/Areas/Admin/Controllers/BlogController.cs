using FitnessApp.Models;
using FitnessApp.Models.Data;
using FitnessApp.Models.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Blog.OrderByDescending(x => x.Date).AsNoTracking().ToListAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog p)
        {
            p.Date = DateTime.Now;
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _context.Blog.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog p)
        {
            p.Date = DateTime.Now;
            _context.Update(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Blog.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
