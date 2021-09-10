using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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

        public bool IsCancelled { get; set; }

        public Genre Genre { get; set; } //navigation property

        [Required]
        public byte GenreId { get; set; } //foreign key property

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }
    }
}