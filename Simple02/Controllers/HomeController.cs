using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple02.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;
using PagedList;
using System.Web.UI;
using static Simple02.Models.FunctionViewModels;
using URE.Common_Code;
using static Simple02.Models.GlobalVariables;

namespace Simple02.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        [Authorize]
        public async Task<ActionResult> Index(int? role)
        {
            var username = User.Identity.GetUserName();
            var userId = User.Identity.GetUserId();

            if (username != null && username != "")
            {
                //return Content(username + " HERE ");
                var rolevalue = 0;//Need to change into a global variable !
                ApplicationDbContext rolegetter = new ApplicationDbContext();

                if (rolegetter.Users.Any(x => x.Id.Equals(userId)))
                {
                    ApplicationUser user = rolegetter.Users.Single(x => x.Id.Equals(userId));
                    if (user.Roles != null)
                    {
                        if (user.Roles.Any(x => x.UserId.Equals(user.Id)))
                            rolevalue = Convert.ToInt32(user.Roles.Last(x => x.UserId.Equals(user.Id)).RoleId);
                    }
                }
                role = rolevalue;
            }

            if (role != null)
            {
                switch (role)
                {
                    case 1:            
                    case 2:
                    case 3:
                        return RedirectToAction("Index", "Enquirer");
                    default:
                        return View();
                }

            }

            return View("ApplyRole");
        }

        //May re-use this
        public bool AddWhileDiff(SuperHashSet<Enquiry> theset, Enquiry other)
        {

            foreach (Enquiry element in theset)
            {
                if (element.Deng((Enquiry)other))
                    return false;
            }
            theset.Add(other);
            return true;
        }

        public async Task<ActionResult> Library(int? page, int? lpage, string sortOrder)
        {
            ViewBag.LDSortParm = sortOrder != "LD_desc" ? "LD_desc" : "LD";
            ViewBag.QSortParm = sortOrder == "Q" ? "Q_desc" : "Q";
            ViewBag.PSortParm = sortOrder == "P" ? "P_desc" : "P";
            ViewBag.NSortParm = sortOrder == "N" ? "N_desc" : "N";

            NotesViewModel testReturn = new NotesViewModel(User.Identity.GetUserId(), page, lpage, sortOrder);
            ViewBag.len = testReturn.YourNotes.Count();
            ViewBag.PageNumber = (page ?? 1);
            ViewBag.PageCount = testReturn.YourNotes.PageCount;

            ViewBag.llen = testReturn.AllCases.Count();
            ViewBag.LPageNumber = (lpage ?? 1);
            ViewBag.LPageCount = testReturn.AllCases.PageCount;


            return View(testReturn);
        }


        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Roles="Role1,Role2,...")]
        public async Task<ActionResult> Search(SearchViewModel model)
        {

            SearchViewModel nshm = new SearchViewModel(model.SearchStr);
            ViewBag.Count = nshm.Results.Count;
            return View(nshm);
        }


        public ActionResult Advance()
        {
            return View();
            /*
            ApplicationDbContext _db = new ApplicationDbContext();
            var result = from r in _db.Enquirys
                         where r.Title.ToLower().Contains("m")
                         select r.Title;
            return Json(result, JsonRequestBehavior.AllowGet);*/
        }

        [HttpPost]
        //[Authorize(Roles="Role1,Role2,...")]
        public ActionResult Advance(AdvancedSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content("Check the required Inputs marked with * !");
            }
            return View(model);
        }


        //[Authorize(User.Identity.IsAuthenticated =true)]
        public ActionResult Consult(int? page, int? spage)
        {
            string UID= User.Identity.GetUserId();
            ConsultViewModel model = new ConsultViewModel(page, spage, UID);
            //var DataContext = new ApplicationDbContext();

            if (spage == null)
            {
                ViewBag.liclass1 = "active text-content";
                ViewBag.tabclass1 = "tab-pane fade in active ";
                ViewBag.liclass2 = "text-content";
                ViewBag.tabclass2 = "tab-pane fade";
            }
            else
            {
                ViewBag.liclass1 = "text-content";
                ViewBag.tabclass1 = "tab-pane fade";
                ViewBag.liclass2 = "active text-content";
                ViewBag.tabclass2 = "tab-pane fade in active";
            }

            Bar = 0;
            ViewBag.FileCount = Bar;

            return View(model);
        }

        public PartialViewResult MoreFileEntries()
        {
            Bar += 1;
            ViewBag.FileCount = Bar;
            return PartialView();
        }

        public PartialViewResult RecordAnswer()
        {
            return PartialView();
        }

        public PartialViewResult CallExperts()
        {
            return PartialView();
        }

        public PartialViewResult ToTechKnowTeam()//ConsultViewModel model
        {
            return PartialView();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Share(ConsultViewModel model, IEnumerable<HttpPostedFileBase> uploads)
        {
            //Show current consultations:
            ViewBag.liclass1 = "active text-content";
            ViewBag.tabclass1 = "tab-pane fade in active ";
            ViewBag.liclass2 = "text-content";
            ViewBag.tabclass2 = "tab-pane fade";

            ApplicationDbContext addEnqNote = new ApplicationDbContext();
            var uid = User.Identity.GetUserId();
            model.crntConsultations = addEnqNote.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(uid) & e.AnswerToCustomer == null).AsEnumerable().OrderBy(e => e.ExpRpDate).ToPagedList(1, 5);
            model.pastConsultations = addEnqNote.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(uid) & e.AnswerToCustomer != null).AsEnumerable().OrderBy(e => e.ExpRpDate).ToPagedList(1, 5);
        

            //The flow:
            if (!ModelState.IsValid || model.techEqry.AnswerToCustomer == null || model.techEqry.BssResult == null)
            {
                ViewBag.massage = "Check the required Fields !";
                return Content("Works");
                //return View(model);
            }

            ApplicationUser current = addEnqNote.Users.Find(uid);
            Enquiry note = new Enquiry(model.techEqry.Title, model.techEqry.AnswerToCustomer, current)
            {
                BssResult = model.techEqry.BssResult,//Useless?

                Product = model.techEqry.Product,
                Service = model.techEqry.Service,
                Destination = model.techEqry.Destination,
                Qdetail = model.techEqry.Qdetail,
                cvoting = new Vote(),
           
            };

            //Files(Attachments) upload:                
            note.FilePaths = new List<FilePath>();
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

                    note.FilePaths.Add(file);

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

            addEnqNote.Enquirys.Add(note);
            addEnqNote.SaveChanges();

            //return RedirectToAction("Index");
            ViewBag.Status = "Successfully Submit !";
            return View("Consult", model);
     
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Consult(ConsultViewModel model, string btnSubmit, IEnumerable<HttpPostedFileBase> uploads)
        {
            ViewBag.liclass1 = "active text-content";
            ViewBag.tabclass1 = "tab-pane fade in active ";
            ViewBag.liclass2 = "text-content";
            ViewBag.tabclass2 = "tab-pane fade";

            var DataContext = new ApplicationDbContext();
            string CUid = User.Identity.GetUserId();
            model.crntConsultations = DataContext.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(CUid) & e.AnswerToCustomer == null).AsEnumerable().OrderBy(e => e.ExpRpDate).ToPagedList(1, 5);
            model.pastConsultations = DataContext.Enquirys.Where(e => e.ExpRpDate != null & e.Noter.Id.Equals(CUid) & e.AnswerToCustomer != null).AsEnumerable().OrderBy(e => e.ExpRpDate).ToPagedList(1, 5);

            if (false)//!ModelState.IsValid
            {
                ViewBag.Message = "Please provide all the required information !";
                return View(model);
                //return Content(uploads.Any().ToString());
            }
            else
            {
                //Create an Enquiry Record:
                ApplicationUser current = DataContext.Users.Find(CUid);

                Enquiry consult = new Enquiry(model.techEqry.Title, current)
                {
                    ExpRpDate = model.techEqry.ExpRpDate,
                    Qdetail = model.techEqry.Qdetail,
                    Product = model.techEqry.Product,
                    Service = model.techEqry.Service,
                    Destination = model.techEqry.Destination,
                    cvoting = new Vote(),
                };

                //Files(Attachments) upload:                
                consult.FilePaths = new List<FilePath>();
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

                        consult.FilePaths.Add(file);

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
                /*
                if (model.File != null&&model.File.ContentLength>0)
                {
                    byte[] uploadedFile = new byte[model.File.InputStream.Length];
                    model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                    consult.File = uploadedFile;
                 }*/
                DataContext.Enquirys.Add(consult);

                DataContext.SaveChanges();

                switch (btnSubmit)
                {
                    case "Consult Experts":

                        //Call Experts:
                        Group TT = new Group();
                        TT.Participants = new List<ApplicationUser>();
                        foreach (string uid in model.CallExpert)
                        {

                            var call = DataContext.Users.Find(uid);

                            if (call != null)
                            {
                                //consult.ExpertAnswer.DcnGroup.Participants.Add(call);
                                TT.Participants.Add(call);
                            }        
                        }
                        TT.Participants.Add(current);

                        consult.ExpertAnswer = new Answer()
                        {
                            sendTime = (DateTime)model.techEqry.ExpRpDate,
                            DcnGroup = TT,
                            dcsnStatus = "on",
                            dsnbgntime = DateTime.Now
                        };
                        DataContext.SaveChanges();
                        ViewBag.Status = "Successfully Submit. Assignees shall reply to you in 2 days ! ";
                        break;

                    case "Consult TechKnowTeam":
                    default:

                        //create TKT discussion:
                        Group TKT = DataContext.Groups.Find(24);
                        TKT.Participants.Add(current);
                            consult.ExpertAnswer = new Answer()
                            {
                                sendTime = (DateTime)model.techEqry.ExpRpDate,
                                DcnGroup = TKT,
                                dcsnStatus = "on",
                                dsnbgntime = DateTime.Now
                            };
                   
                        DataContext.SaveChanges();
                        
                        ViewBag.Status = "Successfully Submit. TechKnowTeam shall reply to you in 2 days ! ";
                        break;
                }
                
                return View(model);
            }
            //There is a bug of titles showing, still do not know why        
        }

        //Should Change into ConsultationDetail Page Function
        public async Task<ActionResult> Cancel(Guid EID)
        {
            var DataContext = new ApplicationDbContext();
            Enquiry eq = DataContext.Enquirys.SingleOrDefault(e => e.EID.Equals(EID));

            //Delete files and posts directly to an enquiry
            var efs = eq.FilePaths.AsEnumerable().ToList();
            DataContext.FilePaths.RemoveRange(efs);
            var eps = eq.CaseComments.AsEnumerable().ToList();
            DataContext.Posts.RemoveRange(eps);

            //Delete files connected to answers, posts and their files in dsn, and answers to enquiry:
            var assignment = eq.ExpertAnswer;

            var aps = assignment.PostsInDsn.AsEnumerable().ToList();
            foreach (Post dpost in aps)
            {
                var pfs = dpost.FilePaths.AsEnumerable().ToList();
                DataContext.FilePaths.RemoveRange(pfs);
            }
            DataContext.Posts.RemoveRange(aps);

            DataContext.Answers.Remove(assignment);
            
            DataContext.Enquirys.Remove(eq);
            DataContext.SaveChanges();
            return RedirectToAction("Consult", new { page = 1 });
         }


        public async Task<ActionResult> CnstDetail(Guid EID)
        {
            CnslttnDetailViewModel model = new CnslttnDetailViewModel(EID);

            return View(model);

        }

        [HttpPost]
        public async Task<ActionResult> MarkCAnswr(CnslttnDetailViewModel model, string btnSubmit)
        {
            ApplicationDbContext fbinput = new ApplicationDbContext();
            Enquiry myEnqry = fbinput.Enquirys.Find(model.eqrytoFbk.EID);
            //get AnswerToCustomer:
            myEnqry.AnswerToCustomer = model.eqrytoFbk.AnswerToCustomer;
            myEnqry.CompletedDate = DateTime.Now;
            fbinput.SaveChanges();

            switch (btnSubmit)
            {
                case "Post":
                    myEnqry.ExpertAnswer.dcsnStatus = "off";
                    break;
                case "Save":
                default:
                    break;
            }
            fbinput.SaveChanges();
            return RedirectToAction("CnstDetail",new {EID= model.eqrytoFbk.EID });
        }

        
        
    }
}
/*
            if ((model.Case.Qtype == QType.Other & model.Case.TypeSpecified == null) || (model.Case.Qtype == QType.Regulation & model.Case.Regulation == null))
            {
                return View(model);
            }*/
