using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Theatre.Data;
using Theatre.Web.Extensions;
using Theatre.Web.Models;

namespace Theatre.Web.Controllers
{
    [Authorize]
    public class ReviewsController : BaseController
    {
        public ActionResult My()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var Reviews = this.db.Reviews
                .Where(e => e.AuthorId == currentUserId)
                .OrderBy(e => e.StartDateTime)
                .Select(ReviewViewModel.ViewModel);

            var upcomingReviews = Reviews.Where(e => e.StartDateTime > DateTime.Now);
            var passedReviews = Reviews.Where(e => e.StartDateTime <= DateTime.Now);
            return View(new UpcomingPassedReviewsViewModel()
            {
                UpcomingReviews = upcomingReviews,
                PassedReviews = passedReviews
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var e = new Review()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    //StartDateTime = model.StartDateTime,
                   // Duration = model.Duration,
                    Description = model.Description,
                    Location = model.Location,
                    IsPublic = model.IsPublic
                };
                this.db.Reviews.Add(e);
                this.db.SaveChanges();
                this.AddNotification("Review created.", NotificationType.INFO);
                return this.RedirectToAction("My");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ReviewToEdit = this.LoadReview(id);
            if (ReviewToEdit == null)
            {
                this.AddNotification("Cannot edit Review #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }

            var model = ReviewInputModel.CreateFromReview(ReviewToEdit);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReviewInputModel model)
        {
            var ReviewToEdit = this.LoadReview(id);
            if (ReviewToEdit == null)
            {
                this.AddNotification("Cannot edit Review #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }

            if (model != null && this.ModelState.IsValid)
            {
                ReviewToEdit.Title = model.Title;
                //ReviewToEdit.StartDateTime = model.StartDateTime;
                //ReviewToEdit.Duration = model.Duration;
                ReviewToEdit.Description = model.Description;
                ReviewToEdit.Location = model.Location;
                ReviewToEdit.IsPublic = model.IsPublic;

                this.db.SaveChanges();
                this.AddNotification("Review edited.", NotificationType.INFO);
                return this.RedirectToAction("My");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var ReviewToDelete = this.LoadReview(id);
            if (ReviewToDelete == null)
            {
                this.AddNotification("Cannot delete Review #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }

            var model = ReviewInputModel.CreateFromReview(ReviewToDelete);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ReviewInputModel model)
        {
            var ReviewToDelete = this.LoadReview(id);
            if (ReviewToDelete == null)
            {
                this.AddNotification("Cannot delete Review #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }

            this.db.Reviews.Remove(ReviewToDelete);
            this.db.SaveChanges();
            this.AddNotification("Review deleted.", NotificationType.INFO);
            return this.RedirectToAction("My");
        }

        private Review LoadReview(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var ReviewToEdit = this.db.Reviews
                .Where(e => e.Id == id)
                .FirstOrDefault(e => e.AuthorId == currentUserId || isAdmin);
            return ReviewToEdit;
        }
    }
    }