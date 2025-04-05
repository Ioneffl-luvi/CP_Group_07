using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class SupportComment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.Now;

        public bool IsAnonymous { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public SupportPost Post { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
