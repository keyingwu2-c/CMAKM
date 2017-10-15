using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class Group
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GroupID")]
        public int GID
        { get; set; }

        //public virtual ICollection<ApplicationUser>  { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }

        public virtual ICollection<Answer> AssignmentsForGDsns { get; set; }

        public Group()
        {

        }
    }

    public class Division : Group
    {
        [Column("division_name")]
        public string DName { get; set; }
    }
}