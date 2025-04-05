using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class SelfAssessment
    {
        [Key]
        public int AssessmentId { get; set; }

        public int UserId { get; set; }

        public int TotalScore { get; set; }

        public DateTime TakenAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}
