using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatre.Data
{
   public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        //get and set all atributes for the comments
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ReviewId { get; set; }

        [Required]
        public virtual Review Review { get; set; }








    }
}
