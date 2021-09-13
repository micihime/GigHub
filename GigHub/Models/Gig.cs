using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public ApplicationUser Artist { get; set; } //navigation property

        [Required]
        public string ArtistId { get; set; } //foreign key property

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public bool IsCancelled { get; private set; }

        public Genre Genre { get; set; } //navigation property

        [Required]
        public byte GenreId { get; set; } //foreign key property

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notif = new Notification(NotificationType.GigCancelled, this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notif);
            }
        }
    }
}