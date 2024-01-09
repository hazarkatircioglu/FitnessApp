using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Models
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
        public List<Order>? Order { get; set; }
    }
}
