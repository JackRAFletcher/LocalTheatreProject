using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Theatre.Web.Models;

namespace Theatre.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //order the reviews based on their start date
            var reviews = this.db.Reviews
                .OrderBy(e => e.StartDateTime)
                .Where(e => e.IsPublic)
                .Select(e => new ReviewViewModel()
                {
                    Id = e.Id, Title = e.Title, StartDateTime = e.StartDateTime, Duration = e.Duration, Author = e.Author.FullName, Location = e.Location 
                });

            var upcomingReviews = reviews.Where(e => e.StartDateTime > DateTime.Now);
            var passedReviews = reviews.Where(e => e.StartDateTime <= DateTime.Now);
            return View(new UpcomingPassedReviewsViewModel()
            {
                UpcomingReviews = upcomingReviews,
                PassedReviews = passedReviews
            });
        }

        public ActionResult ReviewDetailsById(int id)
        {
            //order reviews based on their id
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var ReviewDetails = this.db.Reviews
                .Where(r => r.Id == id)
                .Where(r => r.IsPublic || isAdmin || (r.AuthorId != null && r.AuthorId == currentUserId))
                .Select(ReviewDetailsViewModel.ViewModel)
                .FirstOrDefault();

            var isOwner = (ReviewDetails != null && ReviewDetails.AuthorId != null &&
                ReviewDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;

            return this.PartialView("_ReviewDetails", ReviewDetails);
        }
    }
}