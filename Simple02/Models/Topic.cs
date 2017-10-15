using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple02.Models
{
    public class Topic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TID
        { get; set; }

        public string TName { get; set; }

        public ICollection<Enquiry> Enquiries { get; set; }


        //Use what structure to store Thesaurus ?

        public enum TType
        {
            Product,
            Service,
            Region,
            Regulation,
            Other
        }

        public Topic() { }
    }

    public class Product: Topic
    {

    }

    public class Destination : Topic
    {

    }

}