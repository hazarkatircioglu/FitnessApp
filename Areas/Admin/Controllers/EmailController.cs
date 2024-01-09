using FitnessApp.Models;
using FitnessApp.Models.Data;
using FitnessApp.Models.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class EmailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Edit()
        {
            var data = _context.MailSettings.FirstOrDefault();
            if (data == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(data);
            }
        }
        [HttpPost]
        public IActionResult Edit(MailSettings p)
        {
            _context.Update(p);
            _context.SaveChanges();
            return RedirectToAction("Edit");
        }
        [HttpGet]
        public IActionResult Create()
        {

            var data = _context.MailSettings.FirstOrDefault();
            if (data == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpPost]
        public IActionResult Create(MailSettings p)
        {
            _context.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Edit");
        }
    }
}
