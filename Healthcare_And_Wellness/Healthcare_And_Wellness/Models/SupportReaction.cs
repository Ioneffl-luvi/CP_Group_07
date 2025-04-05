using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class SupportReaction
    {
        [Key]
        public int ReactionId { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public SupportPost Post { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string ReactionType { get; set; } 
    }
}
