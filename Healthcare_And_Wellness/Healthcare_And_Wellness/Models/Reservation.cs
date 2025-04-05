using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Healthcare_And_Wellness.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int InjectionId { get; set; }
        public DateTime AddTime { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("InjectionId")]
        public virtual Injection Injection { get; set; }
    }
}
