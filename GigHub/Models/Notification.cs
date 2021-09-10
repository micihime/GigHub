using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; } //used only when gig is updated
        public string OriginalVenue { get; set; } //used only when gig is updated

        [Required]
        public Gig Gig { get; set; }
    }
}