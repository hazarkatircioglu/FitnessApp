using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? LinkedinLink { get; set; }
        public string? TwitterLink { get; set; }
    }
}
