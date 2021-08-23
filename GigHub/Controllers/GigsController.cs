using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel vm)
        {
            var userId = User.Identity.GetUserId();
            var artist = _context.Users.Single(u => u.Id == userId);
            var genre = _context.Genres.Single(g => g.Id == vm.Genre);

            var gig = new Gig()
            {
                Artist = artist,
                DateTime = DateTime.Parse(string.Format("{0} {1}", vm.Date, vm.Time)), //for now, assuming user puts in valid data (validation in the next task)
                Genre = genre,
                Venue = vm.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home"); //temporary redirection to homepage
        }
    }
}