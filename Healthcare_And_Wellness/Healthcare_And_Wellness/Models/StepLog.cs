using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare_And_Wellness.Models
{
    public class StepLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever] 
        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please enter the number of steps.")]
        [Range(1, int.MaxValue, ErrorMessage = "Steps must be greater than zero.")]
        public int Steps { get; set; }

        public int CaloriesBurned { get; set; }

        public void CalculateCalories()
        {
            CaloriesBurned = (int)(Steps * 0.04);
        }
    }
}
