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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Order.Include(x => x.Product).OrderByDescending(x => x.Date).AsNoTracking().ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _context.Order.Where(x => x.Id == id).Include(x => x.AppUser).Include(x => x.Product).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _context.Order.Where(x => x.Id == id).Include(x => x.AppUser).Include(x => x.Product).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order p)
        {
            var data = await _context.Order.Where(x => x.Id == p.Id).AsNoTracking().FirstOrDefaultAsync();
            data.Status = SatinAlmaDurumlari.ProgramGonderildi;
            data.TrainingProgram = p.TrainingProgram;
            _context.Update(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
