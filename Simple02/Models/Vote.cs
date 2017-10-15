using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class Vote
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VID
        { get; set; }

        public int forvotes { get; set; }

        public virtual ICollection<ApplicationUser> Voters { get; set; }

        public Vote()
        {
            forvotes = 0;
            Voters = new List<ApplicationUser>();
        }
    }

   
}