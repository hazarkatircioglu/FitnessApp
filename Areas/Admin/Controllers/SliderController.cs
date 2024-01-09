using FitnessApp.Models;
using FitnessApp.Models.Data;
using FitnessApp.Models.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;
        public SliderController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_context.Slider.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider p)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {

                var ext = Path.GetExtension(files[0].FileName).ToLower();
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(_he.WebRootPath, @"images");
                using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }
                p.Image = @"/images/" + fileName + ext;
            }
            _context.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Slider.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public IActionResult Edit(Slider p)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var ext = Path.GetExtension(files[0].FileName).ToLower();
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(_he.WebRootPath, @"images");
                using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }
                p.Image = @"/images/" + fileName + ext;
            }
            _context.Update(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var p = _context.Slider.Find(id);
            if (p.Image != null)
            {
                p.Image = p.Image.Replace("/", @"\");
                var imagePath = Path.Combine(_he.WebRootPath, p.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Slider.Remove(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}