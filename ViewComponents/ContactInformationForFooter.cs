using FitnessApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.ViewComponents
{
    public class ContactInformationForFooter : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public ContactInformationForFooter(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.Contact.FirstOrDefault());
        }
    }
}