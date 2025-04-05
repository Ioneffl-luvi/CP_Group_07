using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class MentalArticle
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string FileName { get; set; } 

        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }

}
