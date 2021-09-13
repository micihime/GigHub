using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; } //used only when gig is updated
        public string OriginalVenue { get; private set; } //used only when gig is updated

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {
        }

        private Notification(NotificationType notifType, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Gig = gig;
            Type = notifType;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            var notif = new Notification(NotificationType.GigUpdated, gig);
            notif.OriginalDateTime = originalDateTime;
            notif.OriginalVenue = originalVenue;
            return notif;
        }


        public static Notification GigCancelled(Gig gig)
        {
            return new Notification(NotificationType.GigCancelled, gig);
        }
    }
}