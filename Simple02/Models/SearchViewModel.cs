using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;

namespace Simple02.Models
{
    public class SearchViewModel
    {
        //[Required]
        [Display(Name = "Search:")]
        public string SearchStr { get; set; }

        //May delete this !
        public SuperHashSet<Enquiry> lstResult { get; set; }

        public IPagedList<Enquiry> Results { get; set; }

        public SearchViewModel() { }

        public SearchViewModel(string ssinput)
        {
            int pageNumber = 1;
            ApplicationDbContext finding = new ApplicationDbContext();
            Results = finding.Enquirys.Where(x => x.Title.ToString().Contains(ssinput)).AsEnumerable().OrderByDescending(e => e.lastUpated).ToPagedList(pageNumber, 5);
            
        }
   
    }


}