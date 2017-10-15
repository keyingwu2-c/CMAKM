using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    //Answer = The Whole Discussion !
    public class Answer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AID
        { get; set; }

        [Required]
        public virtual Enquiry ToEqry { get; set; }

        //This will be used to store selected posts of consolidation ! Change the name later !
        public string content { get; set; }

        //Delete this
        public virtual ICollection<FilePath> FilePaths { get; set; }

        public virtual ICollection<Post> PostsInDsn { get; set; }

        public virtual Group DcnGroup { get; set; }

        public string dcsnStatus { get; set; }

        public DateTime? dsnbgntime { get; set; }

        //Change this into time of the latest post !
        public DateTime sendTime { get; set; }

        public Answer()
        {
         
        }
    }
}