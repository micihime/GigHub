using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followerId = User.Identity.GetUserId();

            if (_context.Followings.Any(a => a.FollowerId == followerId || a.ArtistId == dto.ArtistId))
                return BadRequest("The following already exists.");

            var following = new Following()
            {
                ArtistId = dto.ArtistId,
                FollowerId = followerId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}