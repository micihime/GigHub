using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(a => a.ArtistId == userId)
                .Include(g => g.Genre)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                Heading = "Gigs I'm Attending",
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View("GigList", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Heading = "Add a Gig",
                Genres = _context.Genres.ToList()
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = _context.Genres.ToList();
                return View("GigForm", vm);
            }

            var gig = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = vm.GetDateTime(),
                GenreId = vm.Genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine");
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.SingleOrDefault(g => g.Id == gigId
                                      && g.ArtistId == userId);

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a Gig",
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }
    }
}