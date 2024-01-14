using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FitnessApp.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OtherProduct> OtherProduct { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<MailSettings> MailSettings { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Slider> Slider { get; set; }

    }
}
