using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Theatre.Data;

namespace Theatre.Web.Models
{
    public class ReviewInputModel
    {
        
        [Required(ErrorMessage = "Review title is required.")]
        [StringLength(200, ErrorMessage = "The {0} must be between {2} and {1} characters long.",
            MinimumLength = 1)]
        [Display(Name = "Title *")]
        public string Title { get; set; }

        //[DataType(DataType.DateTime)]
        //[Display(Name = "Date and Time *")]
        //public DateTime StartDateTime { get; set; }

       // public TimeSpan? Duration { get; set; }

        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [Display(Name = "Is Public?")]
        public bool IsPublic { get; set; }

        public static ReviewInputModel CreateFromReview(Review r)
        {
            return new ReviewInputModel()
            {
                Title = r.Title,
                //StartDateTime = r.StartDateTime,
                //Duration = r.Duration,
                Location = r.Location,
                Description = r.Description,
                IsPublic = r.IsPublic
            };
        }
    }
}
