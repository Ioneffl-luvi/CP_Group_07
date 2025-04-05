using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class GuidedActivity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
