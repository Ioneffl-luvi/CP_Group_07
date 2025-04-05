using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class SupportPost
    {
        [Key]
        public int PostId { get; set; }

        public int? UserId { get; set; }

        [Required]
        public string Content { get; set; }
        public int Likes { get; set; } = 0;

        public DateTime PostedAt { get; set; } = DateTime.Now;

        public bool IsAnonymous { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool IsPinned { get; set; } = false;

        public ICollection<SupportComment> Comments { get; set; }
        public ICollection<SupportReaction> Reactions { get; set; }
        public ICollection<SupportReport> Reports { get; set; }
    }

}
