using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class Customer
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_code")]
        public Guid CID { get; set; }

        [Column("customer_name")]
        public string CName { get; set; }
        //Company Name



    }
}