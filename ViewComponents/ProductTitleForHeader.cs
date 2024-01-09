using FitnessApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.ViewComponents
{
    public class ProductTitleForHeader : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public ProductTitleForHeader(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.Product.ToList());
        }
    }
}