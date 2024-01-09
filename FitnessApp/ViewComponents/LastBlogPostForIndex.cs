using FitnessApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.ViewComponents
{
    public class LastBlogPostForIndex : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public LastBlogPostForIndex(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.Blog.OrderByDescending(x => x.Date).Take(6));
        }
    }
}