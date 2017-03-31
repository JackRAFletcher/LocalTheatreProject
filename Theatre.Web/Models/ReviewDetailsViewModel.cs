using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Theatre.Data;

namespace Theatre.Web.Models
{
    public class ReviewDetailsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
                     
        public static Expression<Func<Review, ReviewDetailsViewModel>> ViewModel
        {
            get
            {
                return r => new ReviewDetailsViewModel()
                {
                    Id = r.Id,
                    Description = r.Description,
                    Comments = r.Comments.AsQueryable().Select(CommentViewModel.ViewModel),
                    AuthorId = r.Author.Id
                };
            }
        }
    }
    }
