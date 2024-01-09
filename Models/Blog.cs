using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogText { get; set; }
        public DateTime Date { get; set; }
    }
}
