using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet] //[HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancelled)
            {
                return NotFound();
            }

            gig.IsCancelled = true;

            var notif = new Notification
            {
                DateTime = System.DateTime.Now,
                Gig = gig,
                Type = NotificationType.GigCancelled
            };

            var attendees = _context.Attendances.Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendee in attendees)
            {
                attendee.Notify(notif);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
