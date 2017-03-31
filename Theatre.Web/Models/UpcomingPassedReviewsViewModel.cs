using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Theatre.Web.Models
{
    public class UpcomingPassedReviewsViewModel
    {
        public IEnumerable<ReviewViewModel> UpcomingReviews { get; set; }
        public IEnumerable<ReviewViewModel> PassedReviews { get; set; }
       
    }
}