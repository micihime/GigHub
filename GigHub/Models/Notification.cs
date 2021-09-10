using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; } //used only when gig is updated
        public string OriginalVenue { get; set; } //used only when gig is updated

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        public Notification(NotificationType notifType, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Gig = gig;
            Type = notifType;
            DateTime = DateTime.Now;
        }
    }
}