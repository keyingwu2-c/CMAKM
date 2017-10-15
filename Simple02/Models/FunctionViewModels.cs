using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple02.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Web.Mvc;

namespace Simple02.Models
{
    /*SearchViewModel etc. may cut to this cs file ! 
     * No need to reduce the number of view models by combination !*/
    public class FunctionViewModels
    {
        //For people to share notes on enquiries.
        public class NotesViewModel
        {
            public string SrchStr
            { get; set; }

            public string SrchBy
            { get; set; }

            //All existing cases
            public IPagedList<Enquiry> AllCases
            { get; set; }

            public Enquiry Case
            { get; set; }//may not need this !

            //Existing notes inputted by the user.
            public IPagedList<Enquiry> YourNotes
            { get; set; }

            public NotesViewModel()
            {           
                ApplicationDbContext testR = new ApplicationDbContext();                
            }
            public NotesViewModel(string UID, int? page, int? lpage, string sortOrder)
            {
                ApplicationDbContext testR = new ApplicationDbContext();

                int pageNumber = (page ?? 1);

                YourNotes = testR.Enquirys.Where(x=>x.Noter.Id.ToString().Equals(UID)).AsEnumerable().OrderByDescending(e=> e.lastUpated).ToPagedList(pageNumber,5);
                //&&x.ExpRpDate==null

                int lpageNumber = (lpage ?? 1);

                switch (sortOrder)
                {
                    case "LD":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus =="off").AsEnumerable().OrderBy(e => e.lastUpated).ToPagedList(lpageNumber, 5);
                        break;
                    case "Q":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderBy(e => e.Title).ToPagedList(lpageNumber, 5);
                        break;
                    case "Q_desc":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderByDescending(e => e.Title).ToPagedList(lpageNumber, 5);
                        break;
                    case "P":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderBy(e => e.Product).ToPagedList(lpageNumber, 5);
                        break;
                    case "P_desc":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderByDescending(e => e.Product).ToPagedList(lpageNumber, 5);
                        break;
                    case "N":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderBy(e => e.Noter.UserName).ToPagedList(lpageNumber, 5);
                        break;
                    case "N_desc":
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderByDescending(e => e.Noter.UserName).ToPagedList(lpageNumber, 5);
                        break;
                    case "LD_desc":
                    default:
                        AllCases = testR.Enquirys.Where(e => e.ExpertAnswer.dcsnStatus == "off").AsEnumerable().OrderByDescending(e => e.lastUpated).ToPagedList(lpageNumber, 5);
                        break;
                }

     
            }
        }



        public class DiscussionsViewModel
        {

            public IPagedList<Answer> Discussions { get; set; }

            public IPagedList<Answer> PDsns { get; set; }

            public DiscussionsViewModel(int? page, int? spage, string UID)
            {

                ApplicationDbContext geteqryfordcsn = new ApplicationDbContext();
                
                int pageNumber = (page ?? 1);
                Discussions =geteqryfordcsn.Answers.Where(x => x.dcsnStatus.Equals("on")& x.DcnGroup.Participants.Any(u=>u.Id.Equals(UID))).AsEnumerable().OrderBy(x => x.ToEqry.ExpRpDate).ToPagedList(pageNumber, 5);
         
                int spageNumber = (spage ?? 1);
                PDsns = geteqryfordcsn.Answers.Where(x => x.dcsnStatus.Equals("off") & x.DcnGroup.Participants.Any(u => u.Id.Equals(UID))).AsEnumerable().OrderBy(x => x.ToEqry.ExpRpDate).ToPagedList(spageNumber, 5);


            }


        }


        public class EnquirerIndexViewModel
        {

            public DiscussionsViewModel discussions { get; set; }

            public EnquirerIndexViewModel(){  }
        }


        public class InOneDcsnViewModel
        {
            public Answer asmtoDiscuss { get; set; }

            public List<ApplicationUser> participants { get; set; }

            public List<Post> poststoeqry { get; set; }

            public List<Post> Sortedpostslist { get; set; }

            public IPagedList<Post> Pagedpostslist { get; set; }

            public int? CurrentPage { get; set; }

            public int? LastPage { get; set; }

            public Post mycurrentpost { get; set; }

            public InOneDcsnViewModel() { }

            public InOneDcsnViewModel(int? page, int AID)
            {
                ApplicationDbContext getEqry = new ApplicationDbContext();
                asmtoDiscuss = getEqry.Answers.Find(AID);
                poststoeqry = getEqry.Posts.Where(p => p.ToAsmt.AID.Equals(AID)&& p.RlyWhich==null).AsEnumerable().ToList();
                Sortedpostslist = new List<Post>();
                GetChildrens(poststoeqry,getEqry);

                int pageNumber = (page ?? 1);
                Pagedpostslist = new PagedList<Post>(Sortedpostslist, pageNumber, 5);
                CurrentPage = pageNumber;
                LastPage = Pagedpostslist.PageCount;

                if (asmtoDiscuss!=null)
                {
                    participants = asmtoDiscuss.DcnGroup.Participants.ToList();
                }
                
            }

            public void GetChildrens(List<Post> templist, ApplicationDbContext DataContext)
            {
                foreach (Post post in templist)
                {
                    Sortedpostslist.Add(post);
                    var childrens = DataContext.Posts.Where(p => p.RlyWhich.PID.Equals(post.PID)).AsEnumerable().ToList();
                    GetChildrens(childrens,DataContext);
                }

            }
        }

        public class CnslttnDetailViewModel
        {
            public Enquiry eqrytoFbk { get; set; }

            //public List<Post> poststoeqry { get; set; }

            public CnslttnDetailViewModel()
            {

            }
            public CnslttnDetailViewModel(Guid EID)
            {
                ApplicationDbContext getcase = new ApplicationDbContext();
                eqrytoFbk = getcase.Enquirys.Find(EID);
            }

        }

        public class CaseDetailViewModel
        {
            public Enquiry eqrytoVw { get; set; }

            public Post newcomment { get; set; }

            public List<Post> PubComments { get; set; }

            public List<Post> PrvComments { get; set; }

            public bool SendMail { get; set; }

            public CaseDetailViewModel()
            {

            }

            public CaseDetailViewModel(Guid EID)
            {
                ApplicationDbContext getcase = new ApplicationDbContext();
                eqrytoVw=getcase.Enquirys.Find(EID);
                PubComments = eqrytoVw.CaseComments.Where(c => c.Private== false).AsEnumerable().ToList();
                PrvComments = eqrytoVw.CaseComments.Where(c => c.Private == true).AsEnumerable().ToList();
             
            }

        }

    }
}