using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    //For both note sharing and enquiry consulting !
    public class ConsultViewModel
    {
        
        //The name should change into "eqry" !
        [Required]
        public Enquiry techEqry { get; set; }

        [Display(Name = "Call Expert")]
        public string[] CallExpert { get; set; }
        //Should enable calling 0~2 Experts ! Should enable quicker response than other contact channels !

        public IPagedList<Enquiry> crntConsultations { get; set; }

        public IPagedList<Enquiry> pastConsultations { get; set; }

        public int FileCount = 1;



        public ConsultViewModel()
        {

        }

        public ConsultViewModel(int? page, int? spage, string UID)
        {
            var DataContext = new ApplicationDbContext();
            string CUid = UID;
            var CrntCons = DataContext.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(CUid) & e.ExpertAnswer.dcsnStatus != "off").AsEnumerable();
            int pageNumber = (page ?? 1);
            crntConsultations = CrntCons.OrderByDescending(e => e.lastUpated).ToPagedList(pageNumber, 3);

            var PastCons = DataContext.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(CUid) & e.ExpertAnswer.dcsnStatus == "off").AsEnumerable();
            int spageNumber = (spage ?? 1);
            pastConsultations = PastCons.OrderByDescending(e => e.lastUpated).ToPagedList(spageNumber, 1);
        }

    }
}

/*
    IEnumerable<HttpPostedFileBase> AttsForAwr { get; set; }
    public bool SltTKT { get; set; }
*/
