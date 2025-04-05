using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class Injection
    {

        [Key]
        public int InjectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(200)]
        public string Limit { get; set; }

        [Required]
        public int Age { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; } = DateTime.Now;

    }
}
