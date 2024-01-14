using FitnessApp.Models;
using FitnessApp.Models.Data;
using FitnessApp.Models.StaticClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace FitnessApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slider.ToListAsync());
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var data = await _context.Product.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        } 
        public async Task<IActionResult> OtherProductDetail(int id)
        {
            var data = await _context.OtherProduct.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
        public async Task<IActionResult> About()
        {
            var data = await _context.About.FirstOrDefaultAsync();
            return View(data);
        }
        public async Task<IActionResult> Blog()
        {
            var data = await _context.Blog.OrderByDescending(x => x.Date).AsNoTracking().ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> BlogDetail(int id)
        {
            var data = await _context.Blog.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return View(data);
        }
        [Authorize]
        public async Task<IActionResult> CheckOut(int productId)
        {
            ViewBag.data = await _context.Product.Where(x => x.Id == productId).AsNoTracking().FirstOrDefaultAsync();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckOut(Order p)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            p.AppUserId = userId;
            p.Date = DateTime.Now;
            p.Status = SatinAlmaDurumlari.ProgramBekleniyor;
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profil","Account",new {Area="Identity"});
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(string NameSurname, string Email, string Subject, string Message)
        {
            var mailSettings = _context.MailSettings.FirstOrDefault();
            if (mailSettings != null)
            {
                if (NameSurname != null && Email != null && Subject != null && Message != null)
                {
                    MailMessage msg = new MailMessage();
                    msg.Subject = "Yeni bir iletişim mesajı";
                    msg.From = new MailAddress(mailSettings.FromEmailAddress, mailSettings.FromEmailAddressDisplayName);
                    msg.To.Add(new MailAddress(mailSettings.SendEmailAddress, mailSettings.SendEmailAddressDisplayName));
                    msg.IsBodyHtml = true;
                    msg.Body = "Ad Soyad: " + NameSurname + "<br>" + "Email: " + Email + "<br><br>" + "Konu: " + Subject + "<br>" + "Mesaj: " + Message;
                    msg.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient(mailSettings.SmtpHost, Int32.Parse(mailSettings.SmtpPort));
                    NetworkCredential AccountInfo = new NetworkCredential(mailSettings.EmailAddress, mailSettings.EmailAddressPassword);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = AccountInfo;
                    smtp.EnableSsl = false;
                    try
                    {
                        smtp.Send(msg);
                        return RedirectToAction("Contact");
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                }
            }
            return RedirectToAction("Contact");
        }
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
