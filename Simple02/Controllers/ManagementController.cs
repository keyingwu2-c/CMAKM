using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Simple02.Models.FunctionViewModels;

namespace Simple02.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        [Authorize]
        public virtual ActionResult Index(int? page, int? spage)
        {
        
            DiscussionsViewModel dcsreturn = new DiscussionsViewModel(page, spage, User.Identity.GetUserId());
            ViewBag.len2 = dcsreturn.Discussions.Count();

            EnquirerIndexViewModel eqindex = new EnquirerIndexViewModel() {discussions = dcsreturn };
            return View(eqindex);
        }

        [Authorize(Roles = "Management")]
        public virtual ActionResult Management()
        {
            return View();
        }

    }
}