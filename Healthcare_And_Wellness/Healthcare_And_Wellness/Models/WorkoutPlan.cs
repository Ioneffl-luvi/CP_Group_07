using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class WorkoutPlan
    {
       [Key]
        public int WorkoutId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } 

        [Required]
        public int Duration { get; set; } 

        public string? VideoUrl { get; set; } 
    }
}
