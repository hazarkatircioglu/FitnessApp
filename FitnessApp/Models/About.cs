using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string AboutText { get; set; }
    }
}
