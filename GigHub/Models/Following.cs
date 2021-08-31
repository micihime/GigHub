using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Artist { get; set; }

        [Key] //creating composite key to identify attendance
        [Column(Order = 1)] //1st part of the key
        public string FollowerId { get; set; }

        [Key] //creating composite key to identify attendance
        [Column(Order = 2)] //2nd part of the key
        public string ArtistId { get; set; }
    }
}