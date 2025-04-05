using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class SupportReport
    {
        [Key]
        public int ReportId { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public SupportPost Post { get; set; }

        public int? UserId { get; set; }  
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime ReportedAt { get; set; } = DateTime.Now;
    }
}
