using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artists = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Artist)
                .ToList();

            return View(artists);
        }
    }
}