using FitnessApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.ViewComponents
{
    public class OtherProductTitleForHeader : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public OtherProductTitleForHeader(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.OtherProduct.ToList());
        }
    }
}