using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple02.Models;
using static Simple02.Models.FunctionViewModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using static Simple02.Models.Enquiry;
using PagedList;
using System.Data.Entity.Validation;
using System.IO;
using static Simple02.Models.GlobalVariables;

namespace Simple02.Controllers
{
    public class EnquirerController : Controller
    {

        [Authorize]
        public virtual ActionResult Index(int? page, int? spage)
        {
            //Initialize tabs
            ViewBag.liclassA = "active";
            ViewBag.tabclassA = "tab-pane fade in active";
            ViewBag.liclassB = "";
            ViewBag.tabclassB = "tab-pane fade";

            if (spage == null)
            {
                ViewBag.liclassA = "active text-content";
                ViewBag.tabclassA = "tab-pane fade in active";
                ViewBag.liclassB = "text-content";
                ViewBag.tabclassB = "tab-pane fade";
            }
            else
            {
                ViewBag.liclassA = "text-content";
                ViewBag.tabclassA = "tab-pane fade";
                ViewBag.liclassB = "active text-content";
                ViewBag.tabclassB = "tab-pane fade in active";
            }
           
            
            DiscussionsViewModel dcsreturn = new DiscussionsViewModel(page, spage, User.Identity.GetUserId());
            ViewBag.len2 = dcsreturn.Discussions.Count();
            ViewBag.slen2 = dcsreturn.PDsns.Count();

            EnquirerIndexViewModel eqindex = new EnquirerIndexViewModel() {  discussions = dcsreturn };
            return View(eqindex);
        }


        /*
        [HttpPost]
        public virtual async Task<ActionResult> InviteMore(InOneDcsnViewModel model)
        {
            ApplicationDbContext updater = new ApplicationDbContext();
            //return Content(model.eqrytoAnswer.EID.ToString());
            //add initial participants:
            var assigncon = updater.Answers.Find(model.AnswerToEnquirer.AID);
            assigncon.DcnGroup = new Group();
            assigncon.DcnGroup.Participants = new List<ApplicationUser>();
            ApplicationUser me = updater.Users.Find(User.Identity.GetUserId());
            assigncon.DcnGroup.Participants.Add(me);
            foreach (string uid in model.OtrPtcpts)
            {
                var sltduser = updater.Users.Find(uid);
                if (assigncon.DcnGroup.Participants.Contains(sltduser) == false)
                {
                    assigncon.DcnGroup.Participants.Add(sltduser);
                }

            }
            //Change the consultation status
            assigncon.dcsnStatus = "on";
            assigncon.dsnbgntime = DateTime.Now;
            updater.SaveChanges();

            return RedirectToAction("Join", "Enquirer", new { AID = model.AnswerToEnquirer.AID });
        }

        */

        public PartialViewResult NvSearch(int? lpage, string sortOrder)
        {
            ViewBag.LDSortParm = sortOrder != "LD_desc" ? "LD_desc" : "LD";
            ViewBag.QSortParm = sortOrder == "Q" ? "Q_desc" : "Q";
            //PSortParm
            ViewBag.NSortParm = sortOrder == "N" ? "N_desc" : "N";

            NotesViewModel testReturn = new NotesViewModel(User.Identity.GetUserId(), 1, lpage, sortOrder);
            ViewBag.llen = testReturn.AllCases.Count();
            ViewBag.LPageNumber = 1;
            ViewBag.LPageCount = testReturn.AllCases.PageCount;
            return PartialView(testReturn);
        }

        public async Task<ActionResult> Detail(Guid EID)
        {
            var DataContext = new ApplicationDbContext();

            ApplicationUser user = DataContext.Users.Find(User.Identity.GetUserId());
            ViewBag.Role = user.Roles.Last().RoleId;
            DataContext.Enquirys.Find(EID).pageview += 1;
            DataContext.SaveChanges();
            CaseDetailViewModel model = new CaseDetailViewModel(EID);

            return View(model);
        }

        public async Task<PartialViewResult> CaseVote(Guid eId)
        {
            ApplicationDbContext updater = new ApplicationDbContext();
            ApplicationUser crnUser = updater.Users.Find(User.Identity.GetUserId());
            //Enquiry case = updater.Enquirys.Find(eId);

            if (updater.Enquirys.Find(eId).cvoting.Voters.Contains(crnUser) == false)
            {
                updater.Enquirys.Find(eId).cvoting.forvotes += 1;
                updater.Enquirys.Find(eId).cvoting.Voters.Add(crnUser);
            }
            updater.SaveChanges();

            CaseDetailViewModel show = new CaseDetailViewModel(eId);
            return PartialView(show);
        }

        public PartialViewResult QInfo(string PackUp, Guid EID)
        {
            ViewBag.pkp = PackUp;
            CaseDetailViewModel show = new CaseDetailViewModel(EID);
            return PartialView(show);
        }

        public PartialViewResult AInfo(string PackUp, Guid EID)
        {
            ViewBag.pkp = PackUp;
            CaseDetailViewModel show = new CaseDetailViewModel(EID);
            return PartialView(show);
        }

        [HttpPost]
        public async Task<PartialViewResult> Comment(CaseDetailViewModel model)
        {
            ApplicationDbContext addcmmt = new ApplicationDbContext();

            model.newcomment.ToEqry = addcmmt.Enquirys.Find(model.eqrytoVw.EID);
            model.newcomment.Poster = addcmmt.Users.Find(User.Identity.GetUserId());
            model.newcomment.pvoting = new Vote();
            addcmmt.Posts.Add(model.newcomment);
            addcmmt.SaveChanges();

            if (model.SendMail)
            {
                IdentityMessage suggest = new IdentityMessage();
                string front = "Dear Noter, <br /> my comment to the case titled < " + model.newcomment.ToEqry.Title + " > is as follow: <br />";
                string end = "<br />Best Regards !";
                suggest.Body =front+ model.newcomment.content+end;
                suggest.Subject = "Comment from " + model.newcomment.Poster.UserName;
                suggest.Destination = model.newcomment.Poster.Email;

                EmailService casesugst = new EmailService();
                await casesugst.SendAsync01(suggest);
                ViewBag.MailStatus = "Your comment has been sent to the Noter.";
            }

            CaseDetailViewModel show = new CaseDetailViewModel(model.eqrytoVw.EID);
            return PartialView(show);
        }

        [HttpPost] //Delete note/case
        public async Task<ActionResult> Delete(Guid EID)
        {
            ApplicationDbContext deleter = new ApplicationDbContext();
            var enquiry = deleter.Enquirys.Find(EID);
            
            //Add removal of attachments to comment posts here !
            IEnumerable<Post> toremove = deleter.Posts.Where(x => x.ToEqry.EID.Equals(EID)).AsEnumerable().ToList();
            deleter.Posts.RemoveRange(toremove);
            return Content("Removed related comments");

            IEnumerable<FilePath> toremove2 = deleter.FilePaths.Where(x => x.enquiry.EID.Equals(EID)).AsEnumerable().ToList();           
            deleter.FilePaths.RemoveRange(toremove2);

            var aws = deleter.Answers.Where(a => a.ToEqry.EID.Equals(EID)).AsEnumerable().ToList();
            foreach (Answer assignment in aws)
            {
                //Add removal of attachments to dsn posts here !
                var rpsa = deleter.Posts.Where(p => p.ToAsmt.AID.Equals(assignment.AID)).AsEnumerable().ToList();
                deleter.Posts.RemoveRange(rpsa);
                //Add removal of attachments to expert-answers here !
            }
            deleter.Answers.RemoveRange(aws);

            deleter.Enquirys.Remove(enquiry);
            
            deleter.SaveChanges();
            //return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(Guid EID)
        {
            ApplicationDbContext editer = new ApplicationDbContext();
            var enquiry = editer.Enquirys.Find(EID);
            if (enquiry == null)
                return Content("Bug");
            CaseDetailViewModel testReturn = new CaseDetailViewModel(EID);

            return View(testReturn);
            /*

            return RedirectToAction("Index");*/
        }

        [HttpPost]//Edit note/case
        public async Task<ActionResult> Edit(CaseDetailViewModel model)
        {
            ApplicationDbContext updater = new ApplicationDbContext();

            updater.Enquirys.Find(model.eqrytoVw.EID).Title = model.eqrytoVw.Title;
            updater.Enquirys.Find(model.eqrytoVw.EID).AnswerToCustomer = model.eqrytoVw.AnswerToCustomer;
            updater.Enquirys.Find(model.eqrytoVw.EID).Product = model.eqrytoVw.Product;
            updater.Enquirys.Find(model.eqrytoVw.EID).Service = model.eqrytoVw.Service;
            updater.Enquirys.Find(model.eqrytoVw.EID).Destination = model.eqrytoVw.Destination;
            updater.Enquirys.Find(model.eqrytoVw.EID).Regulation = model.eqrytoVw.Regulation;
            updater.Enquirys.Find(model.eqrytoVw.EID).TypeSpecified = model.eqrytoVw.TypeSpecified;
            updater.Enquirys.Find(model.eqrytoVw.EID).lastUpated = DateTime.Now;
            updater.SaveChanges();

            return RedirectToAction("Edit",new {EID= model.eqrytoVw.EID });
        }



        public virtual async Task<ActionResult> Join(int? page,int AID)
        {            
            InOneDcsnViewModel onedcsn = new InOneDcsnViewModel(page, AID);
            ViewBag.len = onedcsn.poststoeqry.Count();
            ViewBag.PageNumber = (page ?? 1);
            ViewBag.PageCount = onedcsn.Pagedpostslist.PageCount;
            Bar = 0;

            return View(onedcsn);
        }








        //Post in discussion detail page.
        [HttpPost]
        public virtual async Task<ActionResult> Post(InOneDcsnViewModel model, IEnumerable<HttpPostedFileBase> uploads,int? ParentId, int? AnswerId, int? CurrentPage)
        {
            if (model.mycurrentpost.content == null)
            {
                //return RedirectToAction("Join", new { AID = model.asmtoDiscuss.AID });
                return Content("Will show a warning dialoge, haha");
            }
            //return Content(model.LastPage.ToString());
            ApplicationDbContext addpost = new ApplicationDbContext();
         
            if (AnswerId != null)
            {
                model.mycurrentpost.ToAsmt = addpost.Answers.Find(AnswerId);
            }
            else
            {
                model.mycurrentpost.ToAsmt = addpost.Answers.Find(model.asmtoDiscuss.AID);
            }
            if (ParentId != null)
            {
                model.mycurrentpost.RlyWhich = addpost.Posts.Find(ParentId);
            }
            model.mycurrentpost.Poster = addpost.Users.Find(User.Identity.GetUserId());
            model.mycurrentpost.pvoting = new Vote();
            
            //Files(Attachments) upload:
            model.mycurrentpost.FilePaths= new List<FilePath>();
            foreach (var upload in uploads)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string ext = Path.GetExtension(upload.FileName).ToLower();
                    string mimeType = "application/unknown";
                    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                    if (regKey != null && regKey.GetValue("Content Type") != null)
                    {
                        mimeType = regKey.GetValue("Content Type").ToString();
                    }

                    var PictureExtensions = new[] { ".png", ".jpg", ".gif", ".jpeg" };
                    var showType = FileType.Tex;
                    if (PictureExtensions.Contains(ext))
                        showType = FileType.Ref;


                    var file = new FilePath
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        OrgnFileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = showType,
                        ContentType = mimeType

                    };

                    model.mycurrentpost.FilePaths.Add(file);

                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"),
                                                   Path.GetFileName(file.FileName));
                        upload.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        return Content(ViewBag.Message);
                    }
                }
            }


            
            addpost.Posts.Add(model.mycurrentpost);
            addpost.SaveChanges();

            //return View("Join", model);
            int aId = model.mycurrentpost.ToAsmt.AID;
            if (ParentId != null)
            {          
                return RedirectToAction("Join", new { page = model.CurrentPage, AID = aId });
            }
            else {
                if (model.LastPage == 0) { model.LastPage += 1; }
                return RedirectToAction("Join", new { page = model.LastPage, AID = aId });
            }
            
        }

        public virtual async Task<ActionResult> VoteFor(int aId, int pId)
        {
            ApplicationDbContext updater = new ApplicationDbContext();
            ApplicationUser crnUser = updater.Users.Find(User.Identity.GetUserId());
            Post post = updater.Posts.Find(pId);

            if (post.pvoting.Voters.Contains(crnUser) == false)
            {   post.pvoting.forvotes += 1;
                post.pvoting.Voters.Add(crnUser);
            }
            updater.SaveChanges();
            return RedirectToAction("Join", new { AID = aId });
        }

        public PartialViewResult ReplyPost(string PackUp, int? ParentId, int? AnswerId, int? CurrentPage)//UD received is empty
        {
            ViewBag.pkp =PackUp!="Show"?"Show": "NotShow";
           
            ViewBag.ParentId = ParentId;
            ViewBag.AnswerId = AnswerId;
            ViewBag.CurrentPage = CurrentPage;
            Bar = 0;
            return PartialView();
        }

        //[HttpPost]
        public async Task<ActionResult> EdPost(int PID, string content)//int PID, string content
        {
            ApplicationDbContext editer = new ApplicationDbContext();
            var post = editer.Posts.Find(PID);
            post.content = content;
            editer.SaveChanges();
            //return Content(""+content);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DelPost(int PID)
        {
            ApplicationDbContext deleter = new ApplicationDbContext();
            var post = deleter.Posts.Find(PID);
            IEnumerable<FilePath> files = deleter.FilePaths.Where(x => x.post.PID.Equals(PID)).AsEnumerable().ToList();
            deleter.FilePaths.RemoveRange(files);
            deleter.Posts.Remove(post);
            deleter.SaveChanges();
            return Content(PID.ToString());
        }

        public async Task<ActionResult> Download(string ImageName)
        {
            return File("~/Images/" + ImageName, "image/jpeg");
        }

        public async Task<ActionResult> DownloadTF(string TextFileName, string ContentType, string DownloadName)
        {
            return File("~/Images/" + TextFileName, ContentType, DownloadName);
            //return File("~/Images/" + TextFileName, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }

    }
}

/*


                
            
            
*/
