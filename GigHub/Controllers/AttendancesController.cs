using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            var artistId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == artistId || a.GigId == gigId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance()
            {
                GigId = gigId,
                AttendeeId = artistId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}