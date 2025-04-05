using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class HealthRecommendation
    {
        [Key]
        public int RecommendationId { get; set; }

        public int UserId { get; set; }

        public string RecommendationText { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
