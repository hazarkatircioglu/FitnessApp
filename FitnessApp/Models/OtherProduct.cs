using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class OtherProduct
    {
        [Key]
        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
    }
}
