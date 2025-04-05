using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}
