using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID
        { get; set; }

        public virtual ApplicationUser Poster { get; set; }

        public virtual Enquiry ToEqry { get; set; }

        //Poster should also see if true as like the case noter !
        public bool Private { get; set; }

        public virtual Answer ToAsmt { get; set; }

        public virtual Post RlyWhich { get; set; }

        public string content { get; set; }

        public virtual ICollection<FilePath> FilePaths { get; set; }

        public DateTime postingTime { get; set; }

        public virtual Vote pvoting { get; set; }

        public bool InCnsldtn { get; set; }

        public string type { get; set; }


        public Post()
        {
            postingTime = DateTime.Now;
            //pvoting = new Vote();
 
        }

        public static Post CopytoNewPoster(Post prepost, ApplicationUser newposter)
        {
            return new Post {
                Poster=newposter,
                ToEqry=prepost.ToEqry,
                content=prepost.content,
                postingTime=prepost.postingTime,
                type=prepost.type,
   
           
            };
        }
    }
}