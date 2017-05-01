using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Theatre.Data;

namespace Theatre.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //define the collections of reviews and comments
        public IDbSet<Review> Reviews { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}