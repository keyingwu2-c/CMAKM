using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple02.Models
{
    public class AdvancedSearchViewModel
    {
        [DisplayName("Company Name of Customer")]
        public string CName { get; set; }

        [DisplayName("Product Name")]
        public string PdName { get; set; }

        [DisplayName("Destination of the Product")]
        public string Destination { get; set; }

        [DisplayName("Place of Origin")]
        public string Origination { get; set; }

        [DisplayName("Question Type")]
        public string Type { get; set; }
        //Service Requested, 3 choices

        public string[] Dvs { get; set; }
        //[DisplayName("Divisions Involved")]
        public IEnumerable<SelectListItem> UsedDivisions { get; set; }
        
        [Display(Name = "Key Words")]
        public string SearchStr { get; set; }

        [Display(Name = "Original Question")]
        public string Question { get; set; }

        public SuperHashSet<Enquiry> lstResult { get; set; }

        /*
        public AdvancedSearchViewModel() {
            UsedDivisions = new SelectList(new[] { "Food & Pharmaceutical", "Electrical","Toys & Material","Business & Divisions"});
            
        }*/
    }
}