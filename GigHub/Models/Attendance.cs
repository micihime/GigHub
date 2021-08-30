using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }

        [Key] //creating composite key to identify attendance
        [Column(Order = 1)] //1st part of the key
        public int GigId { get; set; }

        [Key] //creating composite key to identify attendance
        [Column(Order = 2)] //2nd part of the key
        public string AttendeeId { get; set; }
    }
}