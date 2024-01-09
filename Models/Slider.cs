using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
