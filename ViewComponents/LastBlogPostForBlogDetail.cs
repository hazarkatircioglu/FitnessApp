using FitnessApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.ViewComponents
{
    public class LastBlogPostForBlogDetail : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public LastBlogPostForBlogDetail(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.Blog.OrderByDescending(x => x.Date).Take(6));
        }
    }
}