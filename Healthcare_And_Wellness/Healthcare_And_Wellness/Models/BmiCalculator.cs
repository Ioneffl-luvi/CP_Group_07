using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare_And_Wellness.Models
{
    public class BmiCalculator
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Please enter the height")]
        [Range(0.5, 3.0, ErrorMessage = "Height must be between 0.5m and 3m")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Please enter the weight")]
        [Range(10, 500, ErrorMessage = "Weight must be between 10kg and 500kg")]
        public double? Weight { get; set; }

        public double? BmiResult { get; set; }

        public DateTime DateRecorded { get; set; } = DateTime.Now; 

        [ForeignKey("User")]
        public int UserId { get; set; } 

        public virtual User? User { get; set; } 

        public double? CalculateBmi()
        {
            return Weight / (Height * Height);
        }
    }
}
